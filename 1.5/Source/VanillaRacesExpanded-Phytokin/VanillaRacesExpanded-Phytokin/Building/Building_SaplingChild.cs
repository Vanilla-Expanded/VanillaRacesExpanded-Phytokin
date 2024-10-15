using Verse;
using RimWorld;
using System.Collections.Generic;
using RimWorld.Planet;

namespace VanillaRacesExpandedPhytokin
{
    public class Building_SaplingChild : Building
    {
        public List<GeneDef> MotherGenes = new List<GeneDef>();
        public XenotypeDef MotherXenotype;
        public XenotypeIconDef MotherXenotypeIcon;
        public string MotherXenotypeName;
        public bool MotherUniqueXenotype;
        public Pawn Mother;

        private const int TicksPerDay = 60000;
        private const int TicksToCheckGraphic = 6000;
        private const int TicksToBirth = TicksPerDay * 30; // 30 days

        public int TickCounter;
        public bool SuccessfulBirth;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(ref MotherGenes, nameof(MotherGenes), LookMode.Def);
            Scribe_Defs.Look(ref MotherXenotype, nameof(MotherXenotype));
            Scribe_Values.Look(ref TickCounter, nameof(TickCounter));
            Scribe_Values.Look(ref SuccessfulBirth, nameof(SuccessfulBirth));
            Scribe_References.Look(ref Mother, nameof(Mother));
        }

        public override string GetInspectString()
        {
            string text = base.GetInspectString();

            if (MotherXenotype != null)
            {
                if (text.Length != 0)
                {
                    text += "\n";
                }

                text += "VRE_MotherXenotype".Translate(MotherXenotypeName);
                text += "\n";
                text += "VRE_TimeToHatch".Translate((TicksToBirth - TickCounter).ToStringTicksToPeriod());
            }

            return text;
        }

        public override void Tick()
        {
            base.Tick();
            TickCounter++;
            if (TickCounter > TicksToBirth)
            {
                Hatch();
            }

            if (TickCounter % TicksToCheckGraphic == 0)
            {
                Map.mapDrawer.MapMeshDirty(Position, MapMeshFlagDefOf.Things | MapMeshFlagDefOf.Buildings);
            }
        }

        public override Graphic Graphic
        {
            get
            {
                float sizePercentage = (float)TickCounter / (float)TicksToBirth + 0.5f;
                Graphic modifiedGraphic;
                if (TickCounter > TicksToBirth * 2 / 3)
                {
                    modifiedGraphic = GraphicsCache.graphicMaturePlant;
                    modifiedGraphic.drawSize = def.graphicData.drawSize * sizePercentage * 0.65f;
                }
                else
                {
                    modifiedGraphic = base.Graphic;
                    modifiedGraphic.drawSize = def.graphicData.drawSize * sizePercentage;
                }

                return modifiedGraphic;
            }
        }

        public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
        {
            base.Destroy(mode);
            if (!SuccessfulBirth)
            {
                Mother?.needs?.mood?.thoughts?.memories?.TryGainMemory(ThoughtDefOf.Stillbirth);
            }
        }

        public void Hatch()
        {
            try
            {
                string motherLastName = null;
                if (Mother.Name is NameTriple nameTriple)
                {
                    motherLastName = nameTriple.Last;
                }

                PawnGenerationRequest request = new PawnGenerationRequest(Mother.kindDef, Mother.Faction,
                    PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: false, allowDead: false,
                    allowDowned: true, canGeneratePawnRelations: true, mustBeCapableOfViolence: false, 1f,
                    forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: false, allowFood: true,
                    allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false,
                    forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0f, null, 1f,
                    null, null, null, null, null, null, null, null, fixedLastName: motherLastName, null, null, null,
                    forceNoIdeo: false, forceNoBackstory: false, forbidAnyTitle: false, forceDead: false, null, null,
                    null, null, null, 0f,
                    DevelopmentalStage.Newborn);

                Pawn pawn = PawnGenerator.GeneratePawn(request);
                if (PawnUtility.TrySpawnHatchedOrBornPawn(pawn, this))
                {
                    if (pawn != null)
                    {
                        if (Mother != null)
                        {
                            if (pawn.playerSettings != null && Mother.playerSettings != null)
                            {
                                pawn.playerSettings.AreaRestrictionInPawnCurrentMap = Mother.playerSettings.AreaRestrictionInPawnCurrentMap;
                            }

                            if (pawn.RaceProps.IsFlesh)
                            {
                                pawn.relations.AddDirectRelation(PawnRelationDefOf.Parent, Mother);
                            }
                        }
                    }

                    if (Spawned)
                    {
                        FilthMaker.TryMakeFilth(Position, Map, ThingDefOf.Filth_AmnioticFluid);
                    }

                    ChoiceLetter_BabyBirth choiceLetterBabyBirth = (ChoiceLetter_BabyBirth)LetterMaker.MakeLetter(
                        "VRE_SaplingHatchedLabel".Translate(pawn.NameShortColored),
                        "VRE_SaplingHatched".Translate(pawn.NameShortColored),
                        LetterDefOf.BabyBirth,
                        (TargetInfo)pawn
                    );
                    choiceLetterBabyBirth.Start();
                    Find.LetterStack.ReceiveLetter(choiceLetterBabyBirth);

                    foreach (GeneDef gene in MotherGenes)
                    {
                        pawn.genes.AddGene(gene, false);
                    }

                    if (MotherUniqueXenotype)
                    {
                        pawn.genes.xenotypeName = MotherXenotypeName;
                        pawn.genes.iconDef = MotherXenotypeIcon;
                    }
                    else
                    {
                        pawn.genes.SetXenotype(MotherXenotype);
                    }
                }
                else
                {
                    Find.WorldPawns.PassToWorld(pawn, PawnDiscardDecideMode.Discard);
                }
            }
            finally
            {
                SuccessfulBirth = true;
                Destroy();
            }
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }

            if (!DebugSettings.ShowDevGizmos)
            {
                yield break;
            }

            yield return new Command_Action()
            {
                defaultLabel = "DEV: Hatch Now",
                action = Hatch
            };
        }
    }
}
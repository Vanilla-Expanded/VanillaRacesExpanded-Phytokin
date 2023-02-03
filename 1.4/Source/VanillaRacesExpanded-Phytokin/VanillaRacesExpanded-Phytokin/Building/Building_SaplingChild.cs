using Verse;
using System;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using RimWorld.Planet;

namespace VanillaRacesExpandedPhytokin
{
    public class Building_SaplingChild : Building
    {

        public List<GeneDef> motherGenes = new List<GeneDef>();
        public XenotypeDef motherXenotype;
        public int tickCounter = 0;
        public int ticksToCheckGraphic = 6000;
        public int ticksToBirth = 1800000; // 30 days
        public bool successfulBirth = false;
        public Pawn mother;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(ref this.motherGenes, nameof(this.motherGenes), LookMode.Def);
            Scribe_Defs.Look(ref this.motherXenotype, nameof(this.motherXenotype));
            Scribe_Values.Look(ref this.tickCounter, nameof(this.tickCounter));
            Scribe_Values.Look(ref this.successfulBirth, nameof(this.successfulBirth));
            Scribe_References.Look(ref this.mother, nameof(this.mother));
        }

        public override string GetInspectString()
        {
            string text = base.GetInspectString();
           
            if (motherXenotype != null)
            {
                if (text.Length != 0)
                {
                    text += "\n";
                }
                text += "VRE_MotherXenotype".Translate(motherXenotype.LabelCap);
                text += "\n";
                text += "VRE_TimeToHatch".Translate((ticksToBirth-tickCounter).ToStringTicksToPeriod(true, false, true, true));
            }
            return text;
        }

        public override void Tick()
        {
            base.Tick();
            tickCounter++;
            if ((tickCounter > ticksToBirth))
            {

                Hatch();


            }
            if (tickCounter % ticksToCheckGraphic == 0)
            {
                base.Map.mapDrawer.MapMeshDirty(base.Position, MapMeshFlag.Things | MapMeshFlag.Buildings);
            }

        }

        public override Graphic Graphic
        {
            get
            {
                float sizePercentage = ((float)tickCounter / (float)ticksToBirth)+0.5f;
                Graphic modifiedGraphic;
                if (tickCounter > ticksToBirth * 2 / 3)
                {
                    modifiedGraphic = GraphicsCache.graphicMaturePlant;
                    modifiedGraphic.drawSize = this.def.graphicData.drawSize * sizePercentage *0.65f;

                }
                else
                {
                    modifiedGraphic = base.Graphic;
                    modifiedGraphic.drawSize = this.def.graphicData.drawSize * sizePercentage;
                }




               

                return modifiedGraphic;
            }

        }

        public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
        {
            base.Destroy(mode);
            if (!successfulBirth)
            {
                mother?.needs?.mood?.thoughts?.memories?.TryGainMemory(ThoughtDefOf.Stillbirth);

            }

        }

       

        public void Hatch()
        {
            try
            {
                PawnGenerationRequest request = new PawnGenerationRequest(mother.kindDef, mother.Faction, PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: false, allowDead: false, allowDowned: true, canGeneratePawnRelations: true, mustBeCapableOfViolence: false, 1f, forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: false, allowFood: true, allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0f, null, 1f, null, null, null, null, null, null, null, null, null, null, null, null, forceNoIdeo: false, forceNoBackstory: false, forbidAnyTitle: false, forceDead: false, null, null, null, null, null, 0f, DevelopmentalStage.Newborn);

                Pawn pawn = PawnGenerator.GeneratePawn(request);
                if (PawnUtility.TrySpawnHatchedOrBornPawn(pawn, this))
                {
                    if (pawn != null)
                    {
                        if (mother != null)
                        {
                            if (pawn.playerSettings != null && mother.playerSettings != null)
                            {
                                pawn.playerSettings.AreaRestriction = mother.playerSettings.AreaRestriction;
                            }
                            if (pawn.RaceProps.IsFlesh)
                            {
                                pawn.relations.AddDirectRelation(PawnRelationDefOf.Parent, mother);
                            }
                        }

                    }
                    if (this.Spawned)
                    {
                        FilthMaker.TryMakeFilth(this.Position, this.Map, ThingDefOf.Filth_AmnioticFluid);
                    }

                    Find.LetterStack.ReceiveLetter("VRE_SaplingHatchedLabel".Translate(pawn.NameShortColored), "VRE_SaplingHatched".Translate(pawn.NameShortColored), LetterDefOf.PositiveEvent, (TargetInfo)pawn);



                    foreach (GeneDef gene in motherGenes)
                    {
                        pawn.genes.AddGene(gene, false);
                    }
                    pawn.genes.SetXenotype(motherXenotype);




                }
                else
                {
                    Find.WorldPawns.PassToWorld(pawn, PawnDiscardDecideMode.Discard);
                }

            }
            finally
            {
                successfulBirth = true;
                this.Destroy();
            }
        }


    }
}

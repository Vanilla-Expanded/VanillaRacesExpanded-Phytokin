using Verse;
using System;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using RimWorld.Planet;
using LudeonTK;

namespace VanillaRacesExpandedPhytokin
{
    public class Building_SaplingChild : Building
    {

        public List<GeneDef> motherGenes = new List<GeneDef>();
        public XenotypeDef motherXenotype;
        public XenotypeIconDef motherXenotypeIcon;
        public string motherXenotypeLabel;
        
        public Pawn mother;
        
        public int tickCounter = 0;
        public bool successfulBirth = false;
        
        private const int ticksPerDay = 60000;
        private const int ticksToCheckGraphic = 6000;
        private const int ticksToBirth = ticksPerDay * 30; // 30 days

        private string MotherXenotypeLabel
        {
            get
            {
                if (motherXenotype != null)
                {
                    return motherXenotype.LabelCap;
                }
                return motherXenotypeLabel;
            }
        }
        private bool MotherUniqueXenotype => motherXenotypeLabel != null;
        private bool ShouldHatch => tickCounter > ticksToBirth;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(ref this.motherGenes, nameof(this.motherGenes), LookMode.Def);
            Scribe_Defs.Look(ref this.motherXenotype, nameof(this.motherXenotype));
            Scribe_Defs.Look(ref this.motherXenotypeIcon, nameof(this.motherXenotypeIcon));
            Scribe_Values.Look(ref this.motherXenotypeLabel, nameof(this.motherXenotypeLabel));
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
                text += "VRE_MotherXenotype".Translate(MotherXenotypeLabel);
                text += "\n";
                text += "VRE_TimeToHatch".Translate((ticksToBirth-tickCounter).ToStringTicksToPeriod(true, false, true, true));
            }
            return text;
        }

        public override void Tick()
        {
            base.Tick();
            tickCounter++;
            
            HatchIfReady();
            
            if (tickCounter % ticksToCheckGraphic == 0)
            {
                ResetGraphic();
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
                string motherLastName = null;
                if (mother.Name is NameTriple nameTriple)
                {
                    motherLastName = nameTriple.Last;
                }
                
                PawnGenerationRequest request = new PawnGenerationRequest(mother.kindDef, mother.Faction,
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
                        if (mother != null)
                        {
                            if (pawn.playerSettings != null && mother.playerSettings != null)
                            {
                                pawn.playerSettings.AreaRestrictionInPawnCurrentMap = mother.playerSettings.AreaRestrictionInPawnCurrentMap;
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
                    
                    ChoiceLetter_BabyBirth choiceLetterBabyBirth = (ChoiceLetter_BabyBirth)LetterMaker.MakeLetter(
                        "VRE_SaplingHatchedLabel".Translate(pawn.NameShortColored),
                        "VRE_SaplingHatched".Translate(pawn.NameShortColored),
                        LetterDefOf.BabyBirth,
                        (TargetInfo)pawn
                    );
                    choiceLetterBabyBirth.Start();
                    Find.LetterStack.ReceiveLetter(choiceLetterBabyBirth);

                    foreach (GeneDef gene in motherGenes)
                    {
                        pawn.genes.AddGene(gene, false);
                    }
                    
                    if (MotherUniqueXenotype)
                    {
                        pawn.genes.xenotypeName = motherXenotypeLabel;
                        pawn.genes.iconDef = motherXenotypeIcon;
                    }
                    else
                    {
                        pawn.genes.SetXenotype(motherXenotype);
                    }
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
        private bool HatchIfReady()
        {
            if (ShouldHatch)
            {
                Hatch();
                return true;
            }

            return false;
        }
        
        private void ResetGraphic()
        {
            Map.mapDrawer.MapMeshDirty(Position, MapMeshFlagDefOf.Things | MapMeshFlagDefOf.Buildings);
        }
        
        private void ProgressBirth(int i)
        {
            ProgressBirth((float)i);
        }

        private void ProgressBirth(float i)
        {
            tickCounter += (int)(ticksPerDay * i);
            ResetGraphic();
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
            
            yield return new Command_Action
            {
                defaultLabel = "DEV: Progress Birth (1 hour)",
                action = () => ProgressBirth(1f / 24f)
            };
            
            yield return new Command_Action
            {
                defaultLabel = "DEV: Progress Birth (1 day)",
                action = () => ProgressBirth(1)
            };
            
            yield return new Command_Action
            {
                defaultLabel = "DEV: Progress Birth (2 days)",
                action = () => ProgressBirth(2)
            };
            
            yield return new Command_Action
            {
                defaultLabel = "DEV: Progress Birth (5 days)",
                action = () => ProgressBirth(5)
            };
        }

        [DebugAction("Pawns", name: "Create Saplingchild", actionType = DebugActionType.Action, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void CreateSaplingChild()
        {
            var pawnAction = new DebugActionNode(
                "Select Mother...",
                DebugActionType.ToolMapForPawns,
                pawnAction: pawn =>
                {
                    if (!pawn.genes.HasActiveGene(InternalDefOf.VRE_SaplingBirth))
                    {
                        Log.Warning("Selected pawn does not have the Sapling Birth gene.");
                        return;
                    }
                    
                    var pos = new DebugActionNode(
                        "Select Location...",
                        DebugActionType.ToolMap,
                        action: () =>
                        {
                            var comp = new HediffComp_Saplingchild
                            {
                                parent = new HediffWithComps
                                {
                                    pawn = pawn
                                }
                            };
                            comp.CompPostMake();
        
                            var thing = ThingMaker.MakeThing(InternalDefOf.VRE_SaplingchildTree);
                            thing.stackCount = 1;
                            GenPlace.TryPlaceThing(thing, UI.MouseCell(), pawn.Map, ThingPlaceMode.Direct, out _);
        
                            var sapling = thing as Building_SaplingChild;
        
                            JobDriver_PlantSaplingchild.SetSaplingInfo(pawn, ref comp, ref sapling);
                        }
                    );
                    pos.Enter(null);
                }
            );
            pawnAction.Enter(null);
        }
    }
}

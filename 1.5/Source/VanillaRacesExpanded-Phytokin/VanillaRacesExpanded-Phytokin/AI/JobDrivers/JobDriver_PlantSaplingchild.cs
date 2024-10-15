using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace VanillaRacesExpandedPhytokin
{
    public class JobDriver_PlantSaplingchild : JobDriver
    {
        private IntVec3 TargetPosition => job.GetTarget(TargetIndex.A).Cell;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.A);
            Toil toil = Toils_General
                .Wait(600)
                .WithProgressBarToilDelay(TargetIndex.A)
                .FailOnDespawnedOrNull(TargetIndex.A)
                .FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
            yield return toil;
            
            Toil createSaplingChild = new Toil
            {
                initAction = delegate
                {
                    Thing thing = ThingMaker.MakeThing(InternalDefOf.VRE_SaplingchildTree);
                    thing.stackCount = 1;
                    GenPlace.TryPlaceThing(thing, TargetPosition, pawn.Map, ThingPlaceMode.Direct, out _);

                    Building_SaplingChild sapling = thing as Building_SaplingChild;

                    Hediff pregnancy = pawn.health?.hediffSet?.GetFirstHediffOfDef(InternalDefOf.VRE_Saplingchild);
                    HediffComp_Saplingchild comp = pregnancy?.TryGetComp<HediffComp_Saplingchild>();
                    if (comp != null)
                    {
                        comp.Miscarriage = false;
                        sapling.MotherGenes = comp.MotherGenes;
                        sapling.MotherXenotype = comp.MotherXenotype;
                        sapling.MotherXenotypeIcon = comp.MotherXenotypeIcon;
                        sapling.MotherXenotypeName = comp.MotherXenotypeName;
                        sapling.MotherUniqueXenotype = comp.MotherUniqueXenotype;
                        sapling.Mother = pawn;
                        sapling.SetFaction(pawn.Faction);
                        pawn.health.RemoveHediff(pregnancy);
                    }
                }
            };
            yield return createSaplingChild;
        }
    }
}
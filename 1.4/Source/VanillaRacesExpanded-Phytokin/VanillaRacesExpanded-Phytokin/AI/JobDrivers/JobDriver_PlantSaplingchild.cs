using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;

namespace VanillaRacesExpandedPhytokin
{
    public class JobDriver_PlantSaplingchild : JobDriver
    {


       

        private IntVec3 TargetPosition
        {
            get
            {
                return this.job.GetTarget(TargetIndex.A).Cell;
            }
        }

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            
           
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch).FailOnDespawnedOrNull(TargetIndex.A);
            Toil toil = Toils_General.Wait(600, TargetIndex.None);
            toil.WithProgressBarToilDelay(TargetIndex.A, false, -0.5f);
            toil.FailOnDespawnedOrNull(TargetIndex.A);
            toil.FailOnCannotTouch(TargetIndex.A, PathEndMode.Touch);
            yield return toil;
            Toil createSaplingChild = new Toil();
            createSaplingChild.initAction = delegate ()
            {
                Thing thing = ThingMaker.MakeThing(ThingDef.Named("Plant_TreeAnima"), null);
                thing.stackCount = 1;
                Thing t;
                GenPlace.TryPlaceThing(thing, TargetPosition, pawn.Map, ThingPlaceMode.Direct, out t, null, null, default(Rot4));

                Hediff pregnancy = pawn.health?.hediffSet?.GetFirstHediffOfDef(InternalDefOf.VRE_Saplingchild);
                if (pregnancy != null)
                {
                    pregnancy.TryGetComp<HediffComp_Saplingchild>().miscarriage = false;
                    pawn.health.RemoveHediff(pregnancy);
                }
                
                
            };
            yield return createSaplingChild;
            yield break;
        }

    }
}

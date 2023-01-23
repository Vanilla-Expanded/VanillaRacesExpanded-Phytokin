

using RimWorld;
using Verse;
using Verse.Sound;
using System;
using Verse.AI;
using System.Linq;
using System.Collections.Generic;

namespace VanillaRacesExpandedPhytokin
{
    public class HediffComp_Saplingchild : HediffComp
    {
       

        public HediffCompProperties_Saplingchild Props
        {
            get
            {
                return (HediffCompProperties_Saplingchild)this.props;
            }
        }

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();

            parent.pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(ThoughtDefOf.Miscarried);
        }

        public override IEnumerable<Gizmo> CompGetGizmos()
        {
            base.CompGetGizmos();
            yield return new Command_Action
            {
                defaultLabel = "Bla".Translate(),
                defaultDesc = "Bla".Translate(),
                
                action = delegate
                {
                    Log.Message("Bla");
                }
            };
        }


    }
}


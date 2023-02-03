using RimWorld;
using System.Collections.Generic;
using System;
using UnityEngine;
using VanillaRacesExpandedPhytokin;
using Verse;

namespace VanillaRacesExpandedPhytokin
{
    public class CompDryadCounter : ThingComp
    {
       

        public CompProperties_DryadCounter Props
        {
            get
            {
                return (CompProperties_DryadCounter)this.props;
            }
        }

       

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            if (this.parent.Faction == Faction.OfPlayer)
            {
                Current.Game.World.GetComponent<GauranlenGeneColonists_WorldComponent>().totalDryads++;
            }

        }

     

        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            if (this.parent.Faction == Faction.OfPlayer)
            {
                Current.Game.World.GetComponent<GauranlenGeneColonists_WorldComponent>().totalDryads--;
            }
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (!DebugSettings.ShowDevGizmos)
            {
                yield break;
            }
           
            yield return new Command_Action
            {
                defaultLabel = "DEV: Reset dryad counter",
                defaultDesc="Only use if the dryad system seems to be somehow failing. This will reset the current dryad counter to zero. I recommend deleting (or killing) the currently existing dryads before doing this",
                action = delegate
                {
                    Current.Game.World.GetComponent<GauranlenGeneColonists_WorldComponent>().totalDryads=0;
                },
               
            };
        }


    }
}

using RimWorld;
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


    }
}

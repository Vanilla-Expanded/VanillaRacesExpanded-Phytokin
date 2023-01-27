using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using RimWorld.Planet;

namespace VanillaRacesExpandedPhytokin
{
    public class GauranlenGeneColonists_WorldComponent : WorldComponent
    {

        public int tickCounter = 0;
        public int tickInterval = 1000;
        public int gauranlen_gene_colonists_inWorld_backup = 0;
        public int totalDryads = 0;

        public GauranlenGeneColonists_WorldComponent(World world) : base(world)
        {

        }

      

        public override void FinalizeInit()
        {
            
            StaticCollectionsClass.gauranlen_gene_colonists_inWorld = gauranlen_gene_colonists_inWorld_backup;


            base.FinalizeInit();

        }



        public override void ExposeData()
        {
            Scribe_Values.Look<int>(ref this.gauranlen_gene_colonists_inWorld_backup, "gauranlen_gene_colonists_inWorld_backup", 0, true);
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterGauranlen", 0, true);
            Scribe_Values.Look<int>(ref this.totalDryads, "totalDryads", 0, true);

            base.ExposeData();
        }


        public override void WorldComponentTick()
        {
            base.WorldComponentTick();


            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                gauranlen_gene_colonists_inWorld_backup = 0;
                foreach (Pawn pawn in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive_Colonists)
                {
                    if (pawn.genes?.HasGene(InternalDefOf.VRE_GauranlenAffinity) == true)
                    {
                        gauranlen_gene_colonists_inWorld_backup++;
                    }

                }
                StaticCollectionsClass.gauranlen_gene_colonists_inWorld = gauranlen_gene_colonists_inWorld_backup;



                tickCounter = 0;
            }



        }



    }


}

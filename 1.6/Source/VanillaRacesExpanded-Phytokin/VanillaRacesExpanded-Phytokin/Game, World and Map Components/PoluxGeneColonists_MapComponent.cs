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
    public class PoluxGeneColonists_MapComponent : MapComponent
    {

        public int tickCounter = 0;
        public int tickInterval = 1000;
        public int polux_gene_colonists_inMap_backup = 0;

        public PoluxGeneColonists_MapComponent(Map map) : base(map)
        {

        }

        public override void FinalizeInit()
        {
            
            StaticCollectionsClass.polux_gene_colonists_inMap = polux_gene_colonists_inMap_backup;


            base.FinalizeInit();

        }



        public override void ExposeData()
        {
            Scribe_Values.Look<int>(ref this.polux_gene_colonists_inMap_backup, "polux_gene_colonists_inMap_backup", 0, true);
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterSapling", 0, true);
            base.ExposeData();
        }


        public override void MapComponentTick()
        {


            tickCounter++;
            if ((tickCounter > tickInterval))
            {
                polux_gene_colonists_inMap_backup = 0;
                foreach (Pawn pawn in map.mapPawns.SpawnedPawnsInFaction(Faction.OfPlayer))
                {
                    if (pawn.genes?.HasActiveGene(InternalDefOf.VRE_PoluxAffinity) == true)
                    {
                        polux_gene_colonists_inMap_backup++;
                    }

                }
                StaticCollectionsClass.polux_gene_colonists_inMap = polux_gene_colonists_inMap_backup;



                tickCounter = 0;
            }



        }



    }


}

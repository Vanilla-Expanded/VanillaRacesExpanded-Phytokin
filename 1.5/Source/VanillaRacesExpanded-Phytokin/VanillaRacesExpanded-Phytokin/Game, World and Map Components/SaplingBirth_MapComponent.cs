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
    public class SaplingBirth_MapComponent : MapComponent
    {

        public int tickCounter = 0;
        public int tickInterval = 1000;
        public HashSet<Thing> sapling_birth_needed_backup = new HashSet<Thing>();

        public SaplingBirth_MapComponent(Map map) : base(map)
        {

        }

        public override void FinalizeInit()
        {
            if (map.IsPlayerHome)
            {
                StaticCollectionsClass.sapling_birth_needed = sapling_birth_needed_backup;

            }

            base.FinalizeInit();

        }

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref sapling_birth_needed_backup, "sapling_birth_needed_backup", LookMode.Reference);
            Scribe_Values.Look<int>(ref this.tickCounter, "tickCounterSapling", 0, true);
            base.ExposeData();
        }


        public override void MapComponentTick()
        {


            tickCounter++;
            if ((tickCounter > tickInterval))
            {

                sapling_birth_needed_backup = StaticCollectionsClass.sapling_birth_needed;



                tickCounter = 0;
            }



        }



    }


}

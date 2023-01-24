
using Verse;
using System;
using RimWorld;
using System.Collections.Generic;
using System.Linq;


namespace VanillaRacesExpandedPhytokin
{

    public static class StaticCollectionsClass
    {

        // A list of colonists needing sapling birth
        public static HashSet<Thing> sapling_birth_needed = new HashSet<Thing>();

        public static void AddColonistToSaplingBirthAlert(Thing thing)
        {

            if (!sapling_birth_needed.Contains(thing))
            {
                sapling_birth_needed.Add(thing);
            }
        }

        public static void RemoveColonistFromSaplingBirthAlert(Thing thing)
        {
            if (sapling_birth_needed.Contains(thing))
            {
                sapling_birth_needed.Remove(thing);
            }

        }

       
    }
}


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

        // Number of colonists with the Polux Affinity gene currently on the map 
        public static int polux_gene_colonists_inMap;
       


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

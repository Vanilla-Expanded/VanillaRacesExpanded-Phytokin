

using HarmonyLib;
using RimWorld;
using Verse;

namespace VanillaRacesExpandedPhytokin
{

    [HarmonyPatch(typeof(MeditationFocusTypeAvailabilityCache), "PawnCanUseInt")]
    public static class VanillaRacesExpandedPhytokin_MeditationFocusTypeAvailabilityCache_PawnCanUse_Patch
    {
        public static void Postfix(Pawn p, MeditationFocusDef type, ref bool __result)
        {
            if (ModLister.RoyaltyInstalled && type == MeditationFocusDefOf.Natural)
            {
                if (p.genes?.HasGene(InternalDefOf.VRE_AnimaAffinity)==true) {
                    __result = true;
                }

            }
           
        }
    }
}
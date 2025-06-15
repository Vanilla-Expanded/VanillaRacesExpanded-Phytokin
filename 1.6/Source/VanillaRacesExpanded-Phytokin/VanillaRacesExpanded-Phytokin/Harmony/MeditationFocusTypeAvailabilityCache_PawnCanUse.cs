

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
            if(type== MeditationFocusDefOf.Natural) {
                if (ModsConfig.RoyaltyActive)
                {
                    if (p.genes?.HasActiveGene(InternalDefOf.VRE_AnimaAffinity) == true)
                    {
                        __result = true;
                    }

                }
                if (ModsConfig.IdeologyActive)
                {
                    if (p.genes?.HasActiveGene(InternalDefOf.VRE_GauranlenAffinity) == true)
                    {
                        __result = true;
                    }

                }

            }
            

        }
    }
}
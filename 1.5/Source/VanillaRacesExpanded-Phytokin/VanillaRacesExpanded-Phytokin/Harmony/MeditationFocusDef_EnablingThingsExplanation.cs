
using HarmonyLib;
using RimWorld;
using Verse;

namespace VanillaRacesExpandedPhytokin
{
    [HarmonyPatch(typeof(MeditationFocusDef), nameof(MeditationFocusDef.EnablingThingsExplanation))]
    public static class VanillaRacesExpandedPhytokin_MeditationFocusDef_EnablingThingsExplanation_Patch
    {
        public static void Postfix(Pawn pawn, MeditationFocusDef __instance, ref string __result)
        {
            if (__instance == MeditationFocusDefOf.Natural)
            {
                if (ModsConfig.RoyaltyActive)
                {
                    if (pawn.genes?.HasGene(InternalDefOf.VRE_AnimaAffinity) == true)
                    {
                        __result += "\n  - " + "VRE_UnlockedByGeneAnima".Translate() + ".";
                    }

                }
                if (ModsConfig.IdeologyActive)
                {
                    if (pawn.genes?.HasGene(InternalDefOf.VRE_GauranlenAffinity) == true)
                    {
                        __result += "\n  - " + "VRE_UnlockedByGeneGau".Translate() + ".";
                    }

                }
            }
            


           
        }
    }
}
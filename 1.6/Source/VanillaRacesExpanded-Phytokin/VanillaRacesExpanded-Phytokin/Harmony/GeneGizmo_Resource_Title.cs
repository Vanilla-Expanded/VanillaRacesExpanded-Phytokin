using HarmonyLib;
using RimWorld;
using System.Reflection;
using Verse;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Verse.AI;
using RimWorld.Planet;



namespace VanillaRacesExpandedPhytokin
{


    [HarmonyPatch(typeof(GeneGizmo_Resource))]
    [HarmonyPatch("Title", MethodType.Getter)]

    static class VanillaRacesExpandedPhytokin_GeneGizmo_Resource_Title_Patch
    {


        [HarmonyPrefix]
        public static void ChangeBarColour(Gene ___gene, ref Texture2D ___barTex, ref Texture2D ___barHighlightTex)
        {

            if (___gene.def == InternalDefOf.VRE_PoluxAffinity)
            {
                ___barTex = GraphicsCache.PolluxBarTex;
                ___barHighlightTex = GraphicsCache.PolluxBarTexHighlight;
                return;
            }
            



        }


    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using static RimWorld.BaseGen.SymbolStack;
using Verse.AI;

namespace VanillaRacesExpandedPhytokin
{




    [HarmonyPatch(typeof(JobDriver_PlantSow))]
    [HarmonyPatch("MakeNewToils")]

    public static class VanillaRacesExpandedPhytokin_JobDriver_PlantSow_MakeNewToils_Patch
    {

        public static IEnumerable<Toil> Postfix(IEnumerable<Toil> values, JobDriver_PlantSow __instance)
        {
            List<Toil> resultingList = values.ToList();
            Toil toil;

            if (__instance.pawn?.genes?.HasGene(InternalDefOf.VRE_GreenThumb)==true)
            {
                toil = new Toil
                {
                    initAction = delegate
                    {
                        __instance.pawn?.needs?.mood?.thoughts?.memories?.TryGainMemory(InternalDefOf.VRE_GreenThumbHappy);
                    }
                };
                resultingList.Add(toil);
            }



            return resultingList;




        }

      





    }

}

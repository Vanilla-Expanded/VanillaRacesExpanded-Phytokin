﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using HarmonyLib;

namespace VanillaRacesExpandedPhytokin
{
	[HarmonyPatch(typeof(Hediff_Pregnant), "TickInterval")]
	public static class VanillaRacesExpandedPhytokin_Hediff_Pregnant_TickInterval
    {
		[HarmonyPrefix]
		public static bool Prefix(Hediff_Pregnant __instance)
		{
			if ((Find.TickManager.TicksAbs % 200 == 0) && (__instance?.pawn?.genes?.HasActiveGene(InternalDefOf.VRE_SaplingBirth) ?? false))
			{
				try
				{
					
					Hediff hediff = HediffMaker.MakeHediff(InternalDefOf.VRE_TempSterile, __instance.pawn);
					__instance.pawn.health.AddHediff(hediff);

					Hediff hediff2 = HediffMaker.MakeHediff(InternalDefOf.VRE_Saplingchild, __instance.pawn);
					__instance.pawn.health.AddHediff(hediff2);

					ChoiceLetter letter = LetterMaker.MakeLetter("VRE_SaplingBirthReady".Translate(__instance.pawn.LabelShort), "VRE_SaplingBirthReadyDesc".Translate(__instance.pawn.LabelShort), LetterDefOf.PositiveEvent);
					Find.LetterStack.ReceiveLetter(letter);
					__instance.pawn.health.RemoveHediff(__instance);
				}
				catch (Exception ex)
				{
					Log.Message("Error Suppressed: " + ex.ToString());
				}
				return false;
			}
			return true;
		}

		
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaRacesExpandedPhytokin
{
    [DefOf]
    public static class InternalDefOf
    {
        public static GeneDef VRE_GreenThumb;
        public static GeneDef VRE_ProgressiveAttunement;
        public static GeneDef VRE_SaplingBirth;
        public static GeneDef VRE_PoluxAffinity;
        [MayRequireRoyalty]
        public static GeneDef VRE_AnimaAffinity;
        [MayRequireIdeology]
        public static GeneDef VRE_GauranlenAffinity;

        public static ThoughtDef VRE_GreenThumbHappy;
        [MayRequireRoyalty]
        public static ThoughtDef VRE_AnimaSong;

        public static HediffDef VRE_TempSterile;
        public static HediffDef VRE_Saplingchild;

        public static JobDef VRE_PlantSaplingchild;
        public static JobDef VRE_ConsumeWastepack;

        public static ThingDef VRE_SaplingchildTree;
        public static ThingDef VRE_PoluxBush;
        [MayRequireIdeology]
        public static PawnKindDef VRE_CompanionDryad;

        public static EffecterDef EatVegetarian;

        public static SoundDef Meal_Eat;
        public static SoundDef VRE_AnimaSongSound;

        public static XenotypeDef VRE_Poluxkin;
        [MayRequireRoyalty]
        public static XenotypeDef VRE_Animakin;
        [MayRequireIdeology]
        public static XenotypeDef VRE_Gauranlenkin;






    }
}
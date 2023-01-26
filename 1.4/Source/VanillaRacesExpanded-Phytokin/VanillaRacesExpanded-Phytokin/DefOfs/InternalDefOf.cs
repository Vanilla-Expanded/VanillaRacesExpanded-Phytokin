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
        public static ThoughtDef VRE_GreenThumbHappy;
        public static HediffDef VRE_TempSterile;
        public static HediffDef VRE_Saplingchild;
        public static JobDef VRE_PlantSaplingchild;
        public static ThingDef VRE_SaplingchildTree;

        [MayRequireRoyalty]
        public static GeneDef VRE_AnimaAffinity;
        [MayRequireRoyalty]
        public static ThoughtDef VRE_AnimaSong;

        [MayRequireIdeology]
        public static GeneDef VRE_GauranlenAffinity;


    }
}
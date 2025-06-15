using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using HarmonyLib;

namespace VanillaRacesExpandedPhytokin
{

    public class HarmonyInstance : Mod
    {
        public HarmonyInstance(ModContentPack content) : base(content)
        {
            harmonyInstance = new Harmony("OskarPotocki.VREPhytokin");
            harmonyInstance.PatchAll();
        }

        public static Harmony harmonyInstance;
    }

}

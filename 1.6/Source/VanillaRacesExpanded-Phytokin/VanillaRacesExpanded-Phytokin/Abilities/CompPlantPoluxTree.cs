using RimWorld;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VanillaRacesExpandedPhytokin;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace VanillaRacesExpandedPhytokin
{
    public class CompPlantPoluxTree : CompAbilityEffect
    {

        private new CompProperties_PlantPoluxTree Props => (CompProperties_PlantPoluxTree)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            base.Apply(target, dest);

            Thing thing = ThingMaker.MakeThing(InternalDefOf.VRE_PoluxBush, null);
            thing.stackCount = 1;
            Thing t;
            GenPlace.TryPlaceThing(thing, target.Cell, this.parent.pawn.Map, ThingPlaceMode.Direct, out t, null, null, default(Rot4));

        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            
            if (target.Cell.GetFertility(this.parent.pawn.Map)<0.05)
            {
                if (throwMessages)
                {
                    Messages.Message("VRE_CanOnlyBePlantedOnFertile".Translate(), null, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }

     
            return base.Valid(target, throwMessages);
        }







    }
}
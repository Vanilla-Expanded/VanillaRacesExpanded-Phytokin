using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using VanillaRacesExpandedPhytokin;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace VanillaRacesExpandedPhytokin
{
    public class CompAnimaSong : CompAbilityEffect
    {

        private new CompProperties_AnimaSong Props => (CompProperties_AnimaSong)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            
            base.Apply(target, dest);
            Plant plant = target.Thing as Plant;

            if (plant.Map == null)
            {
                return;
            }
            
            foreach (Pawn item in plant.Map.mapPawns.AllPawnsSpawned)
            {
                item.needs?.mood?.thoughts.memories.TryGainMemory(InternalDefOf.VRE_AnimaSong);
            }
            InternalDefOf.VRE_AnimaSongSound.PlayOneShotOnCamera(plant.Map);


        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            Plant plant = target.Thing as Plant;
            if (plant == null)
            {
                return base.Valid(target, throwMessages);
            }

            if (plant.def != ThingDefOf.Plant_TreeAnima)
            {
                if (throwMessages)
                {
                    Messages.Message("VRE_CanOnlyBeUsedOnAnimaTree".Translate(), plant, MessageTypeDefOf.RejectInput, historical: false);
                }
                return false;
            }

           

            return base.Valid(target, throwMessages);
        }







    }
}
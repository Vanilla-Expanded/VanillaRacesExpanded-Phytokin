

using RimWorld;
using Verse;
using Verse.Sound;
using System;
using Verse.AI;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace VanillaRacesExpandedPhytokin
{
    public class HediffComp_Saplingchild : HediffComp
    {
        public bool miscarriage = true;

        public HediffCompProperties_Saplingchild Props
        {
            get
            {
                return (HediffCompProperties_Saplingchild)this.props;
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look(ref this.miscarriage, nameof(this.miscarriage), true);
        }

        public override void CompPostMake()
        {
            if (parent.pawn.Faction == Faction.OfPlayer) { StaticCollectionsClass.AddColonistToSaplingBirthAlert(parent.pawn); }
            
            base.CompPostMake();
        }

        public override void CompPostPostRemoved()
        {
            StaticCollectionsClass.RemoveColonistFromSaplingBirthAlert(parent.pawn);
            base.CompPostPostRemoved();
            if (miscarriage) {
                parent.pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(ThoughtDefOf.Miscarried);
            }
            
        }

        public override IEnumerable<Gizmo> CompGetGizmos()
        {
            base.CompGetGizmos();
            yield return new Command_Target
            {
                defaultLabel = "VRE_PlantSaplingchild".Translate(),
                defaultDesc = "VRE_PlantSaplingchildDesc".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Abilities/PlantSaplingchild", true),
                targetingParams = new TargetingParameters { canTargetLocations = true },
                action = delegate (LocalTargetInfo target)
                {
                    TryCreateSaplingChild(target);
                }
            };



        }

        public void TryCreateSaplingChild(LocalTargetInfo target)
        {
            if (target.Cell.GetTerrain(parent.pawn.Map).IsSoil)
            {
                Job job = new Job(InternalDefOf.VRE_PlantSaplingchild, target);
                
                parent.pawn.jobs.TryTakeOrderedJob(job, JobTag.Misc);

            }
            else
            {
                Messages.Message("VRE_SaplingNeedsSoil".Translate(), parent.pawn, MessageTypeDefOf.NegativeEvent, true);

            }

        }


    }
}


using RimWorld;
using Verse;
using Verse.AI;
using System.Collections.Generic;
using UnityEngine;

namespace VanillaRacesExpandedPhytokin
{
    public class HediffComp_Saplingchild : HediffComp
    {
        public bool Miscarriage = true;
        public List<GeneDef> MotherGenes = new List<GeneDef>();
        public XenotypeDef MotherXenotype;
        public XenotypeIconDef MotherXenotypeIcon;
        public string MotherXenotypeName;
        public bool MotherUniqueXenotype;

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look(ref Miscarriage, nameof(Miscarriage), true);
            Scribe_Collections.Look(ref MotherGenes, nameof(MotherGenes), LookMode.Def);
            Scribe_Defs.Look(ref MotherXenotype, nameof(MotherXenotype));
        }

        public override void CompPostMake()
        {
            if (parent.pawn.Faction == Faction.OfPlayer)
            {
                StaticCollectionsClass.AddColonistToSaplingBirthAlert(parent.pawn);
            }

            foreach (Gene gene in parent.pawn.genes.Endogenes)
            {
                MotherGenes.Add(gene.def);
            }

            MotherXenotype = parent.pawn.genes.Xenotype;
            MotherUniqueXenotype = parent.pawn.genes.UniqueXenotype;

            if (MotherUniqueXenotype)
            {
                MotherXenotypeIcon = parent.pawn.genes.iconDef;
                MotherXenotypeName = parent.pawn.genes.xenotypeName;
            }
            else
            {
                MotherXenotypeIcon = null;
                MotherXenotypeName = parent.pawn.genes.Xenotype.LabelCap;
            }

            base.CompPostMake();
        }

        public override void CompPostPostRemoved()
        {
            StaticCollectionsClass.RemoveColonistFromSaplingBirthAlert(parent.pawn);
            base.CompPostPostRemoved();
            if (Miscarriage)
            {
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
                icon = ContentFinder<Texture2D>.Get("UI/Abilities/PlantSaplingchild"),
                targetingParams = new TargetingParameters { canTargetLocations = true },
                action = TryCreateSaplingChild
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
                Messages.Message("VRE_SaplingNeedsSoil".Translate(), parent.pawn, MessageTypeDefOf.NegativeEvent);
            }
        }
    }
}
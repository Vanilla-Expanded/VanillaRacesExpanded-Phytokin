using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;
using VanillaRacesExpandedPhytokin;
using System.Net.NetworkInformation;

namespace VanillaRacesExpandedPhytokin
{
    public class JobGiver_ConsumeWastepack : ThinkNode_JobGiver
    {
        public override float GetPriority(Pawn pawn)
        {
            if (!ModsConfig.BiotechActive)
            {
                return 0f;
            }
            Pawn_GeneTracker genes = pawn.genes;
            if (genes != null && genes.GetFirstGeneOfType<Gene_Resource_Wastepacks>() == null)
            {
                return 0f;
            }
            return 9.1f;
        }
        protected override Job TryGiveJob(Pawn pawn)
        {
            if (!ModsConfig.BiotechActive) { return null; }
            var gene = pawn.genes.GetFirstGeneOfType<Gene_Resource_Wastepacks>();
            if (gene == null) { return null; }
            if (!gene.ShouldConsumeNow()) { return null; }
           
            Thing wastepack = GetWastepack(pawn); 
            if (wastepack == null) { return null; }
            Job job = JobMaker.MakeJob(InternalDefOf.VRE_ConsumeWastepack, wastepack);
            job.count = 1;
            return job;
        }
        public static Thing GetWastepack(Pawn pawn)
        {
            
            return GenClosest.ClosestThing_Global_Reachable(pawn.Position, pawn.Map, pawn.Map.listerThings.ThingsOfDef(ThingDefOf.Wastepack), PathEndMode.Touch, TraverseParms.For(pawn), validator: (Thing t) =>
            {
                return pawn.CanReserve(t) && !t.IsForbidden(pawn);
            });
        }
    }
}

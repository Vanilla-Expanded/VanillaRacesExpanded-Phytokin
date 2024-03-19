using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace VanillaRacesExpandedPhytokin
{
    public class Alert_SaplingchildNeedsPlanting : Alert
    {
        public Alert_SaplingchildNeedsPlanting()
        {
            defaultPriority = AlertPriority.High;
            defaultLabel = "VRE_SaplingchildNeedsToBePlanted".Translate();
            
        }

        public override AlertReport GetReport()
        {
           
            var map = Find.CurrentMap;
            if (map == null)
            {
                return AlertReport.Inactive;
            }

            return AlertReport.CulpritsAre(StaticCollectionsClass.sapling_birth_needed.ToList());
        }

        public override TaggedString GetExplanation()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Thing thing in StaticCollectionsClass.sapling_birth_needed)
            {
                Pawn pawn = thing as Pawn;
                stringBuilder.AppendLine("  - " + pawn.NameShortColored.Resolve());
            }
            return "VRE_SaplingchildNeedsToBePlantedDesc".Translate(stringBuilder.ToString());
        }
    }
}

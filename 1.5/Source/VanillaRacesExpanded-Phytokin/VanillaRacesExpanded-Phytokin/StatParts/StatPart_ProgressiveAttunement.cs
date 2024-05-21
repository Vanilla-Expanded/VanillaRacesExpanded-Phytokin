using RimWorld;
using UnityEngine;
using Verse;

namespace VanillaRacesExpandedPhytokin
{

	public class StatPart_ProgressiveAttunement : StatPart
	{
		public override void TransformValue(StatRequest req, ref float val)
		{
			float offset = 0f;
			if (HasRequiredGene(req.Thing))
            {
				offset = OffSetByAge(req.Thing);
            }		
			val += offset;			
		}

		public bool HasRequiredGene(Thing thing)
		{
			Pawn pawn = thing as Pawn;
			if (pawn?.genes?.HasGene(InternalDefOf.VRE_ProgressiveAttunement) == true)
			{
				return true;
			}
			return false;
		}

		public float OffSetByAge(Thing thing)
        {
			float offset = 0f;
			Pawn pawn = thing as Pawn;
            if (pawn != null) {
				offset = pawn.ageTracker.AgeBiologicalYears * 0.01f;
				if (offset > 10)
				{
					offset = 10f;
				}
			}
			return offset;
			
		}

		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing && HasRequiredGene(req.Thing))
			{
				return "VRE_ProgressiveAttunementOffset".Translate() + (": +" + OffSetByAge(req.Thing));
			}
			return null;
		}

		
	}
}

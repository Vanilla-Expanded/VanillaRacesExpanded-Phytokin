
using System.Collections.Generic;
using VanillaRacesExpandedPhytokin;
using Verse;
using Verse.AI;

namespace VanillaRacesExpandedPhytokin
{
    public class ThinkNode_CleaningDryad : ThinkNode_Conditional
    {


        protected override bool Satisfied(Pawn pawn)
        {
            if (pawn.kindDef == InternalDefOf.VRE_CompanionDryad)
            {
                return true;
            }
            return false;
        }
    }
}


using RimWorld;
using VanillaRacesExpandedPhytokin;
using Verse;
namespace VanillaRacesExpandedPhytokin
{
    public class ThoughtWorker_Wastepacks : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
           
            if (pawn.genes?.HasActiveGene(InternalDefOf.VRE_PoluxAffinity) != true)

            {
                return false;               
            }
         
            var gene = pawn.genes.GetFirstGeneOfType<Gene_Resource_Wastepacks>();
            if (gene == null) { return false; }
            if (gene.Value!=0f) { return false; }

            return ThoughtState.ActiveAtStage(0);
            
          
        }
    }
}
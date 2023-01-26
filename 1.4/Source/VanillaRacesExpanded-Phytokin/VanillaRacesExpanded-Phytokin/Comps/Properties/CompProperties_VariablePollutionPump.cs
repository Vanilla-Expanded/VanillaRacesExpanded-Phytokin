
using RimWorld;
using Verse;
namespace VanillaRacesExpandedPhytokin
{
    public class CompProperties_VariablePollutionPump : CompProperties
    {
        public float radius = 3.9f;

        public int intervalTicks = 60000;

        public EffecterDef pumpEffecterDef;

        public int pumpsPerWastepack = 3;

        public bool disabledByArtificialBuildings;

        public CompProperties_VariablePollutionPump()
        {
            compClass = typeof(CompVariablePollutionPump);
        }
    }
}

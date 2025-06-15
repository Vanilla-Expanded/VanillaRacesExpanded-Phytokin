using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
namespace VanillaRacesExpandedPhytokin
{
    public class PlaceWorker_VariablePollutionPumpArtificialBuildings : PlaceWorker
    {
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
        {
            if (!ModsConfig.BiotechActive)
            {
                return;
            }
            CompProperties_VariablePollutionPump compProperties = def.GetCompProperties<CompProperties_VariablePollutionPump>();
            if (compProperties == null)
            {
                return;
            }
            List<Thing> forCell = Find.CurrentMap.listerArtificialBuildingsForMeditation.GetForCell(center, compProperties.radius+ StaticCollectionsClass.polux_gene_colonists_inMap * 2);
            GenDraw.DrawRadiusRing(center, compProperties.radius + StaticCollectionsClass.polux_gene_colonists_inMap*2, Color.white);
            if (forCell.NullOrEmpty())
            {
                return;
            }
            int num = 0;
            foreach (Thing item in forCell)
            {
                if (num++ > 10)
                {
                    break;
                }
                GenDraw.DrawLineBetween(GenThing.TrueCenter(center, Rot4.North, def.size, def.Altitude), item.TrueCenter(), SimpleColor.Red);
            }
        }
    }
}

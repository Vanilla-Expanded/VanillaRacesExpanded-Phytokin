using System;
using UnityEngine;
using Verse;

namespace VanillaRacesExpandedPhytokin
{
    [StaticConstructorOnStartup]
    public static class GraphicsCache
    {

        public static readonly Graphic graphicMaturePlant = (Graphic_Single)GraphicDatabase.Get<Graphic_Single>("Things/Plant/SaplingChild", ShaderDatabase.CutoutComplex, Vector2.one, Color.white);
      
    }
}

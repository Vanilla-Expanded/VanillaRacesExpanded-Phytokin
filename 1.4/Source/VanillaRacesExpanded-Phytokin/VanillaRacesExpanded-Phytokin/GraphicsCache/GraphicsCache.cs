using System;
using UnityEngine;
using Verse;

namespace VanillaRacesExpandedPhytokin
{
    [StaticConstructorOnStartup]
    public static class GraphicsCache
    {

        public static readonly Graphic graphicMaturePlant = (Graphic_Single)GraphicDatabase.Get<Graphic_Single>("Things/Plant/SaplingChild", ShaderDatabase.CutoutComplex, Vector2.one, Color.white);

        public static readonly Texture2D poluxAffinityBarTex = SolidColorMaterials.NewSolidColorTexture(new ColorInt(128, 108, 131).ToColor);
        public static readonly Texture2D poluxAffinityBarHighlightTex = SolidColorMaterials.NewSolidColorTexture(new ColorInt(150, 127, 153).ToColor);

    }
}

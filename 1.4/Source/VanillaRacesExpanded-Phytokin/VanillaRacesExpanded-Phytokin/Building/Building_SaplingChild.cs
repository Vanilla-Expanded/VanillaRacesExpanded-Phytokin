using Verse;
using System;
using RimWorld;
using System.Collections.Generic;

namespace VanillaRacesExpandedPhytokin
{
    public class Building_SaplingChild: Building
    {

        public List<GeneDef> motherGenes = new List<GeneDef>();
        public XenotypeDef motherXenotype;
        public int tickCounter = 0;
        public int ticksToBirth = 1800000; // 30 days
        public bool successfulBirth = false;
        public Pawn mother;

        public override void ExposeData()
        {
            base.ExposeData();
           
            Scribe_Collections.Look(ref this.motherGenes, nameof(this.motherGenes), LookMode.Def);
            Scribe_Defs.Look(ref this.motherXenotype, nameof(this.motherXenotype));
            Scribe_Values.Look(ref this.tickCounter, nameof(this.tickCounter));
            Scribe_Values.Look(ref this.successfulBirth, nameof(this.successfulBirth));
            Scribe_References.Look(ref this.mother, nameof(this.mother));
        }

        public override string GetInspectString()
        {        
            string text = base.GetInspectString();
            if (motherXenotype != null)
            {
                if (text.Length != 0)
                {
                    text += "\n";
                }
                text += "VRE_MotherXenotype".Translate(motherXenotype.LabelCap);
            }
            return text;
        }

        public override void Tick()
        {
            base.Tick();
            tickCounter++;
            if ((tickCounter > ticksToBirth))
            {

               

              
            }

        }

        public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
        {
            base.Destroy(mode);
            if (!successfulBirth) {
                mother?.needs?.mood?.thoughts?.memories?.TryGainMemory(ThoughtDefOf.Stillbirth);

            }
            
        }


    }
}

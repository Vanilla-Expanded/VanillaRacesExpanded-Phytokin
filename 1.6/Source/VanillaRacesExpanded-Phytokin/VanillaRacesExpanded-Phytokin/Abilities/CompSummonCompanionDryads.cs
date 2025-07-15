﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VanillaRacesExpandedPhytokin;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace VanillaRacesExpandedPhytokin
{
    public class CompSummonCompanionDryads : CompAbilityEffect
    {

       

        private new CompProperties_SummonCompanionDryads Props => (CompProperties_SummonCompanionDryads)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            base.Apply(target, dest);

           

            Pawn pawnCreated = PawnGenerator.GeneratePawn(InternalDefOf.VRE_CompanionDryad, parent.pawn.Faction);
            Pawn pawnCreated2 = PawnGenerator.GeneratePawn(InternalDefOf.VRE_CompanionDryad, parent.pawn.Faction);
            Pawn pawn1 = (Pawn)GenSpawn.Spawn(pawnCreated, target.Cell, parent.pawn.Map, Rot4.South);
            Pawn pawn2 = (Pawn)GenSpawn.Spawn(pawnCreated2, target.Cell, parent.pawn.Map, Rot4.South);

           

        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {

            
            int? gauGeneHolders = PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive_OfPlayerFaction.Where(x => x.genes?.HasActiveGene(InternalDefOf.VRE_GauranlenAffinity)==true)?.Count();

            int? dryadNumber = PawnsFinder.AllMapsCaravansAndTravellingTransporters_Alive_OfPlayerFaction.Where(x => x.kindDef == InternalDefOf.VRE_CompanionDryad)?.Count();
          

            if (dryadNumber >= gauGeneHolders*2)
            {
            
                    if (throwMessages)
                    {
                        Messages.Message("VRE_TwoPreviousDryadsMustBeDead".Translate(), parent.pawn, MessageTypeDefOf.RejectInput, historical: false);
                    }
                    return false;
                
                
            }
            


            return base.Valid(target, throwMessages);
        }

      






    }
}
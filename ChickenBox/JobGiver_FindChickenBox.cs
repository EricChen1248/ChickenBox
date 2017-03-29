using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.AI;

namespace ChickenBox
{
    public class JobGiver_FindChickenBox : ThinkNode_JobGiver
    {
        private const int MaxSearchDistance = 30;
        
        protected override Job TryGiveJob(Pawn pawn)
        {
            
            var comp = pawn.TryGetComp<CompEggLayer>();
            if (comp == null || !comp.CanLayNow)
                return null;

            var thingReq = new ThingRequest {singleDef = ThingDef.Named("ChickenNest")};
            var chickenNest =
                (Building_Bed)
                GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, thingReq, PathEndMode.OnCell,
                    TraverseParms.For(pawn, Danger.None, TraverseMode.ByPawn, false), maxDistance: MaxSearchDistance);
            return new Job(JobDefOf.LayEgg, (LocalTargetInfo)chickenNest.Position);
        }
    }
}

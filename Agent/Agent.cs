namespace IntelligentVacuum.Agent
{
    using System;
    using System.Collections.Generic;
    using Environments;

    public class Agent
    {
        private readonly Dictionary<int, AgentAction> direction= new Dictionary<int, AgentAction>()
        {{(int)AgentAction.MOVE_RIGHT,AgentAction.MOVE_RIGHT}, 
        {-(int)AgentAction.MOVE_RIGHT,AgentAction.MOVE_LEFT}};

        private Room prevRoom=new Room(-1,-1);

        private int horzDir=(int)AgentAction.MOVE_RIGHT;
        private int prevDir=(int)AgentAction.NONE;
        public Agent()
        {
        }

        public AgentAction DecideAction(Room room)
        {
            AgentAction result=AgentAction.NONE;
            if (room.IsDirty)
            {
                result=AgentAction.CLEAN;
            }
             else if (prevDir == (int)AgentAction.MOVE_DOWN)
            {
                // see if changed rooms-if not then do nothing
               if (prevRoom.XAxis != room.XAxis || prevRoom.YAxis != room.YAxis)
               {
                //change direction
                horzDir=-horzDir;
            
                // save previous room, previous direction
                prevRoom=room;
                result=direction[horzDir];
                prevDir=(int)result;
               }
            }
            else if (prevRoom.XAxis != room.XAxis || prevRoom.YAxis != room.YAxis)
            {
                // different room-keep moving same direction
                prevRoom=room;
                result=direction[horzDir];
            }
            else
            {
                // same room, something bad happen, move different direction
                prevRoom=room;
                result=AgentAction.MOVE_DOWN;
                prevDir=(int)result;
            }
            return result;
        }
    }
}
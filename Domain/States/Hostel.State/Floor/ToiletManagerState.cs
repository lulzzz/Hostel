﻿using Hostel.Event.Created;
using Hostel.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hostel.State.Floor
{
    public class ToiletManagerState : Message, IState<ToiletManagerState>
    {
        public string FloorId { get; }
        public string Tag { get; }
        public IEnumerable<ToiletSpec> Toilets { get; }
        public ToiletManagerState(string floor, IEnumerable<ToiletSpec> toilets, string tag)
        {
            FloorId = floor;
            Tag = tag;
            Toilets = toilets;
        }
        public ToiletManagerState Update(IEvent evnt)
        {
            switch(evnt)
            {
                case CreatedToilet createdToilet:
                    {
                        var toilet = createdToilet.Toilet;
                        var toilets = Toilets.Where(x => x.Tag != toilet.Tag).ToList();
                        toilets.Add(toilet);
                        return new ToiletManagerState(FloorId, toilets, toilet.Tag);
                    }
                default: return this;
            }
        }
    }
}

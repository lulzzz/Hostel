﻿using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hostel.State.Floor.Units
{
    public class ToiletState : Message, IState<ToiletState>
    {
        public ToiletState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}

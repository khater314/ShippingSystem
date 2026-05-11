using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Contracts.Shipment
{
    public interface ITrackingNumberCreator
    {
        public string Create();
    }
}

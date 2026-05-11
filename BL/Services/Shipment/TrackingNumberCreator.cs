using BL.Contracts.Shipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Shipment
{
    public class TrackingNumberCreator : ITrackingNumberCreator
    {
        public string Create()
        {
            // Implementation for creating a tracking number
            return Guid.NewGuid().ToString();
        }
    }
}

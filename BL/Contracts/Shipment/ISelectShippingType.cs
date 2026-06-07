using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Contracts.Shipment
{
    public interface ISelectShippingType
    {
        Task<Guid> GetShippingTypeId(Guid senderCityId, Guid receiverCityId);
    }
}

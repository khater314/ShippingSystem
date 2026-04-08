using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Enums
{
    public enum ShipmentStatus
    {
        Pending = 1,    // قيد الانتظار
        InTransit = 2,  // في الطريق
        Delivered = 3,  // تم التسليم
        Cancelled = 4,  // ملغية
        Returned = 5    // مرتجع
    }
}

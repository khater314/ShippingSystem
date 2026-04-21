using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains.Enums
{
    // Don't Forget string.Replace('_', ' ')
    public enum ShipmentStatus
    {
        [Display(Name = "Pending")]
        Pending = 1, 
        
        [Display(Name = "In Transit")]
        In_Transit = 2, 

        [Display(Name = "Delivered")]
        Delivered = 3, 

        [Display(Name = "Cancelled")]
        Cancelled = 4, 
        
        [Display(Name = "Returned")]
        Returned = 5   
    }
    public enum ShipmentStatusAr
    {
        [Display(Name = "قيد الانتظار")]
        Pending = 1,  
        
        [Display(Name = "في الطريق")]
        In_Transit = 2, 
        
        [Display(Name = "تم التسليم")]
        Delivered = 3, 

        [Display(Name = "ملغية")]
        Cancelled = 4, 
        
        [Display(Name = "مرتجع")]
        Returned = 5    
    }
}

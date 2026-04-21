using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains.Enums
{
    // Don't Forget string.Replace('_', ' ')
    public enum ContactType : byte
    {
        [Display(Name = "Sender And Receiver")]
        Sender_And_Receiver = 0,

        [Display(Name = "Sender Only")]
        Sender_Only = 1,

        [Display(Name = "Receiver Only")]
        Receiver_Only = 2,
    }

    public enum ContactTypeAr : byte
    {
        [Display(Name = "مرسل ومستلم")]
        Sender_And_Receiver = 0,

        [Display(Name = "مرسل فقط")]
        Sender_Only = 1,

        [Display(Name = "مستلم فقط")]
        Receiver_Only = 2,
    }
}

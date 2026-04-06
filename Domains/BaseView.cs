using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domains
{
    public abstract class BaseView
    {
        [Key]
        public Guid Id { get; set; }

        public int CurrentState { get; set; }

    }
}

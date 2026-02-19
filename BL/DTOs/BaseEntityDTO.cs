using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BL.DTOs;

public abstract class BaseEntityDTO 
{
    [Key]
    public Guid Id { get; set; }

}

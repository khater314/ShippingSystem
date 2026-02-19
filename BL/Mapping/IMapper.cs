using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Mapping
{
    public interface IMapper 
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}

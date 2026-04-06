using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace BL.Mapping
{
    // Adapter that delegates your BL.Mapping.IMapper calls to AutoMapper's IMapper
    public class AutoMapperAdapter(AutoMapper.IMapper inner) : IMapper
    {
        private readonly AutoMapper.IMapper _inner = inner;

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _inner.Map<TSource, TDestination>(source);
        }
    }
}

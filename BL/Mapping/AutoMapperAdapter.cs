using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace BL.Mapping
{
    // Adapter that delegates your BL.Mapping.IMapper calls to AutoMapper's IMapper
    public class AutoMapperAdapter : IMapper
    {
        private readonly AutoMapper.IMapper _inner;

        public AutoMapperAdapter(AutoMapper.IMapper inner)
        {
            _inner = inner;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _inner.Map<TSource, TDestination>(source);
        }
    }
}

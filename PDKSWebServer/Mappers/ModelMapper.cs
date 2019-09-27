using AutoMapper;
using PDKSWebServer.Dtos;
using PDKSWebServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Mappers
{
    internal class ModelMapper
    {
        private class TicksToDateTimeConverter : ITypeConverter<long, DateTime>
        {
            public DateTime Convert(long source, DateTime destination, ResolutionContext context)
            {
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddMilliseconds(source).ToLocalTime();
                return dtDateTime;
            }
        }

        private class DateTimeToTicksConverter : ITypeConverter<DateTime, long>
        {
            public long Convert(DateTime source, long destination, ResolutionContext context)
            {
                return new DateTimeOffset(source).ToUnixTimeMilliseconds();
            }
        }

        private static ModelMapper _modelMapper;
        internal static ModelMapper GetMapper =>
            _modelMapper ?? (_modelMapper = new ModelMapper());

        private readonly IMapper _mapper;

        private ModelMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
            });

            _mapper = new Mapper(config);
        }

        internal TDest Map<TSource, TDest>(TSource source)
        {
            if (source == null)
                return default;
            return _mapper.Map<TSource, TDest>(source);
        }

        internal List<TDest> MapList<TSource, TDest>(List<TSource> source)
        {
            if (source == null)
                return null;

            List<TDest> res = new List<TDest>();

            foreach (var obj in source)
            {
                res.Add(Map<TSource, TDest>(obj));
            }
            return res;
        }
    }
}

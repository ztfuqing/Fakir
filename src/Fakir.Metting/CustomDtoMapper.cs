using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Fakir.Metting.Domain;
using Fakir.Metting.Dtos.Agendas;

namespace Fakir.Metting
{
    internal static class CustomDtoMapper
    {
        private static volatile bool _mappedBefore;
        private static readonly object SyncObj = new object();

        public static void CreateMappings()
        {
            lock (SyncObj)
            {
                if (_mappedBefore)
                {
                    return;
                }

                CreateMappingsInternal();

                _mappedBefore = true;
            }
        }

        private static void CreateMappingsInternal()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Agenda, AgendaListDto>()
                    .ForMember(a => a.ConferenceName, b => b.MapFrom(a=>a.Conference.Name));
            });
        }
    }
}

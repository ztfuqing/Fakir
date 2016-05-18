using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Fakir.Metting.Dtos
{
    public class GetConferenceForEditOutput : IOutputDto
    {
        public ConferenceEditDto Conference { get; set; }

        public ConferenceAgendaDto[] Agendas { get; set; }

        public ConferenceUserDto[] Users { get; set; }
    }
}

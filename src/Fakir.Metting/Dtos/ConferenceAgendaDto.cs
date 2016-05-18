using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Fakir.Metting.Domain;

namespace Fakir.Metting.Dtos
{
    [AutoMap(typeof(Agenda))]
    public class ConferenceAgendaDto : IDto
    {
        public string Subject { get; set; }

        public string Spokesman { get; set; }

        public DateTime SpeakTime { get; set; }

        public string SpeakLength { get; set; }

        public string Department { get; set; }

        public int Order { get; set; }
    }
}
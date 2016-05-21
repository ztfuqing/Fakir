using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Fakir.Metting.Dtos
{
    public class ConferenceDetailDto : IOutputDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public string Target { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Site { get; set; }

        public string Emcee { get; set; }

        public bool IsActive { get; set; }

        public List<ConferenceAgendaDetailDto> AgendaDetails { get; set; }
    }

    public class ConferenceAgendaDetailDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

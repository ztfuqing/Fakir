using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Fakir.Metting.Dtos.Agendas
{
    public class AgendaEditDto : IDto
    {
        public int AgendaId { get; set; }

        public string AgendaName { get; set; }

        public string ConferenceName { get; set; }

        public string Spokesman { get; set; }

        public List<AgendaFileDto> Files { get; set; }

        public string Content { get; set; }
    }


}

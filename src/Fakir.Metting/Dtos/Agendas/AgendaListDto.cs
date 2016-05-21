using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace Fakir.Metting.Dtos.Agendas
{
    public class AgendaListDto : IOutputDto
    {
        public int Id { get; set; }

        public string ConferenceName { get; set; }

        public  string Subject { get; set; }

        public  string Spokesman { get; set; }

        public  DateTime SpeakTime { get; set; }

        public  string SpeakLength { get; set; }

        public  string DepartmentName { get; set; }

    }
}

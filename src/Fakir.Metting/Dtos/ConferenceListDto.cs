using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Fakir.Metting.Domain;

namespace Fakir.Metting.Dtos
{
    [AutoMapFrom(typeof(Conference))]
    public class ConferenceListDto : EntityDto<long>
    {

        public string Name { get; set; }
               
        public string Subject { get; set; }
               
        public string Target { get; set; }
               
        public DateTime StartTime { get; set; }
               
        public DateTime EndTime { get; set; }
               
        public string Site { get; set; }
               
        public string Emcee { get; set; }
               
        public string Participant { get; set; }
               
        public bool IsActive { get; set; }
    }
}

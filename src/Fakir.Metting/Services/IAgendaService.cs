using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Fakir.Metting.Dtos.Agendas;

namespace Fakir.Metting.Services
{
    public interface IAgendaService: IApplicationService
    {
        Task<List<AgendaListDto>> GetUserDepartmentAgendas(NullableIdInput<long> input);


        Task<AgendaEditDto> GetAgendaForEdit(NullableIdInput<int> input);

        Task SaveAgenda(AgendaEditDto input);

        Task SaveFile(AgendaFileDto input);

        Task DeleteFile(NullableIdInput<int> input);
    }
}

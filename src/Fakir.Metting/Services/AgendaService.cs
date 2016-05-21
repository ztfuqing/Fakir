using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Fakir.Metting.Domain;
using Fakir.Metting.Dtos;
using System.Linq;
using Fakir.Dto;
using Abp.Organizations;
using Fakir.Metting.Dtos.Agendas;
using Fakir.Authorization.Users;
using Abp.UI;

namespace Fakir.Metting.Services
{
    public class AgendaService : FakirAppServiceBase, IAgendaService
    {
        private readonly IRepository<Conference, int> _cRepository;
        private readonly IRepository<Agenda, int> _aRepository;
        private readonly IRepository<ConferenceUser, int> _uRepository;
        private readonly IRepository<OrganizationUnit, long> _oRepository;
        private readonly IRepository<AgendaFile, int> _fRepository;

        public AgendaService(IRepository<Conference> cRepository, IRepository<Agenda, int> aRepository, IRepository<ConferenceUser, int> uRepository,
            IRepository<AgendaFile, int> fRepository,
            IRepository<OrganizationUnit, long> oRepository)
        {
            _cRepository = cRepository;
            _aRepository = aRepository;
            _uRepository = uRepository;
            _fRepository = fRepository;
            _oRepository = oRepository;
        }

        public async Task<List<AgendaListDto>> GetUserDepartmentAgendas(NullableIdInput<long> input)
        {
            var userId = input.Id.HasValue ? input.Id.Value : AbpSession.UserId.Value;
            var user = await UserManager.GetUserByIdAsync(userId);

            var departmentId = user.As<User>().Organization.Id;
            var query = await _aRepository.GetAll()
              .Where(a => a.DepartmentId == departmentId && a.Conference.IsActive == true).ToListAsync();

            return query.MapTo<List<AgendaListDto>>();
        }

        public async Task<AgendaEditDto> GetAgendaForEdit(NullableIdInput<int> input)
        {
            if (!input.Id.HasValue)
                throw new UserFriendlyException("不允许单独添加议程");

            var agenda = await _aRepository.GetAsync(input.Id.Value);

            var files = agenda.Files.Select(a => new AgendaFileDto
            {
                FileName = a.FileName,
                FileSize = a.FileSize,
                FileType = a.FileType,
                FileUrl = a.FileUrl,
                Id = a.Id
            }).ToList();

            return new AgendaEditDto
            {
                AgendaId = agenda.Id,
                AgendaName = agenda.Subject,
                Spokesman = agenda.Spokesman,
                ConferenceName = agenda.Conference.Name,
                Content = agenda.Content,
                Files = files
            };
        }

        public async Task SaveAgenda(AgendaEditDto input)
        {
            var agenda = _aRepository.Get(input.AgendaId);
            agenda.Content = input.Content;

            await _aRepository.UpdateAsync(agenda);
        }

        public async Task SaveFile(AgendaFileDto input)
        {
            var file = new AgendaFile
            {
                AgendaId = input.AgendaId,
                FileName = input.FileName,
                FileUrl = input.FileUrl
            };

            await _fRepository.InsertAsync(file);
        }

        public async Task DeleteFile(NullableIdInput<int> input)
        {
            if (input.Id.HasValue)
            {
                var file = await _fRepository.GetAsync(input.Id.Value);

                await _fRepository.DeleteAsync(file);
            }
        }
    }
}

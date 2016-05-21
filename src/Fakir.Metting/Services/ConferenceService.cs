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
namespace Fakir.Metting.Services
{
    public class ConferenceService : FakirAppServiceBase, IConferenceService
    {
        private readonly IRepository<Conference, int> _cRepository;
        private readonly IRepository<Agenda, int> _aRepository;
        private readonly IRepository<ConferenceUser, int> _uRepository;
        private readonly IRepository<OrganizationUnit, long> _oRepository;

        public ConferenceService(IRepository<Conference> cRepository, IRepository<Agenda, int> aRepository, IRepository<ConferenceUser, int> uRepository, IRepository<OrganizationUnit, long> oRepository)
        {
            _cRepository = cRepository;
            _aRepository = aRepository;
            _uRepository = uRepository;
            _oRepository = oRepository;
        }


        public async Task CreateOrUpdateConference(CreateOrUpdateConferenceInput input)
        {
            if (input.Conference.Id.HasValue)
            {
                await UpdateConferenceAsync(input);
            }
            else
            {
                await CreateConferenceAsync(input);
            }
        }

        public async Task<PagedResultOutput<ConferenceListDto>> GetConferences(GetConferencesInput input)
        {
            var query = _cRepository.GetAll()
              .WhereIf(
                  !input.Name.IsNullOrWhiteSpace(),
                  u => u.Name.Contains(input.Name) || u.Subject.Contains(input.Name));

            var conferenceCount = await query.CountAsync();
            var conferences = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var listDtos = conferences.MapTo<List<ConferenceListDto>>();

            return new PagedResultOutput<ConferenceListDto>(
                conferenceCount,
                listDtos
                );
        }

        public async Task<List<ConferenceListDto>> GetEnCompleteConference()
        {
            var query = await _cRepository.GetAllListAsync(a => a.IsActive == true);

            return query.MapTo<List<ConferenceListDto>>();
        }

        public async Task<ConferenceDetailDto> GetConferenceDetail(IdInput<int> input)
        {
            var conference = await _cRepository.GetAsync(input.Id);

            return new ConferenceDetailDto
            {
                Id = conference.Id,
                Name = conference.Name,
                Subject = conference.Subject,
                Target = string.Join("<br/>", conference.Target.Split("\n")),
                Emcee = conference.Emcee,
                Site = conference.Site,
                StartTime = conference.StartTime,
                EndTime = conference.EndTime,
                IsActive = conference.IsActive,
                AgendaDetails = conference.Agendas.Select(a => new ConferenceAgendaDetailDto
                {
                    Id = a.Id,
                    Name = a.Subject
                }).ToList()
            };
        }

        public async Task<GetConferenceForEditOutput> GetConferenceForEdit(NullableIdInput<int> input)
        {
            var output = new GetConferenceForEditOutput
            {
                DepartmentUserData = new DepartmentUserData { DepartmentUsers = await GetDepartmentUserTree() },
                Departments = (await GetDepartmentUserTree(false)).Select(a => new ComboboxItemDto(a.Id, a.DisplayName)).ToArray()


            };

            if (!input.Id.HasValue)
            {
                output.Conference = new ConferenceEditDto();
            }
            else
            {
                var conference = await _cRepository.GetAsync(input.Id.Value);

                output.Conference = conference.MapTo<ConferenceEditDto>();

                output.Users = conference.Users.Select(a => new ConferenceUserDto
                {
                    DisplayName = a.CreatorUser.Surname,
                    UserId = a.CreatorUserId.Value
                }).ToArray();
                output.DepartmentUserData.SelectedUsers = output.Users.Select(a => a.UserId.ToString()).ToList();

                output.Agendas = conference.Agendas.Select(a => a.MapTo<ConferenceAgendaDto>()).ToArray();
            }

            return output;
        }

        private async Task CreateConferenceAsync(CreateOrUpdateConferenceInput input)
        {
            var conference = input.Conference.MapTo<Conference>();
            conference.IsActive = true;
            await _cRepository.InsertAsync(conference);

            if (input.Agendas != null && input.Agendas.Length > 0)
            {
                foreach (var ag in input.Agendas)
                {
                    var agC = ag.MapTo<Agenda>();
                    agC.ConferenceId = conference.Id;
                    await _aRepository.InsertAsync(agC);
                }
            }

            if (input.Users != null && input.Users.Length > 0)
            {
                foreach (var ag in input.Users)
                {
                    var agC = ag.MapTo<ConferenceUser>();
                    agC.ConferenceId = conference.Id;
                    await _uRepository.InsertAsync(agC);
                }
            }
        }

        private async Task UpdateConferenceAsync(CreateOrUpdateConferenceInput input)
        {

            var conference = await _cRepository.GetAsync(input.Conference.Id.Value);


            input.Conference.MapTo(conference);



            while (conference.Agendas.Count() > 0)
            {
                _aRepository.Delete(conference.Agendas.First());
            }


            while (conference.Users.Count() > 0)
            {
                _uRepository.Delete(conference.Users.First());
            }


            await _cRepository.UpdateAsync(conference);
            if (input.Agendas != null && input.Agendas.Length > 0)
            {
                foreach (var ag in input.Agendas)
                {
                    var agC = ag.MapTo<Agenda>();
                    agC.ConferenceId = conference.Id;
                    await _aRepository.InsertAsync(agC);
                }
            }

            if (input.Users != null && input.Users.Length > 0)
            {
                foreach (var ag in input.Users)
                {
                    var agC = ag.MapTo<ConferenceUser>();
                    agC.ConferenceId = conference.Id;
                    await _uRepository.InsertAsync(agC);
                }
            }

            await _cRepository.UpdateAsync(conference);
        }

        private async Task<List<FlatDepartmentUserDto>> GetDepartmentUserTree(bool containsUser = true)
        {
            List<FlatDepartmentUserDto> list = new List<FlatDepartmentUserDto>();

            _oRepository.GetAllList().ForEach(a =>
            {
                list.Add(new FlatDepartmentUserDto
                {
                    DisplayName = a.DisplayName,
                    Id = (containsUser ? "D" : "") + a.Id.ToString(),
                    ParentId = (containsUser ? "D" : "") + (a.ParentId.HasValue ? a.ParentId.Value.ToString() : "")
                });
            });
            if (containsUser)
            {
                var users = await UserManager.Users.ToListAsync();

                users.ForEach(a =>
                {
                    list.Add(new FlatDepartmentUserDto
                    {
                        DisplayName = a.Surname,
                        Id = a.Id.ToString(),
                        ParentId = "D" + a.OrgId.ToString()
                    });
                });
            }
            return list;
        }

        public async Task CompleteConference(IdInput<int> input)
        {
            var conference = await _cRepository.GetAsync(input.Id);
            conference.IsActive = false;
            await _cRepository.UpdateAsync(conference);

        }

        public async Task FinishConference(IdInput<int> input)
        {
            var conference = await _cRepository.GetAsync(input.Id);
            conference.IsFinished = true;
            await _cRepository.UpdateAsync(conference);
        }
    }
}

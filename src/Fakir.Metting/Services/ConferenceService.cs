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

namespace Fakir.Metting.Services
{
    public class ConferenceService : FakirAppServiceBase, IConferenceService
    {
        private readonly IRepository<Conference, int> _cRepository;
        private readonly IRepository<Agenda, int> _aRepository;
        private readonly IRepository<ConferenceUser, int> _uRepository;

        public ConferenceService(IRepository<Conference> cRepository, IRepository<Agenda, int> aRepository, IRepository<ConferenceUser, int> uRepository)
        {
            _cRepository = cRepository;
            _aRepository = aRepository;
            _uRepository = uRepository;
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

        public async Task<GetConferenceForEditOutput> GetConferenceForEdit(NullableIdInput<int> input)
        {
            var output = new GetConferenceForEditOutput
            {
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

                output.Agendas = conference.Agendas.Select(a => new ConferenceAgendaDto
                {
                    Department = a.Department,
                    Order = a.Order,
                    SpeakLength = a.SpeakLength,
                    SpeakTime = a.SpeakTime,
                    Subject = a.Subject,
                    Spokesman = a.Spokesman
                }).ToArray();
            }

            return output;
        }

        private async Task CreateConferenceAsync(CreateOrUpdateConferenceInput input)
        {
            var conference = input.Conference.MapTo<Conference>();

            await _cRepository.InsertAsync(conference);

            if (input.Agendas!=null&&input.Agendas.Length > 0 )
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

            await _cRepository.UpdateAsync(conference);
        }
    }
}

using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Fakir.Dto
{
    public class PagedAndFilteredInputDto : IInputDto, IPagedResultRequest
    {
        [Range(1, FakirConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        public string Filter { get; set; }

        public PagedAndFilteredInputDto()
        {
            MaxResultCount = FakirConsts.DefaultPageSize;
        }
    }
}

using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Fakir.Dto
{
    public class PagedInputDto : IInputDto, IPagedResultRequest
    {
        [Range(1, FakirConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        public PagedInputDto()
        {
            MaxResultCount = FakirConsts.DefaultPageSize;
        }
    }
}

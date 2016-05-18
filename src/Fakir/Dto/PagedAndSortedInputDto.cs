using Abp.Application.Services.Dto;

namespace Fakir.Dto
{
    public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
    {
        public string Sorting { get; set; }

        public PagedAndSortedInputDto()
        {
            MaxResultCount = FakirConsts.DefaultPageSize;
        }
    }
}

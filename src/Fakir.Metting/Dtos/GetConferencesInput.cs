using System;
using Abp.Runtime.Validation;
using Fakir.Dto;

namespace Fakir.Metting.Dtos
{
    public class GetConferencesInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Name { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id Desc";
            }
        }
    }
}

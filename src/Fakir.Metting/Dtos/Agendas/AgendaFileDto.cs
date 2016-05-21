using Abp.Application.Services.Dto;

namespace Fakir.Metting.Dtos.Agendas
{
    public class AgendaFileDto : IDto
    {
        public int AgendaId { get; set; }

        public int Id { get; set; }

        public string FileName { get; set; }

        public string FileUrl { get; set; }

        public string FileType { get; set; }

        public int FileSize { get; set; }
    }
}
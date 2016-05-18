using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using Fakir.Metting.Domain;

namespace Fakir.Metting.Dtos
{
    [AutoMap(typeof(Conference))]
    public class ConferenceEditDto 
    {
        public int? Id { get; set; }     

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [StringLength(1000)]
        public string Target { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public  DateTime EndTime { get; set; }

        //地点
        [StringLength(100)]
        public  string Site { get; set; }

        //主持人
        [StringLength(50)]
        public  string Emcee { get; set; }
    }
}

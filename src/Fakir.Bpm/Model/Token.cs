using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Bpm.Model
{
    public class Token
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? EnterTime { get; set; }

        public DateTime? ArchiveTime { get; set; }

        public Token Parent { get; set; }

        public ProcessInstance ProcessInstance { get; set; }

        public Dictionary<string, string> Datas { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Workflow.Model
{
    public class ProcessDefinition
    {
        public Guid Id { get; set; }

        public Point Point { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Workflow.Model
{
    public class TransitionDefinition
    {
        public string Name { get; set; }

        public ActivityDefinition From { get; set; }

        public ActivityDefinition To { get; set; }
    }
}

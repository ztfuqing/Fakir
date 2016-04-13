using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Workflow.Model
{
    public class ConditionDefinition
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ConditionType Type { get; set; }
    }
}

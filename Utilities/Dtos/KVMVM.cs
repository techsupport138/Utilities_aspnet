using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Utilities.Dtos
{
    public class KVMVM
    {
        public Guid Key { get; set; }
        public string Value { get; set; }
        public string? Alt { get; set; } = null;
        public string? Media { get; set; } = null;
    }
    public class KVMIVM
    {
        public int Key { get; set; }
        public string Value { get; set; }
        public string? Alt { get; set; } = null;
        public string? Media { get; set; } = null;
    }
}

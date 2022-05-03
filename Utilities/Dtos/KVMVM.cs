﻿using System;
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
        public string? Alt { get; set; }
        public string? Media { get; set; }
    }
    public class KVMIVM
    {
        public int Key { get; set; }
        public string Value { get; set; }
        public string? Alt { get; set; }
        public string? Media { get; set; }
    }
}
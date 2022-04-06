using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities_aspnet.Values.Entities
{
    public enum ValueType : byte
    {
        SingleLineText = 1,
        MultiLineText = 2,
        Option = 3,
        CheckListBox = 4,
        Bool = 5,
        Int = 6,
        Decimal = 7,
        File = 8,
        Image = 9
    }
}

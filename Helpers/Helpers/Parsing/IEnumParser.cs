using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Parsing;

public interface IEnumParser<TEnum> where TEnum : struct, Enum
{
    TEnum Parse(byte value);
}

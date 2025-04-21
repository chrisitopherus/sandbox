using Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Parsing;

public static class EnumParser<TEnum> where TEnum : struct, Enum
{
    private static IEnumParser<TEnum>? _parser;
    private static Func<byte, TEnum>? _customParser;

    public static void SetParser(IEnumParser<TEnum> parser)
    {
        Validator.NotNull(parser, nameof(parser));
        _parser = parser;
    }

    public static void SetParser(Func<byte, TEnum> parser)
    {
        Validator.NotNull(parser, nameof(parser));
        _customParser = parser;
    }

    public static TEnum Parse(byte value)
    {
        Func<byte, TEnum> parse = PickParser();
        return parse(value);
    }

    public static bool TryParse(byte value, out TEnum enumValue)
    {
        try
        {
            enumValue = Parse(value);
            return true;
        }
        catch
        {
            enumValue = default;
            return false;
        }
    }

    public static byte ToByte(TEnum value) => Convert.ToByte(value);

    public static bool HasParser => _parser != null || _customParser != null;

    private static Func<byte, TEnum> PickParser()
    {
        if (_customParser == null && _parser == null)
        {
            throw new InvalidOperationException($"No parser has been set for enum type {typeof(TEnum).Name}.");
        }

        return _parser != null ? _parser.Parse : _customParser ?? throw new InvalidOperationException($"No parser has been set for enum type {typeof(TEnum).Name}.");
    }
}

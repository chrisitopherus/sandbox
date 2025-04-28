using ConsoleWyrm.Networking.Messages.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm.Utility;

public static class ExceptionFactory
{
    public static InvalidDataException CreateDecodeFailure(string productName, Exception exception)
    {
        return CreateInvalidDataException("decode", productName, exception);
    }

    public static InvalidDataException CreateEncodeFailure(string sourceName, Exception exception)
    {
        return CreateInvalidDataException("encode", sourceName, exception);
    }

    public static InvalidDataException CreateInvalidFormat(string productName)
    {
        return new InvalidDataException($"Invalid {productName} format.");
    }

    private static InvalidDataException CreateInvalidDataException(string operation, string name, Exception? exception = default) 
    {
        return new InvalidDataException($"Failed to {operation} {name}", exception);
    }
}

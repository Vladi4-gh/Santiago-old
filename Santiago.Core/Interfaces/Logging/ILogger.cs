using System;

namespace Santiago.Core.Interfaces.Logging
{
    public interface ILogger
    {
        void Error(Exception exception, string message);
    }
}
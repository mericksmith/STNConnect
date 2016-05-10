using System;

namespace StationCasinos.WebAPI.Utility.Interface
{
    public interface ILogging
    {
        void Write(string message, string source);

        void Write(string message);

        void Write(System.Exception exception, string source);

        void Write(System.Exception exception);

        void SetReuqestId(string sourceId);
    }
}

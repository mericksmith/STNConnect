using System;
using StationCasinos.WebAPI.Utility.Interface;
using System.Web;

namespace StationCasinos.WebAPI.Utility.Logging
{
    public class Logging : ILogging
    {
        private static log4net.ILog Log { get; set; }

        static Logging()
        {
            Log = log4net.LogManager.GetLogger(typeof(Logging));
        }

        public void SetReuqestId(string sourceId)
        {
            log4net.GlobalContext.Properties["requestId"] = sourceId;
        }


        public void Write(Exception exception)
        {
            Write(exception,"-");
        }

        public void Write(string message)
        {
            Write(message, "-");
        }

        public void Write(string message, string source)
        {
            Log.Info(string.Format("Source: {0} | Message: {1}",source,message));
        }

        public void Write(System.Exception ex, string source)
        {
            Log.Error(string.Format("Source: {0} | Message: ", source), ex);
        }

    }
}

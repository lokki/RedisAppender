using System;
using log4net.Core;

namespace Lokki.Utils.RedisAppender
{
    public class LogLine
    {
        private static readonly DateTime EpochStart = new DateTime(1970, 1, 1);
        
        public LogLine(LoggingEvent loggingEvent)
        {
            HostName = loggingEvent.LookupProperty(LoggingEvent.HostNameProperty).ToString();
            Identity = loggingEvent.Identity;
            UserName = loggingEvent.UserName;
            Domain = loggingEvent.Domain;
            TimeStamp = (long)(loggingEvent.TimeStamp.ToUniversalTime() - EpochStart).TotalMilliseconds;
            Level = loggingEvent.Level.DisplayName;
            LoggerName = loggingEvent.LoggerName;
            Thread = loggingEvent.ThreadName;
            Message = loggingEvent.RenderedMessage;
            Throwable = loggingEvent.GetExceptionString();
            Location = new Location
                {
                    Class = loggingEvent.LocationInformation.ClassName,
                    Method = loggingEvent.LocationInformation.MethodName,
                    File = loggingEvent.LocationInformation.FileName,
                    Line = loggingEvent.LocationInformation.LineNumber
                };
        }

        public string HostName { get; set; }
        public string Identity { get; set; }
        public string UserName { get; set; }
        public string Domain { get; set; }
        public string LoggerName { get; set; }
        public long TimeStamp { get; set; }
        public string Level { get; set; }
        public string Thread { get; set; }
        public string Message { get; set; }
        public string Throwable { get; set; }
        public Location Location { get; set; }
    }
}
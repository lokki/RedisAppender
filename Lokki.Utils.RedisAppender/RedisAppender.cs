using System;
using ServiceStack.Redis;
using log4net.Appender;
using log4net.Core;

namespace Lokki.Utils.RedisAppender
{
    public class RedisAppender : AppenderSkeleton
    {
        private IRedisClientsManager _factory;

        public string RedisHost { get; set; }
        public string ListName { get; set; }
        public int MaxLength { get; set; }
        public bool TrimList { get; set; }
        
        protected override void Append(LoggingEvent loggingEvent)
        {
            CheckEvent(loggingEvent);
            CheckFactory();
            
            try
            {
                SendLine(loggingEvent);
            }
            catch{}
        }

        private void SendLine(LoggingEvent loggingEvent)
        {
            using (var client = _factory.GetClient().As<LogLine>())
            {
                var line = new LogLine(loggingEvent);
                client.Lists[ListName].Prepend(line);
                if (TrimList)
                    client.Lists[ListName].Trim(0, MaxLength - 1);
            }
        }

        private void CheckFactory()
        {
            if (_factory == null)
                _factory = new PooledRedisClientManager(RedisHost);
        }

        private void CheckEvent(LoggingEvent loggingEvent)
        {
            if (loggingEvent == null)
                throw new ArgumentNullException("loggingEvent");
        }
    }
}
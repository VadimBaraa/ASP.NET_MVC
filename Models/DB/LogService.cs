using ASP.NET_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASP.NET_MVC.Services
{
    public class LogService : ILogService
    {
        private readonly List<LogEntry> _logs = new List<LogEntry>();

        public void Log(string message)
        {
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Message = message
            };
            _logs.Add(logEntry);
            
        }

        public List<LogEntry> GetLogs()
        {
            return _logs.OrderByDescending(l => l.Timestamp).ToList(); // Получаем логи в обратном хронологическом порядке
        }
    }
}
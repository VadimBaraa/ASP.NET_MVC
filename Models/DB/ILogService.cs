using ASP.NET_MVC.Models;
using System.Collections.Generic;

namespace ASP.NET_MVC.Services
{
    public interface ILogService
    {
        void Log(string message);
        List<LogEntry> GetLogs();
    }
}
using Microsoft.AspNetCore.Components.Web;
using my_books_api.Data.Models;
using my_books_api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books_api.Data.Services
{
    public class LogsService
    {

        private AppDbContext _context;

        public LogsService(AppDbContext context)
        {
            _context = context;

        }

        public List<Log>GetAllLogsFromDB() => _context.Logs.ToList();
    
public void AddLog(string level, string message, string exception = "")
{
    Console.WriteLine("ADD LOG EXECUTED");

    var log = new Log()
    {
        Level = level,
        Message = message,
        Exception = exception,
        TimeStamp = DateTime.Now
    };

    _context.Logs.Add(log);
    _context.SaveChanges();

    Console.WriteLine("LOG SAVED");
}

}

}
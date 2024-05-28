using CareerFIZ.Models;
using CareerFIZ.DataContext;
using System.Net;

namespace CareerFIZ.Services
{
    public class LogCatcher
    {
        private readonly DataDbContext context;
        public LogCatcher(DataDbContext context)
        {
            this.context = context;
        }

        public void Logging(Exception ex, string Id, string page, string ipad) 
        {
            var gd = new Guid("00000001-0000-0000-0000-000000000000");
            if (Id != null) { gd = Guid.Parse(Id); }
            string[] octets = ipad.Split('.');
            string firstTwoOctets = string.Join('.', octets.Take(2));
            Log lg = new Log();
            lg.Action = ex.ToString();
            lg.ActionTime = DateTime.Now;
            lg.AppUserId = gd;
            lg.Ipaddress = firstTwoOctets;
            
        }
    }
}

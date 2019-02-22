using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AlbumInfo.API.Services
{
    public class LocalMailServices : IMailService
    {
        public string mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        public string mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {mailFrom} was to send to {mailTo}, with LocalMailSerivce");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}

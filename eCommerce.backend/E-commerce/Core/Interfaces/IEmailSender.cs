using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmailSender
    {
        public Task SendEmail(string email, string subject, string htmlMessage);
    }
}

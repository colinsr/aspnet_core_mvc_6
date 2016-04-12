using System.Diagnostics;

namespace TheWorld_V2.Services
{
    class DebugMailService : IMailService
    {
        public bool SendMail(string to, string @from, string subject, string body)
        {
            Debug.WriteLine($"Sending mail to {to}, Subject: {subject}.");

            return true;
        }
    }
}

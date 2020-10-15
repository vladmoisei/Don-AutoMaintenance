using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MVCWithBlazor.Models;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVCWithBlazor.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IOptions<SmtpOptions> _options;
        public SmtpEmailSender(IOptions<SmtpOptions> options)
        {
            _options = options;
        }
        public async Task SendEmailAsync(string fromAdress, string toAddress, string subjsect, string messaege)
        {
            var mailMessage = new MailMessage(fromAdress, toAddress, subjsect, messaege);
            using (var client = new SmtpClient(_options.Value.Host, _options.Value.Port)
            {
                Credentials = new NetworkCredential(_options.Value.Username, _options.Value.Password),
                EnableSsl = _options.Value.EnableSsl
            })
            {
                await client.SendMailAsync(mailMessage);
            }
        }

        // Create JSON File with 
        public string WriteToJsonMailData(MailModel mailModel, IWebHostEnvironment env)
        {
            string mailFileName = $"MailDate.JSON";
            string wwwPath = env.WebRootPath;
            string filePath = Path.Combine(wwwPath, "Fisiere");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = Path.Combine(filePath, mailFileName).ToString();
            string jsonString = JsonSerializer.Serialize(mailModel, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
            return filePath;
        }

        // Return mailDataModel From Json File
        public async Task<MailModel> GetMailModelAsync(string filePath)
        {
            MailModel mailModel;
            using (FileStream fs = File.OpenRead(filePath))
            {
                mailModel = await JsonSerializer.DeserializeAsync<MailModel>(fs);
            }
            return mailModel;
        }
    }
}

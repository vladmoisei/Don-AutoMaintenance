using Microsoft.AspNetCore.Hosting;
using MVCWithBlazor.Models;
using System.Threading.Tasks;

namespace MVCWithBlazor.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string fromAdress, string toAddress, string subjsect, string messaege);
        string WriteToJsonMailData(MailModel mailModel, IWebHostEnvironment env);
        Task<MailModel> GetMailModelAsync(string filePath);
    }
}

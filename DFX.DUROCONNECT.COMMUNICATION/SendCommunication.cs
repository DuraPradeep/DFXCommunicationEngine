using DFX.DUROCONNECT.COMM.BAL;
using DFX.DUROCONNECT.COMM.ENTITIES;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace DFX.DUROCONNECT.COMMUNICATION
{
    public class SendCommunication
    {
        private static IConfiguration _configuration;
        private static ILogger<SendCommunication> _logger;
        private static HttpClient _httpClient;
        private static  CommFacade _commFacade = new CommFacade();

        //public static void Initialize(IConfiguration configuration, ILogger<SendCommunication> logger, IHttpClientFactory httpClientFactory)
        //{
        //    _configuration = configuration;
        //    _logger = logger;
        //    _httpClient = httpClientFactory.CreateClient();
        //}
        public SendCommunication(IConfiguration configuration, ILogger<SendCommunication> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _commFacade = new CommFacade();
        }

        public static async Task<string> Send()
        {
            var sendingItemList = _commFacade.ISendingItemList();

            foreach (var item in sendingItemList)
            {
                tblCommunication tblCommunication = item;
                try
                {
                    switch (item.CommunicationType)
                    {
                        case 1:
                            await HandleSmsCommunication(tblCommunication);
                            break;
                        case 2:
                            HandleEmailCommunication(tblCommunication);
                            break;
                        default:
                            //_logger.LogWarning("Unknown communication type: {CommunicationType}", item.CommunicationType);
                            item.Status = 2; // Error
                            _commFacade.SaveCommunicationResponse(item);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, "Error processing communication for item: {ItemId}", item.ID);
                    item.Status = 2; // Error
                    _commFacade.SaveCommunicationResponse(item);
                }
            }
            return "Communications processed successfully.";
        }

        private static async Task HandleSmsCommunication(tblCommunication item)
        {
            try
            {
                
                var smsConfiguration = _commFacade.GetSMSConfiguration(item.CommunicationType).FirstOrDefault();
                if (smsConfiguration == null)
                {
                    item.Status = 2; // Error
                    _commFacade.SaveCommunicationResponse(item);
                    //_logger.LogWarning("SMS configuration not found for communication type: {CommunicationType}", item.CommunicationType);
                    return;
                }

                var apiUrl = smsConfiguration.Url.Replace("{Sendto}", item.SendTo).Replace("{msg}", item.Text);
                HttpClient _httpClient = new HttpClient();
                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    item.Status = 1; // Success
                    //_logger.LogInformation("SMS sent successfully to: {SendTo}", item.SendTo);
                }
                else
                {
                    item.Status = 2; // Error
                    //_logger.LogWarning("Failed to send SMS to: {SendTo}. Status Code: {StatusCode}", item.SendTo, response.StatusCode);
                }
                _commFacade.SaveCommunicationResponse(item);
            }
            catch (HttpRequestException ex)
            {
                //_logger.LogError(ex, "HTTP request error while sending SMS to: {SendTo}", item.SendTo);
                item.Status = 2; // Error
                _commFacade.SaveCommunicationResponse(item);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Unexpected error while sending SMS to: {SendTo}", item.SendTo);
                item.Status = 2; // Error
                _commFacade.SaveCommunicationResponse(item);
            }
        }

        private static void HandleEmailCommunication(tblCommunication item)
        {
            try
            {

                var emailConfiguration = _commFacade.GetEmailConfiguration(item.CommunicationType).FirstOrDefault();
                if (emailConfiguration == null)
                {
                    item.Status = 2; // Error
                    _commFacade.SaveCommunicationResponse(item);
                    //_logger.LogWarning("Email configuration not found for communication type: {CommunicationType}", item.CommunicationType);
                    return;
                }

                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailConfiguration.FromAddress);
                    mail.To.Add(item.SendTo);
                    if (!string.IsNullOrEmpty(emailConfiguration.CC))
                    {
                        mail.CC.Add(emailConfiguration.CC);
                    }
                    if (!string.IsNullOrEmpty(emailConfiguration.BCC))
                    {
                        mail.Bcc.Add(emailConfiguration.BCC);
                    }

                    mail.Subject = emailConfiguration.Subject;
                    mail.Body = item.Text;
                    mail.IsBodyHtml = emailConfiguration.IsBodyHtml;

                    using (var smtp = new SmtpClient(emailConfiguration.Host, emailConfiguration.Port))
                    {
                        smtp.Credentials = new NetworkCredential(emailConfiguration.FromAddress, emailConfiguration.FromPassword);
                        smtp.EnableSsl = emailConfiguration.EnableSsl;
                        smtp.Send(mail);
                        item.Status = 1; // Success

                    }
                }
            }
            catch (SmtpException ex)
            {
                //_logger.LogError(ex, "SMTP error while sending email to: {SendTo}", item.SendTo);
                item.Status = 2; // Error
                _commFacade.SaveCommunicationResponse(item);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Unexpected error while sending email to: {SendTo}", item.SendTo);
                item.Status = 2; // Error

            }
            finally { _commFacade.SaveCommunicationResponse(item); }
        }
    }
}

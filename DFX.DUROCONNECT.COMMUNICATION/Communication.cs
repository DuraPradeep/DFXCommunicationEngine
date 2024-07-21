using DFX.DUROCONNECT.COMM.BAL;
using DFX.DUROCONNECT.COMM.COMMON;
using DFX.DUROCONNECT.COMM.ENTITIES;
using DFX.DUROCONNECT.COMM.ENTITIES.Common;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DFX.DUROCONNECT.COMMUNICATION
{
    public class Communication
    {
        private readonly CommFacade _commFacade = new CommFacade();

        public string GetUserDetails(string mobileNo)
        {
            var userLogin = _commFacade.CheckUserEmailExistOrNot(mobileNo).ObjectParam;

            if (userLogin == null || userLogin.ChannelCode.IsNullOrEmpty())
            {
                return "User details not registered on Duroconnect portal, please contact an administrator";
            }

            var maskedMobile = MaskMobileNumber(userLogin.Mobileno);
            var maskedEmail = MaskEmail(userLogin.Email);

            string message = $"OTP sent to {maskedMobile} {maskedEmail} Successfully";

            SendAndSaveOtp(userLogin.ChannelCode, userLogin.Email, 0);

            return message;
        }

        private string MaskMobileNumber(string mobileNo)
        {
            return string.IsNullOrEmpty(mobileNo) ? string.Empty : $"Your Mobile {Regex.Replace(mobileNo, @"\d(?!\d{0,3}$)", "X")}";
        }

        private string MaskEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return string.Empty;
            var pattern = @"(?<=[\w]{1})[\w\-._\+%]*(?=[\w]{1}@)";
            return $"Your Email {Regex.Replace(email, pattern, m => new string('*', m.Length))}";
        }

        public  ServiceResponse<UserOTP> SendAndSaveOtp(string channelCode, string? newEmail, int typeId)
        {
            var userProfile = _commFacade.UserDetailsByLoginId(channelCode).ObjectParam;
            var otp = OTPGenerator.GenerateNumericOTP();
            //SendCommunication sendCommunication = null;
            try
            {
                if (typeId == 1)
                {
                    return ProcessSMSOtp(userProfile.Mobileno, userProfile, otp);
                }

                if (typeId == 2)
                {
                    return ProcessEmailOtp(userProfile.Email, userProfile, otp);
                }

                var smsResponse = ProcessSMSOtp(userProfile.Mobileno, userProfile, otp);
 
                var emailResponse = ProcessEmailOtp(userProfile.Email, userProfile, otp);
                SendCommunication.Send();


                //var EmailAllow = Configuration.GetSection("Data:OTPTemplate").Value;



                return smsResponse ?? emailResponse;
            }
            catch (Exception)
            {
                // Log error here
                return new ServiceResponse<UserOTP>();
            }
        }

        private ServiceResponse<UserOTP> ProcessSMSOtp(string newEmail, UserLogin userProfile, string otp)
        {
            if (!string.IsNullOrEmpty(newEmail))
            {
                userProfile.Mobileno = newEmail;
            }

            if (string.IsNullOrEmpty(userProfile.Mobileno)) return null;

            var smsConfig = _commFacade.GetSMSConfiguration(1).FirstOrDefault();
            var templateFormat = _commFacade.GetTemplateFormat(1).FirstOrDefault();
            var templateMessage = TemplateGeneration.SMSGenerate(templateFormat, userProfile, otp, out int templateId);

            return _commFacade.SaveOneTimePassword(smsConfig.ID, templateMessage, userProfile, otp, 1, templateId);
        }

        private ServiceResponse<UserOTP> ProcessEmailOtp(string newEmail, UserLogin userProfile, string otp)
        {
            if (!string.IsNullOrEmpty(newEmail))
            {
                userProfile.Email = newEmail;
            }

            if (string.IsNullOrEmpty(userProfile.Email)) return null;

            var emailConfig = _commFacade.GetEmailConfiguration(2).FirstOrDefault();
            var templateFormat = _commFacade.GetTemplateFormat(2).FirstOrDefault();
            var templateMessage = TemplateGeneration.EmailGenerate(templateFormat, userProfile, otp, out int templateId);

            return _commFacade.SaveOneTimePassword(emailConfig.Id, templateMessage, userProfile, otp, 2, templateId);
        }
    }
}

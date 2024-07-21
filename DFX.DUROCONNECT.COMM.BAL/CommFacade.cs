using DFX.DUROCONNECT.COMM.DAL.Mapper;
using DFX.DUROCONNECT.COMM.DAL.Wrapper;
using DFX.DUROCONNECT.COMM.ENTITIES;
using DFX.DUROCONNECT.COMM.ENTITIES.Common;
using System;
using System.Collections.Generic;

namespace DFX.DUROCONNECT.COMM.BAL
{
    public class CommFacade
    {
        private T ExecuteServiceCall<T>(Func<T> function)
        {
            try
            {
                return function();
            }
            catch (Exception ex)
            {
                // Log the exception
                // log.Error($"Exception at :", ex);
                return default(T);
            }
        }

        public ServiceResponse<UserLogin> CheckUserEmailExistOrNot(string emailMobile)
        {
            return ExecuteServiceCall(() =>
                DataMapper.MapCheckLoginUserEmailExistOrNot(SPWrapper.CheckLoginUserEmailExistOrNot(emailMobile))
            );
        }

        public ServiceResponse<UserLogin> UserDetailsByLoginId(string channelCode)
        {
            return ExecuteServiceCall(() =>
                DataMapper.UserDetailsByLoginId(SPWrapper.UserDetailsByLoginId(channelCode))
            );
        }

        public ServiceResponse<UserOTP> SaveOneTimePassword(int configId, string content, UserLogin userProfile, string otp, int typeId, int templateId)
        {
            return ExecuteServiceCall(() =>
                DataMapper.MapSetUserOTP(SPWrapper.SaveOneTimePassword(configId, content, userProfile, otp, typeId, templateId))
            );
        }

        public ServiceResponse<UserLogin> CheckLoginEmailIdWithOtp(string emailMobile, string otp, string userId)
        {
            return ExecuteServiceCall(() =>
                DataMapper.MapCheckLoginUserEmailWithOtp(SPWrapper.CheckLoginUserEmailWithOtp(emailMobile, otp, userId))
            );
        }

        public IEnumerable<tblSMSConfiguration> GetSMSConfiguration(int type)
        {
            return ExecuteServiceCall(() => SPWrapper.GetSMSConfiguration(type));
        }

        public IEnumerable<tblEmailConfiguration> GetEmailConfiguration(int type)
        {
            return ExecuteServiceCall(() => SPWrapper.GetEmailConfiguration(type));
        }

        public IEnumerable<tblTemplateMaster> GetTemplateFormat(int type)
        {
            return ExecuteServiceCall(() => SPWrapper.GetTemplateFormat(type));
        }

        public IEnumerable<tblCommunication> ISendingItemList()
        {
            return ExecuteServiceCall(() => SPWrapper.ISendingItemList());
        }

        public int SaveCommunicationResponse(tblCommunication tCommunication)
        {
            return ExecuteServiceCall(() => SPWrapper.SaveCommunicationResponse(tCommunication));
        }
    }
}

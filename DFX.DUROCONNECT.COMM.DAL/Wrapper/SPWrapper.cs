using DFX.DUROCONNECT.COMM.ENTITIES;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DFX.DUROCONNECT.COMM.DAL.Wrapper
{
    public class SPWrapper
    {
        public static string SqlConnectionString { get; set; }

        public SPWrapper(string configuration)
        {
            SqlConnectionString = configuration;
        }

        private static SqlConnection GetConnection()
        {
            return new SqlConnection(SqlConnectionString);
        }

        private static DataSet ExecuteStoredProcedure(string procedureName, Action<SqlCommand> parameterize)
        {
            using (var con = GetConnection())
            using (var com = new SqlCommand(procedureName, con) { CommandType = CommandType.StoredProcedure })
            using (var da = new SqlDataAdapter(com))
            {
                var ds = new DataSet();
                parameterize(com);
                con.Open();
                da.Fill(ds);
                return ds;
            }
        }

        public static DataSet CheckLoginUserEmailExistOrNot(string emailMobile)
        {
            return ExecuteStoredProcedure("SP_CheckUser", com =>
            {
                com.Parameters.AddWithValue("@mailmobile", emailMobile);
            });
        }

        public static DataSet UserDetailsByLoginId(string loginId)
        {
            return ExecuteStoredProcedure("sp_getUserDetailsbyId", com =>
            {
                com.Parameters.AddWithValue("@ChannelCode", loginId);
            });
        }

        public static DataSet SaveOneTimePassword(int configId, string content, UserLogin userProfile, string otp, int typeId, int templateId)
        {
            return ExecuteStoredProcedure("Proc_SaveOneTimePassword", com =>
            {
                com.Parameters.AddWithValue("@ConfigurationId", configId);
                com.Parameters.AddWithValue("@TemplateId", templateId);
                com.Parameters.AddWithValue("@SendTo", typeId == 2 ? userProfile.Email : userProfile.Mobileno);
                com.Parameters.AddWithValue("@Text", content);
                com.Parameters.AddWithValue("@OTP", otp);
                com.Parameters.AddWithValue("@TimeoutDate", DateTime.Now.AddMinutes(5));
                com.Parameters.AddWithValue("@UserId", userProfile.ChannelCode);
                com.Parameters.AddWithValue("@TypeId", typeId);
            });
        }

        public static DataSet CheckLoginUserEmailWithOtp(string emailMobile, string otp, string userId)
        {
            return ExecuteStoredProcedure("Proc_UserLoginByEmailAndOTP", com =>
            {
                com.Parameters.AddWithValue("@SendTo", emailMobile);
                com.Parameters.AddWithValue("@OTP", otp);
                com.Parameters.AddWithValue("@UserId", userId);
            });
        }

        public static IEnumerable<tblSMSConfiguration> GetSMSConfiguration(int type)
        {
            using var connection = GetConnection();
            return connection.ExecuteQuery<tblSMSConfiguration>("[dbo].[Proc_GetConfiguration]", new { @typeid = type }, commandType: CommandType.StoredProcedure).ToList();
        }

        public static IEnumerable<tblEmailConfiguration> GetEmailConfiguration(int type)
        {
            using var connection = GetConnection();
            return connection.ExecuteQuery<tblEmailConfiguration>("[dbo].[Proc_GetConfiguration]", new { @typeid = type }, commandType: CommandType.StoredProcedure).ToList();
        }

        public static IEnumerable<tblTemplateMaster> GetTemplateFormat(int type)
        {
            using var connection = GetConnection();
            return connection.ExecuteQuery<tblTemplateMaster>("[dbo].[Proc_GetTemplateFormat]", new { @typeid = type }, commandType: CommandType.StoredProcedure).ToList();
        }

        public static IEnumerable<tblCommunication> ISendingItemList()
        {
            using var connection = GetConnection();
            return connection.ExecuteQuery<tblCommunication>("[dbo].[Proc_SendingItemList]", new { }, commandType: CommandType.StoredProcedure).ToList();
        }

        public static int SaveCommunicationResponse(tblCommunication tCommunication)
        {
            using var connection = GetConnection();
            return connection.Update(tCommunication);
        }
    }
}

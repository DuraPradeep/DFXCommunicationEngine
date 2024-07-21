using DFX.DUROCONNECT.COMM.ENTITIES;
using DFX.DUROCONNECT.COMM.ENTITIES.Common;
using System;
using System.Data;

namespace DFX.DUROCONNECT.COMM.DAL.Mapper
{
    public class DataMapper
    {
        public static ServiceResponse<UserLogin> MapCheckLoginUserEmailExistOrNot(DataSet pds)
        {
            return MapUserLoginResponse(pds);
        }

        public static ServiceResponse<UserLogin> UserDetailsByLoginId(DataSet pds)
        {
            return MapUserLoginResponse(pds);
        }

        public static ServiceResponse<UserOTP> MapSetUserOTP(DataSet pds)
        {
            ServiceResponse<UserOTP> response = new ServiceResponse<UserOTP>();
            if (IsDataSetValid(pds))
            {
                response.Id = Convert.ToInt32(pds.Tables[0].Rows[0]["ID"]);
                response.ObjectParam = new UserOTP();
            }
            else
            {
                response.Errcode = 500;
            }
            return response;
        }

        public static ServiceResponse<UserLogin> MapCheckLoginUserEmailWithOtp(DataSet pds)
        {
            return MapUserLoginResponse(pds);
        }

        private static ServiceResponse<UserLogin> MapUserLoginResponse(DataSet pds)
        {
            ServiceResponse<UserLogin> response = new ServiceResponse<UserLogin>();
            if (IsDataSetValid(pds))
            {
                response.ObjectParam = new UserLogin
                {
                    ChannelCode = pds.Tables[0].Rows[0]["ChannelCode"].ToString(),
                    ChannelName = pds.Tables[0].Rows[0]["ChannelName"].ToString(),
                    Mobileno = pds.Tables[0].Rows[0]["Mobileno"].ToString(),
                    Email = pds.Tables[0].Rows[0]["Email"].ToString()
                };
            }
            else
            {
                response.ObjectParam = new UserLogin();
                response.Errcode = 500;
            }
            return response;
        }

        private static bool IsDataSetValid(DataSet pds)
        {
            return pds != null && pds.Tables.Count > 0 && pds.Tables[0].Rows.Count > 0;
        }
    }
}

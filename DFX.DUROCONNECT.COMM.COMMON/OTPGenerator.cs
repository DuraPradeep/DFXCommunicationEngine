using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DFX.DUROCONNECT.COMM.COMMON
{
    public class OTPGenerator
    {
         

        public static string GenerateNumericOTP()
        {
            int length = 6;
            if (length <= 0) throw new ArgumentException("Length must be greater than 0");

            // Generate a random number and ensure it doesn't start or end with zero
            while (true)
            {
                byte[] randomNumber = new byte[4]; // 4 bytes for a 32-bit integer
                RandomNumberGenerator.Fill(randomNumber);
                int otpValue = BitConverter.ToInt32(randomNumber, 0);
                otpValue = Math.Abs(otpValue) % (int)Math.Pow(10, length); // Ensure the value is within the range

                string otp = otpValue.ToString();

                // Check if the OTP has the correct length and doesn't start or end with zero
                if (otp.Length == length && otp[0] != '0' && otp[otp.Length - 1] != '0')
                {
                    return otp;
                }
            }
        }
    }
}

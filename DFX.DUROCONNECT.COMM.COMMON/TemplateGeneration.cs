using DFX.DUROCONNECT.COMM.ENTITIES;

namespace DFX.DUROCONNECT.COMM.COMMON
{
    public static class TemplateGeneration
    {
        public static string SMSGenerate(tblTemplateMaster template, UserLogin userProfile, string OTP, out int TemplateId)
        {
            TemplateId = template.ID;
            var content = template.TemplateText.Replace("{FirstName}", userProfile.ChannelName);
            content = content.Replace("{CompanyName}", "DuroConnect");
            content = content.Replace("{Time}", "5");
            content = content.Replace("{Otp}", OTP);
            content = content.Replace("{Teams}", "DuroConnect Team");
            return content;
        }
        public static string EmailGenerate(tblTemplateMaster template, UserLogin userProfile, string OTP, out int TemplateId)
        {
            TemplateId = template.ID;
            string content = template.TemplateText;
            //string content1 = System.IO.File.ReadAllText(address);
            content = content.Replace("{FirstName}", userProfile.ChannelName);
            content = content.Replace("{CompanyName}", "Duroflex Private Limited.");
            content = content.Replace("{OTP}", OTP);
            content = content.Replace("{SubCompanyName}", "DuroConnect");
            content = content.Replace("{TeamName}", "DuroConnect Team");
            content = content.Replace("{Address}", "Duroflex Private Limited. #30/6, NR Trident Tec Park, Hosur Main Road, HSR Layout, Sector 6,Bengaluru, Karnataka, India 560068");
            content = content.Replace("{Country}", "India");
            return content;


        }
    }
}

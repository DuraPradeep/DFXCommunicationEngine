using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFX.Duroconnect.CommunicationEngine.Entity
{
    public class tblConfiguration
    {

        public tblSMSConfiguration SMSConfiguration { get; set; }
        public tblEmailConfiguration EmailConfiguration { get; set; }

    }
    public class tblSMSConfiguration
    {
        public int ID { get; set; }

        public string Url { get; set; }

        public string msg_type { get; set; }

        public string userid { get; set; }

        public string password { get; set; }

        public string version { get; set; }

        public string format { get; set; }

        public string auth_scheme { get; set; }

        public string mask { get; set; }

        public string RequestType { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedIP { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedIP { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? bdeleted { get; set; }

        public string Method { get; set; }

        public string send_to { get; set; }

        public string msg { get; set; }
    }
    public class tblEmailConfiguration
    {
        public int Id { get; set; }

        public string FromAddress { get; set; }

        public string FromPassword { get; set; }

        public string Host { get; set; }

        public string CC { get; set; }

        public string BCC { get; set; }

        public string Subject { get; set; }

        public bool IsBodyHtml { get; set; }

        public string Body { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        public string Send { get; set; }

    }
}

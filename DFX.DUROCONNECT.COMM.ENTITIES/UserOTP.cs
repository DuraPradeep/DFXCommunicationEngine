using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFX.DUROCONNECT.COMM.ENTITIES
{
    public class UserOTP
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string OTP { get; set; }
        public string TemplateEmail { get; set; }
        public DateTime TimeoutDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedIP { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedIP { get; set; }
        public int ModifiedBy { get; set; }
        public bool bdeleted { get; set; }

    }
}

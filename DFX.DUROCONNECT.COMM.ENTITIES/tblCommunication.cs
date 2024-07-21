using System;

namespace DFX.DUROCONNECT.COMM.ENTITIES
{
    public class tblCommunication
    {
        public int ID { get; set; }
        public int CommunicationType { get; set; }

        public int ConfigurationId { get; set; }

        public int? TemplateId { get; set; }

        public string SendTo { get; set; }

        public string Text { get; set; }

        public int? Status { get; set; }

        public DateTime? TimeoutDate { get; set; }

        public int? UserId { get; set; }

        public string OTP { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedIP { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedIP { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? bdeleted { get; set; }

    }
}

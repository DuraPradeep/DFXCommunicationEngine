using System;

namespace DFX.DUROCONNECT.COMM.ENTITIES
{
    public class tblTemplateMaster
    {
        public int ID { get; set; }

        public int? TypeId { get; set; }

        public int? FormatType { get; set; }

        public string TemplateText { get; set; }

        public string Parameters { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedIP { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string ModifiedIP { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? bdeleted { get; set; }

    }
}

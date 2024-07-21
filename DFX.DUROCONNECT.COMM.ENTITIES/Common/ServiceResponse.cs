using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DFX.DUROCONNECT.COMM.ENTITIES.Common
{
    public class ServiceResponse<T>
    {
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the errcode.
        /// </summary>
        [DataMember]
        public int Errcode { get; set; }

        /// <summary>
        /// Gets or sets the errdesc.
        /// </summary>
        [DataMember]
        public string Errdesc { get; set; }

        /// <summary>
        /// Gets or sets the object param.
        /// </summary>
        [DataMember]
        public T ObjectParam { get; set; }

        /// <summary>
        /// Gets or sets the additional param.
        /// </summary>
        [DataMember]
        public string AdditionalParam { get; set; }

        /// <summary>
        /// Gets or sets the is confirmation id.
        /// </summary>
        [DataMember]
        public int IsConfirmationId { get; set; }
        [DataMember]
        public bool IsSuccess { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}

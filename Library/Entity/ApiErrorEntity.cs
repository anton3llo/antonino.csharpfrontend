using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HendoHealth.Library.Entity
{
    [DataContract]
    public class ApiErrorEntity
    {
        /// <summary>
        /// Error type
        /// </summary>
        [DataMember]
        public string Error { get; set; }
        /// <summary>
        /// Error description
        /// </summary>
        [DataMember]
        public string ErrorDescription { get; set; }
        /// <summary>
        /// Error code
        /// </summary>
        [DataMember]
        public int ErrorCode { get; set; }

        public ApiErrorEntity(string aError, string aErrorDescription, int aErrorCode)
        {
            this.Error = aError;
            this.ErrorCode = aErrorCode;
            this.ErrorDescription = aErrorDescription;
        }
    }
}
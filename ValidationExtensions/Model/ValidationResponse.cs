using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValidationExtensions.Enums;

namespace ValidationExtensions.Model
{
    public class ValidationResponse
    {
        public CertificateStatus status { get; set; }
        public DateTime revokationDate { get; set; }

        public ValidationResponse()
        { }

        public ValidationResponse(CertificateStatus status)
        {
            this.status = status;
        }

        public ValidationResponse(CertificateStatus status, DateTime revokationDate)
        {
            this.status = status;
            this.revokationDate = revokationDate;
        }
    }
}

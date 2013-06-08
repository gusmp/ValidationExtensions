using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationExtensions.Enums
{
    public enum CertificateStatus
    {
        VALID,
        SUSPENSED,
        REVOKED,
        EXPIRED,
        NOT_VALID_YET,
        UNKNOWN
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValidationExtensions.Enums;
using ValidationExtensions.Model;

namespace ValidationExtensions.Service.ExpiryDate.Impl
{
    public class ExpiryDateServiceImpl : ExpiryDateService
    {
        public ValidationResponse ValidateCertificate(System.Security.Cryptography.X509Certificates.X509Certificate2 certificate)
        {
            DateTime now = DateTime.Now;

            if (now.CompareTo(certificate.NotBefore) < 0)
            {
                return new ValidationResponse(CertificateStatus.NOT_VALID_YET);
            }
            else if (now.CompareTo(certificate.NotAfter) > 0)
            {
                return new ValidationResponse(CertificateStatus.EXPIRED);
            }

            return new ValidationResponse(CertificateStatus.VALID);
        }
    }
}

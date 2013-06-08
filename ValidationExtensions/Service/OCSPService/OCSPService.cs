using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValidationExtensions.Enums;
using System.Security.Cryptography.X509Certificates;
using ValidationExtensions.Model;

namespace ValidationExtensions.Service.OCSPService
{
    public interface OCSPService
    {
        ValidationResponse ValidateCertificate(X509Certificate2 certificate, X509Certificate2 issuer);
        ValidationResponse ValidateCertificate(X509Certificate2 certificate, X509Certificate2 issuer, String urlOCSP);
        ValidationResponse ValidateCertificate(string serialNumber, X509Certificate2 issuer, String urlOCSP);
    }
}

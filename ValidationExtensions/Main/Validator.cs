using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValidationExtensions.Model;
using System.Security.Cryptography.X509Certificates;
using ValidationExtensions.Service.OCSPService;
using ValidationExtensions.Service.OCSPService.Impl;

namespace ValidationExtensions.Main
{
    public class Validator
    {
        private static OCSPService ocspService = new OCSPServiceImpl();

        /// <summary>
        /// Validate a certificate againts a OCSP server. Use this methods to validate a 
        /// certificate without creating an X509Certificate2 object.
        /// </summary>
        /// <param name="serialNumber">Serial number of the certificate in hexadecimal</param>
        /// <param name="issuer">Certificate which issues the certificate to validate.</param>
        /// <param name="urlOCSP">Url of the ocsp server</param>
        /// <returns>ValidationResponse enum with the status</returns>
        public static ValidationResponse ValidateCertificate(string serialNumber, X509Certificate2 issuer, string urlOCSP)
        {
            return ocspService.ValidateCertificate(serialNumber, issuer, urlOCSP);
        }
    }
}

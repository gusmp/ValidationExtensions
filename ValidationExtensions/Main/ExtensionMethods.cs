using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValidationExtensions.Enums;
using System.Security.Cryptography.X509Certificates;
using ValidationExtensions.Service.ExpiryDate;
using ValidationExtensions.Service.ExpiryDate.Impl;
using ValidationExtensions.Service.CRLService;
using ValidationExtensions.Service.CRLService.Impl;
using ValidationExtensions.Service.OCSPService;
using ValidationExtensions.Service.OCSPService.Impl;
using ValidationExtensions.Model;

namespace ValidationExtensions.Main
{
    public static class ExtensionMethods
    {
        private static ExpiryDateService expiryDateService = new ExpiryDateServiceImpl();
        private static CRLService crlService = new CRLServiceImpl();
        private static OCSPService ocspService = new OCSPServiceImpl();

        /// <summary>
        /// Validate if a certificate is valid according to the current date ONLY.
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns>ValidationResponse enum with the status</returns>
        public static ValidationResponse ValidateExpiryDate(this X509Certificate2 certificate)
        {
            return expiryDateService.ValidateCertificate(certificate);
        }

        /// <summary>
        /// Validate if a certificate is valid against a CRL. The CRL is obtained from the CRL 
        /// distribuition point extenson. If this extension is not present, this method returns UNKNOWN.
        /// NOTE: This method has to be used in conjuction to ValidateExpiryDate due to issuers take out
        /// expired certificates form CRL to avoid an unlimited growth of it.
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns>ValidationResponse enum with the status. Additionally returns the revocation date.</returns>
        public static ValidationResponse ValidateWithCRL(this X509Certificate2 certificate)
        {
            return crlService.ValidateCertificate(certificate);
        }

        /// <summary>
        /// Validate if a certificate is valid against a CRL. This method has to be used in conjuction 
        /// to ValidateExpiryDate due to issuers take out expired certificates form CRL to avoid an unlimited 
        /// growth of it.
        /// </summary>
        /// <param name="certificate"></param>
        /// <param name="urlCRL">Url of the crl to be used</param>
        /// <returns>ValidationResponse enum with the status. Additionally returns the revocation date.</returns>
        public static ValidationResponse ValidateWithCRL(this X509Certificate2 certificate, String urlCRL)
        {
            return crlService.ValidateCertificate(certificate, urlCRL);
        }

        /// <summary>
        /// Validate if the certificate is valid against a OCSP server. The url is taken form the access 
        /// information access extension. If this extension is not available, UNKOWN is returned.
        /// </summary>
        /// <param name="certificate"></param>
        /// <param name="issuer">Certificate's issuer</param>
        /// <returns>ValidationResponse enum with the status</returns>
        public static ValidationResponse ValidateWithOCSP(this X509Certificate2 certificate, X509Certificate2 issuer)
        {
            return ocspService.ValidateCertificate(certificate, issuer);
        }

        /// <summary>
        /// Validate if the certificate is valid against a OCSP server. 
        /// </summary>
        /// <param name="certificate"></param>
        /// <param name="issuer">Certificate's issuer</param>
        /// <param name="urlOCSP">Url of the ocsp server</param>
        /// <returns>ValidationResponse enum with the status</returns>
        public static ValidationResponse ValidateWithOCSP(this X509Certificate2 certificate, X509Certificate2 issuer, String urlOCSP)
        {
            return ocspService.ValidateCertificate(certificate, issuer, urlOCSP);
        }
    }
}

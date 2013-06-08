using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValidationExtensions.Enums;
using System.Security.Cryptography.X509Certificates;
using ValidationExtensions.Service.TransferDataService.Impl;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.X509;
using ValidationExtensions.Model;
using Org.BouncyCastle.Asn1;
using ValidationExtensions.Exception;

namespace ValidationExtensions.Service.CRLService.Impl
{
    public class CRLServiceImpl : CRLService
    {
        private static TransferDataService.TransferDataService transferHttpDataService = new TransferHttpDataServiceImpl();

        public ValidationResponse ValidateCertificate(X509Certificate2 certificate)
        {
            Org.BouncyCastle.X509.X509Certificate certificateBC = Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(certificate);

            try
            {
                Asn1Object crlDpAsn1 = Asn1Object.FromByteArray(certificateBC.GetExtensionValue(new DerObjectIdentifier("2.5.29.31")).GetOctets());
                CrlDistPoint crlDistributionPoint = CrlDistPoint.GetInstance(crlDpAsn1);
                foreach (DistributionPoint crlDp in crlDistributionPoint.GetDistributionPoints())
                {
                    GeneralNames gns = (GeneralNames)crlDp.DistributionPointName.Name;
                    foreach (GeneralName gn in gns.GetNames())
                    {
                        ValidationResponse validationResponse = ValidateCertificate(certificate, gn.Name.ToString());
                        if ((validationResponse.status == CertificateStatus.VALID) || (validationResponse.status == CertificateStatus.REVOKED))
                        {
                            return validationResponse;
                        }
                    }
                }
            }
            catch (NullReferenceException) 
            { 
                // No Crl distribution points in the certificate
            }

            return new ValidationResponse(CertificateStatus.UNKNOWN);
        }

        public ValidationResponse ValidateCertificate(X509Certificate2 certificate, String urlCRL)
        {
            try
            {
                byte[] crl = transferHttpDataService.GetFile(urlCRL);
                X509Crl x509crl = new X509CrlParser().ReadCrl(crl);
                Org.BouncyCastle.X509.X509Certificate certificateBC = Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(certificate);
                X509CrlEntry crlEntry = x509crl.GetRevokedCertificate(certificateBC.SerialNumber);
                if (crlEntry != null)
                {
                    return new ValidationResponse(CertificateStatus.REVOKED, crlEntry.RevocationDate);
                }

                return new ValidationResponse(CertificateStatus.VALID);
            }
            catch (CommunicationException)
            {
                return new ValidationResponse(CertificateStatus.UNKNOWN);
            }
        }
    }
}

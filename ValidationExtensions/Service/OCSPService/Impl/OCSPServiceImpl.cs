using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValidationExtensions.Enums;
using System.Security.Cryptography.X509Certificates;
using ValidationExtensions.Model;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System.Collections;
using Org.BouncyCastle.Math;
using System.IO;
using ValidationExtensions.Service.TransferDataService.Impl;

namespace ValidationExtensions.Service.OCSPService.Impl
{
    public class OCSPServiceImpl : OCSPService
    {
        private static TransferDataService.TransferDataService transferHttpDataService = new TransferHttpDataServiceImpl();

        public ValidationResponse ValidateCertificate(X509Certificate2 certificate, X509Certificate2 issuer)
        {
            Org.BouncyCastle.X509.X509Certificate certificateBC = Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(certificate);
            try
            {
                Asn1Object derAiaExtension = Asn1Object.FromByteArray(certificateBC.GetExtensionValue(new DerObjectIdentifier("1.3.6.1.5.5.7.1.1")).GetOctets());
                Asn1InputStream asn1Stream = new Asn1InputStream(derAiaExtension.GetDerEncoded());
                Asn1Sequence asn1Sequence = (Asn1Sequence)asn1Stream.ReadObject();

                foreach (Asn1Encodable entry in asn1Sequence)
                {
                    AccessDescription aiaEntry = AccessDescription.GetInstance(entry.ToAsn1Object());
                    if (aiaEntry.AccessMethod.Id == AccessDescription.IdADOcsp.Id)
                    {
                        Console.Out.WriteLine(aiaEntry.AccessLocation.ToString());
                        GeneralName gn = (GeneralName)aiaEntry.AccessLocation;
                        ValidationResponse validationResponse = ValidateCertificate(certificate, issuer,gn.Name.ToString());
                        if ((validationResponse.status == ValidationExtensions.Enums.CertificateStatus.VALID) ||
                            (validationResponse.status == ValidationExtensions.Enums.CertificateStatus.REVOKED))
                        {
                            return validationResponse;
                        }
                    }
                }
            }
            catch (NullReferenceException)
            { 
                // No Access Information Exception
            }

            return new ValidationResponse(ValidationExtensions.Enums.CertificateStatus.UNKNOWN);
        }

        public ValidationResponse ValidateCertificate(X509Certificate2 certificate, X509Certificate2 issuer, string urlOCSP)
        {
            return ValidateCertificate(certificate.SerialNumber, issuer, urlOCSP);
        }

        public ValidationResponse ValidateCertificate(string serialNumber, X509Certificate2 issuer, String urlOCSP)
        {
            try
            {
                OcspReqGenerator ocspReqGenerator = new OcspReqGenerator();
                ocspReqGenerator.AddRequest(new CertificateID(CertificateID.HashSha1,
                    Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(issuer),
                    new BigInteger(serialNumber, 16)));

                // Extensions
                IList oidList = new ArrayList();
                IList valueList = new ArrayList();

                // nonce
                byte[] nonce = new byte[16];
                Random rand = new Random();
                rand.NextBytes(nonce);
                oidList.Add(OcspObjectIdentifiers.PkixOcspNonce);
                valueList.Add(new Org.BouncyCastle.Asn1.X509.X509Extension(false, new DerOctetString(nonce)));
                ocspReqGenerator.SetRequestExtensions(new X509Extensions(oidList, valueList));

                // requestor name
                ocspReqGenerator.SetRequestorName(new GeneralName(GeneralName.DirectoryName, new X509Name(issuer.Subject)));

                OcspReq ocspReq = ocspReqGenerator.Generate();

                OcspResp ocspResponse = new OcspResp(transferHttpDataService.SendOcspRequest(urlOCSP, ocspReq.GetEncoded()));
                if (ocspResponse.Status == OcspResponseStatus.Successful)
                {
                    BasicOcspResp ocspBasicResponse = (BasicOcspResp)ocspResponse.GetResponseObject();
                    if (ocspBasicResponse.Responses[0].GetCertStatus() == Org.BouncyCastle.Ocsp.CertificateStatus.Good)
                    {
                        return new ValidationResponse(ValidationExtensions.Enums.CertificateStatus.VALID);
                    }
                    else if (ocspBasicResponse.Responses[0].GetCertStatus().GetType() == typeof(RevokedStatus))
                    {
                        return new ValidationResponse(ValidationExtensions.Enums.CertificateStatus.REVOKED);
                    }
                    // Default case
                    //else if (ocspBasicResponse.Responses[0].GetCertStatus().GetType() == typeof(UnknownStatus))
                    //{ }
                }
            }
            catch (System.Exception) { }

            return new ValidationResponse(ValidationExtensions.Enums.CertificateStatus.UNKNOWN);
        }
    }
}

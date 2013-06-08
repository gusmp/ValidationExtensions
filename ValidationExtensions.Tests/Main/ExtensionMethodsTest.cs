using ValidationExtensions.Main;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ValidationExtensions.Enums;
using System.Security.Cryptography.X509Certificates;
using ValidationExtensions.Model;

namespace ValidationExtensions.Tests.Main
{
    
    [TestClass()]
    public class ExtensionMethodsTest : BaseTest
    {
        public ExtensionMethodsTest()
        {
            certificateTestEC_ACC = new X509Certificate2(getCurrentAssemblyPath() + certificateNameEC_ACC);
            certificateTestEC_IDCAT = new X509Certificate2(getCurrentAssemblyPath() + certificateNameEC_IDCAT);
            
            certificateTestFP_ROOT = new X509Certificate2(getCurrentAssemblyPath() + certificateNameFP_ROOT);
            certificateTestFP_INFRAESTRUCTURA = new X509Certificate2(getCurrentAssemblyPath() + certificateNameFP_INFRAESTRUCTURA);
            
            certificateTestIMI = new X509Certificate2(getCurrentAssemblyPath() + certificateNameIMI);

            certificateTestIZENPE_ROOT = new X509Certificate2(getCurrentAssemblyPath() + certificateNameIZENPE_ROOT);
            certificateTestIZENPE_INFRAESTRUCTURA = new X509Certificate2(getCurrentAssemblyPath() + certificateNameIZENPE_INFRAESTRUCTURA);
        }

        [TestMethod()]
        public void validateExpiryDateTest()
        {
            ValidationResponse validationResponse = certificateTestEC_IDCAT.ValidateExpiryDate();
            Assert.AreEqual(CertificateStatus.VALID, validationResponse.status);
        }

        [TestMethod()]
        public void validateWithCRLTest()
        {
            ValidationResponse validationResponse = certificateTestEC_IDCAT.ValidateWithCRL();
            Assert.AreEqual(CertificateStatus.VALID, validationResponse.status);
        }

        [TestMethod()]
        public void validateWithCRL_WithHttpUrlCrl_Test()
        {
            ValidationResponse validationResponse = certificateTestEC_IDCAT.ValidateWithCRL(httpUrlCRL);
            Assert.AreEqual(CertificateStatus.VALID, validationResponse.status);
        }

        [TestMethod()]
        public void validateWithCRL_WitHttpsUrlCrl_Test()
        {
            ValidationResponse validationResponse = certificateTestFP_INFRAESTRUCTURA.ValidateWithCRL(httpsUrlCRL);
            Assert.AreEqual(CertificateStatus.VALID, validationResponse.status);
        }

        [TestMethod()]
        public void validateWithCRL_WithNoCRLDP_Test()
        {
            ValidationResponse validationResponse = certificateTestIMI.ValidateWithCRL();
            Assert.AreEqual(CertificateStatus.UNKNOWN, validationResponse.status);
        }

        [TestMethod()]
        public void validateWithOCSP_Valid_CertificateTest()
        {
            ValidationResponse validationResponse = certificateTestFP_INFRAESTRUCTURA.ValidateWithOCSP(certificateTestFP_ROOT);
            Assert.AreEqual(CertificateStatus.VALID, validationResponse.status);
        }

        [TestMethod()]
        public void validateWithOCSP_No_Default_Port_Valid_CertificateTest()
        {
            // http://ocsp.izenpe.com:8094
            ValidationResponse validationResponse = certificateTestIZENPE_INFRAESTRUCTURA.ValidateWithOCSP(certificateTestIZENPE_ROOT);
            Assert.AreEqual(CertificateStatus.VALID, validationResponse.status);
        }

        [TestMethod()]
        public void validateWithOCSP_WithHttpUrlOcsp_Test()
        {
            ValidationResponse validationResponse = certificateTestEC_IDCAT.ValidateWithOCSP(certificateTestEC_ACC,httpUrlCatcertOCSP);
            Assert.AreEqual(CertificateStatus.VALID, validationResponse.status);
        }
        
        [TestMethod()]
        public void validateWithOCSP_WithNoAIA_Test()
        {
            ValidationResponse validationResponse = certificateTestEC_IDCAT.ValidateWithOCSP(certificateTestEC_ACC);
            Assert.AreEqual(CertificateStatus.UNKNOWN, validationResponse.status);
        }
    }
}

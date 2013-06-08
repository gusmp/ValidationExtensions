using ValidationExtensions.Main;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Cryptography.X509Certificates;
using ValidationExtensions.Model;
using ValidationExtensions.Enums;

namespace ValidationExtensions.Tests.Main
{
    

    [TestClass()]
    public class ValidatorTest : BaseTest
    {
        public ValidatorTest()
        {
            certificateTestEC_ACC = new X509Certificate2(getCurrentAssemblyPath() + certificateNameEC_ACC);
        }

        [TestMethod()]
        public void ValidateCertificateTest()
        {
            ValidationResponse validationResponse = Validator.ValidateCertificate("4eb36670f9251d5f4cbc659495136c6b", certificateTestEC_ACC, httpUrlCatcertOCSP);
            Assert.AreEqual(CertificateStatus.REVOKED, validationResponse.status);
        }
    }
}

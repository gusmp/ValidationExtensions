using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace ValidationExtensions.Tests.Main
{
    public class BaseTest
    {
        protected const String certificateNameEC_ACC = @"..\..\support\ec-acc.cer";
        protected const String certificateNameEC_IDCAT = @"..\..\support\ec-idcat.cer";

        protected const String certificateNameFP_ROOT = @"..\..\support\FPROOT.cer";
        protected const String certificateNameFP_INFRAESTRUCTURA = @"..\..\support\fp_infraestructura.cer";

        protected const String certificateNameIMI = @"..\..\support\imiCA.cer";

        protected const String certificateNameIZENPE_ROOT = @"..\..\support\izenpe_root.crt";
        protected const String certificateNameIZENPE_INFRAESTRUCTURA = @"..\..\support\izenpe_ciudadanos_entidades.crt";

        protected const String httpUrlCRL = "http://epscd.catcert.net/crl/ec-acc.crl";
        protected const String httpsUrlCRL = "https://crl.firmaprofesional.com/firmaprofesional1.crl";

        protected const String httpUrlCatcertOCSP = "http://ocsp.catcert.net";
        protected const String httpUrlFPOCSP = "http://ocsp.firmaprofesional.com";

        protected X509Certificate2 certificateTestEC_ACC;
        protected X509Certificate2 certificateTestEC_IDCAT;

        protected X509Certificate2 certificateTestFP_ROOT;
        protected X509Certificate2 certificateTestFP_INFRAESTRUCTURA;

        protected X509Certificate2 certificateTestIZENPE_ROOT;
        protected X509Certificate2 certificateTestIZENPE_INFRAESTRUCTURA;

        protected X509Certificate2 certificateTestIMI;

        protected String getCurrentAssemblyPath()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".dll", "");
        }
    }
}

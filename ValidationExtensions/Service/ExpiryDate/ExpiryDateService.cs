using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ValidationExtensions.Enums;
using System.Security.Cryptography.X509Certificates;
using ValidationExtensions.Model;

namespace ValidationExtensions.Service.ExpiryDate
{
    public interface ExpiryDateService
    {
        ValidationResponse ValidateCertificate(X509Certificate2 certificate);
    }
}

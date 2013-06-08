using ValidationExtensions.Service.TransferDataService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ValidationExtensions.Service.TransferDataService.Impl;

namespace ValidationExtensions.Tests.Service.TransferData
{
    
    [TestClass()]
    public class TransferHttpDataServiceTest
    {
        private static ValidationExtensions.Service.TransferDataService.TransferDataService fileHttpService = new TransferHttpDataServiceImpl(); 
        private const string fileName = "http://epscd.catcert.net/crl/ec-acc.crl";

        [TestMethod()]
        public void getFileTest()
        {
            byte[] file = fileHttpService.GetFile(fileName);
            Assert.IsNotNull(file);
        }
    }
}

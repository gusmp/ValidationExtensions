using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidationExtensions.Service.TransferDataService
{
    public interface TransferDataService
    {
        byte[] GetFile(String fileName);
        byte[] SendOcspRequest(String url, byte[] data);
    }
}

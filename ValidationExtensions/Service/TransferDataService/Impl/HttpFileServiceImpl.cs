using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using ValidationExtensions.Exception;

namespace ValidationExtensions.Service.TransferDataService.Impl
{

    public class TransferHttpDataServiceImpl : TransferDataService
    {
        
        private WebClient webClient = new WebClient();

        public byte[] GetFile(string fileName)
        {
            try
            {
                return webClient.DownloadData(fileName);
            }
            catch (WebException exc)
            {
                throw new CommunicationException(exc);
            }
        }

        public byte[] SendOcspRequest(String url, byte[] data)
        {

            //Do not work: webClient.UploadData(url, data);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "application/ocsp-request";
            webRequest.Method = "POST";
            webRequest.ContentLength = data.Length;

            Stream outStream = webRequest.GetRequestStream();
            outStream.Write(data, 0, data.Length);

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();

            MemoryStream memStream = new MemoryStream();
            const int BUFFER_SIZE = 1000;
            byte[] buffer = new byte[BUFFER_SIZE];
            int bufferSize = 0;
            while ((bufferSize = response.GetResponseStream().Read(buffer, 0, BUFFER_SIZE)) != 0)
            {
                memStream.Write(buffer, 0, bufferSize);
            }

            return memStream.ToArray();
        }
    }
}


using System;
using System.Collections.Generic;


namespace Tools.Web
{
    

    class DownloadHelper
    {


        public static string ToCanonicalUrl(string relativeUrl)
        {
            string strScheme = System.Web.HttpContext.Current.Request.Url.Scheme;
            string strAuth = System.Web.HttpContext.Current.Request.Url.Authority;
            string strCanonicalBase = strScheme + System.Uri.SchemeDelimiter + strAuth;

            string strAbsoluteURL = Portal.ASP.NET.ContentUrl(relativeUrl);
            return strCanonicalBase + strAbsoluteURL;


            //return string.Format("http{0}://{1}{2}",
            //    (System.Web.HttpContext.Current.Request.IsSecureConnection) ? "s" : ""
            //    , System.Web.HttpContext.Current.Request.Url.Host
            //    //,Page.ResolveUrl(relativeUrl)
            //    , ((System.Web.UI.Page)System.Web.HttpContext.Current.Handler).ResolveUrl(relativeUrl)
            //);
        } // End Function ToCanonicalUrl


        public static byte[] DownloadFile(string strURL)
        {
            byte[] imageBytes = null;
            System.Net.WebClient webClient = new System.Net.WebClient();
            imageBytes = webClient.DownloadData(strURL);

            return imageBytes;
        } // End Function DownloadFile


        // ImageToByteArray(img, System.Drawing.Imaging.ImageFormat.Gif);
        public static byte[] ImageToByteArray(System.Drawing.Image img, System.Drawing.Imaging.ImageFormat imgFormat)
        {
            byte[] baImageData = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                img.Save(ms, imgFormat);
                baImageData = ms.ToArray();
            } // End Using System.IO.MemoryStream ms

            return baImageData;
        } // End Function ImageToByteArray


        public static System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
        {
            System.Drawing.Image returnImage = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArrayIn))
            {
                returnImage = System.Drawing.Image.FromStream(ms);
            } // End Using System.IO.MemoryStream ms
            
            return returnImage;
        } // End Function ByteArrayToImage


        public static DateTime GetLastModifiedDate(string strURL)
        {
            DateTime LastModified;

            System.Net.WebRequest req = System.Net.HttpWebRequest.Create(strURL);
            req.Method = "HEAD";
            using (System.Net.WebResponse resp = req.GetResponse())
            {

                if (DateTime.TryParse(resp.Headers.Get("Last-Modified"), out LastModified))
                {
                    //Check if date is good and then go to full download method.
                } // End if (DateTime.TryParse(resp.Headers.Get("Last-Modified"), out LastModified))

            } // End Using resp

            return LastModified;
        } // End Function GetLastModifiedDate


        // Header Beispiel
        //Content-Location: http://www.cor-management.ch/corwebsite/pictures/Titelbild_01.jpg
        //Accept-Ranges: bytes
        //Content-Length: 18509
        //Content-Type: image/jpeg
        //Date: Wed, 29 Jan 2014 14:18:21 GMT
        //ETag: "a885ab326ebdcb1:c56"
        //Last-Modified: Wed, 26 Jan 2011 15:32:10 GMT
        //Server: Microsoft-IIS/6.0
        //X-Powered-By: ASP.NET
        public static string GetHeaders(string strURL)
        {
            string strHeaderText = "";

            System.Net.WebRequest req = System.Net.HttpWebRequest.Create(strURL);
            req.Method = "HEAD";
            using (System.Net.WebResponse resp = req.GetResponse())
            {
                foreach (string strHeader in resp.Headers.AllKeys)
                {
                    strHeaderText += strHeader + ": " + resp.Headers[strHeader] + Environment.NewLine;
                } // Next strHeader
            } // End Using resp

            return strHeaderText;
        } // End Function GetHeaders


        public static byte[] StreamToByteArray(System.IO.Stream strmInput)
        {
            byte[] buffer = new byte[16 * 1024];
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                int read;
                while ((read = strmInput.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                } // Whend
                return ms.ToArray();
            } // End Using ms

        } // End Function StreamToByteArray


        protected static void WriteBufferToStream(System.IO.Stream strmData, byte[] baBuffer, int iStart, int iSize)
        {
            lock (strmData)
            {
                strmData.Seek(iStart, System.IO.SeekOrigin.Begin);
                strmData.Write(baBuffer, 0, iSize);
            } // End lock (strmData)

        } // End Sub WriteBufferToStream


        // http://stackoverflow.com/questions/1366848/httpwebrequest-getresponse-throws-webexception-on-http-304
        public static System.Net.HttpWebResponse GetHttpResponse(System.Net.HttpWebRequest request)
        {
            try
            {
                return (System.Net.HttpWebResponse)request.GetResponse();
            }
            catch (System.Net.WebException ex)
            {
                if (ex.Response == null || ex.Status != System.Net.WebExceptionStatus.ProtocolError)
                    throw;

                return (System.Net.HttpWebResponse)ex.Response;
            }
        } // End Function GetHttpResponse


        public static byte[] GetFileAsByteArray(string strURL)
        {
            byte[] baFile = null;

            System.Net.HttpWebRequest hwrRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strURL);
            //System.Net.HttpWebResponse wrResponse = (System.Net.HttpWebResponse)hwrRequest.GetResponse();
            System.Net.HttpWebResponse wrResponse = GetHttpResponse(hwrRequest);

            if (hwrRequest.HaveResponse)
            {

                if (wrResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    using (System.IO.MemoryStream msTempStream = new System.IO.MemoryStream())
                    {

                        using (System.IO.Stream strmResponse = wrResponse.GetResponseStream())
                        {
                            int iTotalBytes = 0;
                            const int iBufferLength = 256;
                            byte[] baBuffer = new byte[iBufferLength];
                            int iBytesRead = strmResponse.Read(baBuffer, 0, iBufferLength);
                            while (iBytesRead > 0)
                            {
                                WriteBufferToStream(msTempStream, baBuffer, iTotalBytes, iBytesRead);
                                iTotalBytes += iBytesRead;
                                iBytesRead = strmResponse.Read(baBuffer, 0, iBufferLength);
                            } // Whend

                            strmResponse.Close();
                        } // End Using strmResponse 

                        wrResponse.Close();
                        baFile = msTempStream.ToArray();
                        
                        // System.IO.File.WriteAllBytes(@"d:\test.jpg", baFile);
                        msTempStream.Close();
                    } // End Using msTempStream

                } // End if (wrResponse.StatusCode == System.Net.HttpStatusCode.OK)

            } // End if (hwrRequest.HaveResponse)

            return baFile;
        } // End Function GetFileAsByteArray


        public static void SaveFileToPath(string strURL, string strPath)
        {
            System.Net.HttpWebRequest hwrRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strURL);
            //System.Net.HttpWebResponse wrResponse = (System.Net.HttpWebResponse)hwrRequest.GetResponse();
            System.Net.HttpWebResponse wrResponse = GetHttpResponse(hwrRequest);

            if (hwrRequest.HaveResponse)
            {
                if (wrResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    using (System.IO.FileStream fsFileStream = new System.IO.FileStream(strPath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    {

                        using (System.IO.Stream strmResponse = wrResponse.GetResponseStream())
                        {
                            int iTotalBytes = 0;
                            const int iBufferLength = 256;
                            byte[] baBuffer = new byte[iBufferLength];
                            int iBytesRead = strmResponse.Read(baBuffer, 0, iBufferLength);
                            while (iBytesRead > 0)
                            {
                                WriteBufferToStream(fsFileStream, baBuffer, iTotalBytes, iBytesRead);
                                iTotalBytes += iBytesRead;
                                iBytesRead = strmResponse.Read(baBuffer, 0, iBufferLength);
                            } // Whend

                            strmResponse.Close();
                        } // End Using strmResponse 

                        wrResponse.Close();
                        fsFileStream.Close();
                    } // End Using System.IO.FileStream fsFileStream 

                } // End if (wrResponse.StatusCode == System.Net.HttpStatusCode.OK)

            } // End if (hwrRequest.HaveResponse)

        } // End Function SaveFileToPath 


    } // End Class DownloadHelper


} // End Namespace Tools.Web

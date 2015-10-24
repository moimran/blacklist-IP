using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlackListFeed
{
    class DownloadFiles
    {
        public void Download(string URL, int j)                                     
        { 
            Console.WriteLine("Task {0} has been created .....",j);
            // Function will return the number of bytes processed
            // to the caller. Initialize to 0 here.
            int bytesProcessed = 0;

            //// Assign values to these objects here so that they can
            //// be referenced in the finally block
            Stream remoteStream = null;
            Stream localStream = null;
            HttpWebResponse response = null;
            string localFilename = null;

            // Use a try/catch/finally block as both the WebRequest and Stream
            // classes throw exceptions upon error
            try
            {
                // Create a request for the specified remote file name
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                if (request != null)
                {
                    // Send the request to the server and retrieve the
                    // WebResponse object 
                    response = (HttpWebResponse) request.GetResponse();
                    if (response != null)
                    {
                        string defaultname = "feed";
                        localFilename = response.Headers["Content-Disposition"] != null ?
                        response.Headers["Content-Disposition"].Replace("attachment;filename=", "").Replace("\"", "") :
                        response.Headers["Location"] != null ? Path.GetFileName(response.Headers["Location"]) :
                        Path.GetFileName(URL).Contains('?') || Path.GetFileName(URL).Contains('=') ?
                        Path.GetFileName(response.ResponseUri.ToString()) : defaultname;
                        if (defaultname == "feed")
                        {
                            int count = URL.Split('/').Count();
                            string Filename = URL.Split('/')[count - 1];
                            if (Filename.Split('.')[1] == "csv" || Filename.Split('.')[1] == "txt" || Filename.Split('.')[1] == "ipset")
                            {
                                string path = Common.GetPath();
                                localFilename = path + Filename;
                            }
                        }
                        // Once the WebResponse object has been retrieved,
                        // get the stream object associated with the response's data
                        //int i = 0;
                        remoteStream = response.GetResponseStream();
                    //    Console.WriteLine("-----------{0}",i);
                    //    i++;
                        // Create the local file
                        localStream = File.Create(localFilename);

                        // Allocate a 1k buffer
                        byte[] buffer = new byte[1024];
                        int bytesRead;
                        //Console.WriteLine("---------{0}",buffer.Count());

                        // Simple do/while loop to read from stream until
                        // no bytes are returned
                        do
                        {
                            // Read data (up to 1k) from the stream
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                            // Write the data to the local file
                            localStream.Write(buffer, 0, bytesRead);

                            // Increment total bytes processed
                            bytesProcessed += bytesRead;
                        } while (bytesRead > 0);                       
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Close the response and streams objects here 
                // to make sure they're closed even if an exception
                // is thrown at some point
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
            }

            // Return total bytes processed to caller.
            Console.WriteLine(" Byets Processed {0}", bytesProcessed);
            Console.WriteLine("Task {0} has been Completed .....",j);
           // Console.ReadLine();
            
        }

        //=======================================================================
        public static void HttpHandler(string URL)
        {
            Stream remoteStream = null;
            // Create a request for the specified remote file name
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Timeout = 3000000;
            request.ReadWriteTimeout = 30000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string defaultname = "feed";
            string localFilename = response.Headers["Content-Disposition"] != null ?
            response.Headers["Content-Disposition"].Replace("attachment;filename=", "").Replace("\"", "") :
            response.Headers["Location"] != null ? Path.GetFileName(response.Headers["Location"]) :
            Path.GetFileName(URL).Contains('?') || Path.GetFileName(URL).Contains('=') ?
            Path.GetFileName(response.ResponseUri.ToString()) : defaultname;
            if (defaultname == "feed")
            {
                 int count = URL.Split('/').Count();
                 string Filename = URL.Split('/')[count - 1];
                 if (Filename.Split('.')[1] == "csv" || Filename.Split('.')[1] == "txt" || Filename.Split('.')[1] == "ipset")
                 {
                    string path = Common.GetPath();
                    localFilename = path + Filename;
                 }
            }
            // Once the WebResponse object has been retrieved,
            // get the stream object associated with the response's data
            remoteStream = response.GetResponseStream();
            Console.WriteLine("stream length = {0}",remoteStream.Length);      
            int bytesProcessed = 0;
            //// Assign values to these objects here so that they can
            //// be referenced in the finally block
            Stream localStream = null;
            //string localFilename = null;

            // Create the local file
            localStream = File.Create(localFilename);

            // Allocate a 1k buffer
            byte[] buffer = new byte[1024];
            int bytesRead;

            // Simple do/while loop to read from stream until
            // no bytes are returned
            do
            {
                // Read data (up to 1k) from the stream
                bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                // Write the data to the local file
                localStream.Write(buffer, 0, bytesRead);

                // Increment total bytes processed
                bytesProcessed += bytesRead;
            } while (bytesRead > 0);                  
            
        }


    }
}

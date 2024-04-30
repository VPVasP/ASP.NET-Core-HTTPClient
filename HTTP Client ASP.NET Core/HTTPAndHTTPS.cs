using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;

namespace HTTP_Client_ASP.NET_Core
{
    public class HTTPAndHTTPS
    {
        //the file output path where the html response is saved to.
        public static string fileOutput = "Output.txt";
        public static HttpResponseMessage? response;
        public static async Task Main(string[] args)
        {
            //enter the url 
            Console.Write("Enter the HTML URL: ");
            string? url = Console.ReadLine();
            if (url != null)
            {
                var htmlResponse = await CallURL(url);

                if (!string.IsNullOrEmpty(htmlResponse))
                {
                    File.WriteAllText(fileOutput, htmlResponse);
                    Console.WriteLine("HTML Response output to " + fileOutput);
                }
                else
                {
                    Console.WriteLine("Failed to retrieve an html response.");
                }

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
        public static async Task<string> CallURL(string url)
        {
            //create a new httpclient instance
            HttpClient client = new HttpClient();

            //clear all the default accept headers
            client.DefaultRequestHeaders.Accept.Clear();

            //check if the URL starts with "http://" or "https://"
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))

            {
                Console.WriteLine("The url is http:");
                //send an httpresponse
                response = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);

                //if the response is succesfull
                if (response.IsSuccessStatusCode)
                {
                    //read the response as a string
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine(response.StatusCode.ToString());
                }
            }
            else if(url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("The url is https:");
                //send an httpresponse
                response = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);

                //if the response is succesfull
                if (response.IsSuccessStatusCode)
                {
                    //read the response as a string
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine(response.StatusCode.ToString());
                }
            }
            //invalid url
            else
            {
                Console.WriteLine("Invalid url.");
                throw new ArgumentException("Invalid url.");
            }
            return response.StatusCode.ToString();
        }
    }
 }
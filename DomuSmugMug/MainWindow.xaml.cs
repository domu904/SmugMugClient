using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows;
using RestSharp;

namespace DomuSmugMug
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PingSmugMugService();
        }

        public void CallSmugMug()
        {            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var user = new User()
                           {
                               UserName = UserNameTextBox.SelectedText,
                               Password = PasswordTextBox.SelectedText
                           };
            PingSmugMugService();
        }

        private static void PingSmugMugService()
        {
            PingSmugMug();
        }

        public static bool PingSmugMug()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["SmugMugServiceURL"]);
            var request = new RestRequest("/", Method.POST);
            request.AddParameter("method", "smugmug.service.ping");
            request.AddParameter("APIKey", ConfigurationManager.AppSettings["SmugMugAPIKey"]);
            request.AddHeader("header", "value");

            var response = client.Execute(request);
            var content = response.Content;

            return !string.IsNullOrEmpty(content);
        }

        static string HttpGet(string url)
        {
            var req = WebRequest.Create(url)
                                 as HttpWebRequest;
            string result = null;
            using (var resp = req.GetResponse()
                                          as HttpWebResponse)
            {
                var reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;
        }

        static string HttpPost(string url, string[] paramName, string[] paramVal)
        {
            var req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            // Build a string with all the params, properly encoded.
            // We assume that the arrays paramName and paramVal are
            // of equal length:
            var paramz = new StringBuilder();
            for (var i = 0; i < paramName.Length; i++)
            {
                paramz.Append(paramName[i]);
                paramz.Append("=");
                paramz.Append(HttpUtility.UrlEncode(paramVal[i]));
                paramz.Append("&");
            }

            // Encode the parameters as form data:
            var formData = UTF8Encoding.UTF8.GetBytes(paramz.ToString());
            req.ContentLength = formData.Length;

            // Send the request:
            using (var post = req.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            // Pick up the response:
            string result = null;
            using (var resp = req.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }

            return result;
        }

    }

    public class User
    {
        private string apiKey;
        private string myPvtKey;

        public User()
        {
            apiKey = ConfigurationManager.AppSettings["SmugMugAPIKey"];
            myPvtKey = ConfigurationManager.AppSettings["myPvtKey"];
        }

        public string UserName { get; set; }
        public string Password { get; set; }        
    }
}

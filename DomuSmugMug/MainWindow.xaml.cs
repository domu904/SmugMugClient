using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RestSharp;

namespace DomuSmugMug
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            CallRequest();

        }

        private void CallRequest()
        {
            var client = new RestClient("http://api.smugmug.com/services/api/rest/1.3.0/");


            var request = new RestRequest("resource/{id}", Method.POST);
            request.AddParameter("method", "smugmug.service.ping"); 
            request.AddUrlSegment("method", "smugmug.service.ping");
            request.AddUrlSegment("APIKey", ConfigurationManager.AppSettings["myKey"]);

            request.AddHeader("header", "value");

            // execute the request
            var response = client.Execute(request);
            var content = response.Content; 
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
        private string myKey;
        private string myPvtKey;

        public User()
        {
            myKey = ConfigurationManager.AppSettings["myKey"];
            myPvtKey = ConfigurationManager.AppSettings["myPvtKey"];
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        
    }
}

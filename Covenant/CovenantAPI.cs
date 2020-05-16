using System;
using System.Text;
using System.Net;
using System.IO;
using System.Text.Json;

namespace Covenant
{
    class CovenantAPI
    {
        public static string DoStuff(string _CovenantURL, string _Username, string _Password)
        {
            creds Credentials = new creds();
            Credentials.password = _Password;
            Credentials.username = _Username;

            string jsonstring;
            jsonstring = JsonSerializer.Serialize(Credentials);

            Console.WriteLine("Authorizing to Covenant...");
            AuthorizeToken authentication = JsonSerializer.Deserialize<AuthorizeToken>(CovenantAPI.Authenticate(_CovenantURL ,jsonstring));
            Console.WriteLine("Generating Binary Launcher...");
            BinaryLauncher Grunt = JsonSerializer.Deserialize<BinaryLauncher>(CovenantAPI.GetBinaryLaucher(_CovenantURL, "Bearer " + authentication.covenantToken));
            //Console.WriteLine(authentication.covenantToken);

            //Console.WriteLine(Grunt.launcherString);

            return Grunt.launcherString;

        }
        static string Authenticate(string CovenantURL, string data)
        {
            var baseAddress = CovenantURL + "/api/users/login";
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(data);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();
            return content;
        }

        static string GetBinaryLaucher(string CovenantURL, string token)
        {
            var baseAddress = CovenantURL + "/api/launchers/binary";
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "GET";
            http.Headers.Add("Authorization", token);
            string binarystager;

            using (HttpWebResponse response = (HttpWebResponse)http.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                binarystager = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }

            return binarystager;
        }
    } 
}

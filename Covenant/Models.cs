
namespace Covenant
{
    public class AuthorizeToken
    {
        public bool success { get; set; }
        public string covenantToken { get; set; }
    }

    public class creds
    {
        public string username { get; set; }
        public string password { get; set; }
    }

 
    public class BinaryLauncher
    {
        public int id { get; set; }
        public int listenerId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int dotNetFrameworkVersion { get; set; }
        public int type { get; set; }
        public int implantTemplateId { get; set; }
        public bool validateCert { get; set; }
        public bool useCertPinning { get; set; }
        public string smbPipeName { get; set; }
        public int delay { get; set; }
        public int jitterPercent { get; set; }
        public int connectAttempts { get; set; }
        public string killDate { get; set; }
        public string launcherString { get; set; }
        public string stagerCode { get; set; }
        public string base64ILByteString { get; set; }
        public int outputKind { get; set; }
        public bool compressStager { get; set; }
    }
}

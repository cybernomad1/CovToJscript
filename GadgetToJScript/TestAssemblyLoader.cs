using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;

namespace GadgetToJScript
{
    class TestAssemblyLoader
    {
        public static Assembly compile()
        {

            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.CompilerOptions = "/unsafe";

            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("System.Runtime.InteropServices.dll");
            parameters.ReferencedAssemblies.Add("System.EnterpriseServices.dll");
            parameters.ReferencedAssemblies.Add("System.IO.Compression.dll");

            string currentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string filePath = System.IO.Path.Combine(currentDirectory, "", "payload.txt");
            CompilerResults results = provider.CompileAssemblyFromFile(parameters, filePath);
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    sb.AppendLine(String.Format("Error ({0}): {1}: {2}", error.ErrorNumber, error.ErrorText, error.Line));
                }

                throw new InvalidOperationException(sb.ToString());
            }

            Assembly _compiled = results.CompiledAssembly;

            return _compiled;
        }

    }
}
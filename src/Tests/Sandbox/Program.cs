namespace Sandbox
{
    using System;
    using System.IO;
    using System.Reflection;
    using CasesNET.Web;


    public static class Program
    {
        public static void Main(string[] args)
        {
            var path = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            var comb = Path.GetFullPath(Path.Combine(path,@"..\..\..\..\"));
            Console.WriteLine(path);
            Console.WriteLine(comb );
        }
    }
}

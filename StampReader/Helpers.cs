using System;
using System.Diagnostics;
using System.Reflection;

namespace StampReader
{
    internal class Helpers
    {
        public static string Copyright { get; internal set; }
        public static string AppVersion { get; internal set; }
        public static string CompanyName { get; internal set; }
        public static string AppName { get; internal set; }

        public static string Author { get; private set; } = "Jeff Lynch";
        internal static void SetVersioningInfo()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            Copyright = fvi.LegalCopyright;
            AppVersion = fvi.FileVersion;
            CompanyName = fvi.CompanyName;
            AppName = fvi.ProductName;
        }
    }
}
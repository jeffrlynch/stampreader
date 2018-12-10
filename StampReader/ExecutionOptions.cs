using CommandLine;
using CommandLine.Text;

namespace StampReader
{
    public class ExecutionOptions
    {
        [Option('m', "mode",Required=true,HelpText = "Specify Stamp reader action (s (single), m (multi), OR my (missingyear) ")]
        public string Mode { get; set; }
        [Option('i', "input",Required =false,HelpText ="Specify Scott's Number(s) to search for")]
        public string StampsToFind { get; set; }
        [Option('y', "year",Required =false,DefaultValue =0, HelpText ="Specify numerical filter, currently only used for 4 digit Year")]
        public int NumParam { get; set; }
        [Option('j',"jsonOut",DefaultValue =false,Required =false,HelpText ="Create JSON Output")]
        public bool JSonOutput { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo(Helpers.AppName, Helpers.AppVersion),
                Copyright = new CopyrightInfo(Helpers.CompanyName, 2018),
                AddDashesToOption = true
            };
            help.AddPreOptionsLine("Usage:");
            help.AddPreOptionsLine("To list what stamps are still needed for a specif year");
            help.AddPreOptionsLine("\t stampreader -m my -y 1975");
            help.AddPreOptionsLine("\t stampreader -m missingyear -y 1975");
            help.AddPreOptionsLine("To check if a specific Scott's number is already owned, " +
                "will provide details if owned otherwise will provide catalog details to provide " +
                "guidance for purchasing");
            help.AddPreOptionsLine("\t stampreader -m s -i 285");
            help.AddPreOptionsLine("\t stampreader -m single -i 285");
            help.AddPreOptionsLine("To check for multiple Scott's numbers enter multiple Scott Numbers delimited by a semicolon ';' ");
            help.AddPreOptionsLine("\t stampreader -m sm -i 45;678;285");
            help.AddOptions(this);
            return help;
        }
    }
}

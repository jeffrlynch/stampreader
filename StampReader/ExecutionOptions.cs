using CommandLine;
using CommandLine.Text;

namespace StampReader
{
    public class ExecutionOptions
    {
        [Option('m', "mode",Required=true,HelpText = "Specify Stamreader action (s (single),sm (single missing), m (multi), OR my(missingyear) ")]
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
            help.AddPreOptionsLine("Usage: stampreader -m missingyear -y 1975");
            help.AddPreOptionsLine("Check database for missing stamps from year 1975");
            help.AddOptions(this);
            return help;
        }
    }
}

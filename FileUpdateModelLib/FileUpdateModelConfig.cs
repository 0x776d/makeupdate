using ArgumentsLib;

namespace FileUpdateModelLib
{
    public class FileUpdateModelConfig
    {
        public FileUpdateModelConfig(Arguments arguments)
        {
            Source = arguments.GetValue<string>("source");
            Destination = arguments.GetValue<string>("destination");
            Program = arguments.GetValue<string>("program");

            SkipVersionCheck = arguments.GetValue<bool>("skipversion");
            NoBackup = arguments.GetValue<bool>("nobackup");
            NoZip = arguments.GetValue<bool>("nozip");
            StartAfterUpdate = arguments.GetValue<bool>("start");
        }

        public string Source { get; set; }
        public string Destination { get; set; }
        public string Program { get; set; }

        public bool SkipVersionCheck { get; set; }
        public bool NoBackup { get; set; }
        public bool NoZip { get; set; }
        public bool StartAfterUpdate { get; set; }
    }
}

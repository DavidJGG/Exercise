using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Files_monitor
{
    public class Monitor
    {
        private Folder _folder;

        public Monitor(Folder folder)
        {
            _folder = folder;
        }

        public void start()
        {
            while (true)
            {
                Console.WriteLine(DateTime.Now.ToString("[MM-dd-yyyy HH:mm:ss]")+ " Scanning ...");
                _folder.searchNewFiles();
                _folder.processFilesNotApplicable();
                _folder.processXlsx();
                Thread.Sleep(5000);
            }
        }
    }
}

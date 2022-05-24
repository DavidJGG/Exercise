using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetLight;

namespace Files_monitor
{
    public class ArchivoXlsx
    {
        private string _pathName;
        private string _processedPath;

        public ArchivoXlsx(string pathName, string processedPath)
        {
            _pathName = pathName;            
            _processedPath = processedPath;
        }

        public void process()
        {
            SLDocument document = new SLDocument(_pathName);
            List<string> sheets = document.GetSheetNames();
            document.CloseWithoutSaving();


            foreach (string sheet in sheets)
            {
                SLDocument newWorkBook = new SLDocument(_pathName);
                newWorkBook.CopyWorksheet(sheet, "Master_"+sheet);
                newWorkBook.SelectWorksheet("Master_" + sheet);
                sheets.ForEach(s => newWorkBook.DeleteWorksheet(s));

                newWorkBook.SaveAs(_processedPath + "\\master_" + sheet+" "+DateTime.Now.ToString("MMddyyyy HHmmss") +".xlsx");
                
            }

            File.Delete(_pathName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files_monitor
{
    public class Folder
    {
        private string _mainFolder;
        private string _notApplicableFolder;
        private string _processedFolder;
        private List<string> _files;

        public Folder(string mainFolder)
        {
            _mainFolder = mainFolder;
            _notApplicableFolder = _mainFolder+"\\Not Applicable";
            _processedFolder = _mainFolder+"\\Processed";

            Directory.CreateDirectory(_processedFolder);
            Directory.CreateDirectory(_notApplicableFolder);
        }

        public void searchNewFiles()
        {
            _files = Directory.GetFiles(_mainFolder).ToList();
        }

        public void processXlsx()
        {
            foreach (string file in _files)
            {
                string fileName = Path.GetFileName(file);
                if (isExcel(fileName))
                {
                    Console.WriteLine($"\t\t Excel: {fileName}");
                    ArchivoXlsx archivoXlsx = new ArchivoXlsx(file, _processedFolder);
                    archivoXlsx.process();
                }
            }
        }

        public void processFilesNotApplicable()
        {            
            List<string> onlyExcelFiles = new List<string>();

            foreach (string file in _files)
            {
                onlyExcelFiles.Add(file);
                string fileName = Path.GetFileName(file);
                if (!isExcel(fileName)) 
                { 
                    Console.WriteLine($"\t\t Other file: {fileName}");

                    if (!File.Exists(_notApplicableFolder + "\\" + fileName))
                        Directory.Move(file, _notApplicableFolder + "\\" + fileName);
                    else
                        Directory.Move(file, _notApplicableFolder + "\\"+DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss") + "_" + fileName);

                }
            }

            _files = onlyExcelFiles;
        }

        private bool isExcel(string file)
        {
            string[] fileArr = file.Split('.');
            string[] ext = { "xlsx", "xls" };
            if (ext.Contains(fileArr[fileArr.Length - 1]))
            {
                return true;
            }
            return false;
        }
    }
}

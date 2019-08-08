using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFApp_Maps
{
    class Data_connection
    {
        public string datalocation()
        {
            //Creamos la carpeta Comunidad de Montes Dornelas en MyDocuments
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Comunidad de Montes Dornelas";

            string path = AppDomain.CurrentDomain.BaseDirectory + @"\lib\Ficheros\DB";
            DirectoryInfo di = Directory.CreateDirectory(path);

            return "Data Source=" + path + "\\DBdeslinde.mdf;";


        }

    }
}
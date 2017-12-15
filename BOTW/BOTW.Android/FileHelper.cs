using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BOTW.Data;
using BOTW.Droid;
using SQLite;
using Xamarin.Forms;



[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace BOTW.Droid
{
    class FileHelper : IFileHelper
    {
        public string GetPath(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
    }
}

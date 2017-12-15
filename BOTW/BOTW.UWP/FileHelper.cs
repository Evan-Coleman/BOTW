using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Windows.Storage;
using BOTW.Data;
using BOTW.UWP;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace BOTW.UWP
{
    class FileHelper : IFileHelper
    {
        public string GetPath(string fileName)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);
        }
    }
}

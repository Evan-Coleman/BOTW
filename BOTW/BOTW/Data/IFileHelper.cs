using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BOTW.Data
{
    public interface IFileHelper
    {
        string GetPath(string fileName);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace People
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            //TODO:  if iOS change to string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);
        }
    }
}

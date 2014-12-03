using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundsLibraryTest
{
    class FileComparisonHandle
    {
        private FileInfo m_fileInfo;

        public FileComparisonHandle(string fileName)
        {
            // TODO: Complete member initialization
            this.m_fileInfo = new FileInfo(fileName);

            if (!m_fileInfo.Exists)
            {
                throw new FileNotFoundException();
            }
        }
    }
}

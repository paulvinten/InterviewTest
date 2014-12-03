using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundsLibraryTest
{
    class FileComparisonReport
    {
        public IEnumerable<FileComparisonEntry> Data { get; set; }

        public bool FilesAreSame { get; set; }
    }
}

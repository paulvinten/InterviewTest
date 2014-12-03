using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundsLibraryTest
{
    public interface IFileComparisonExclusion
    {

        void ApplyExclusion(FileComparisonEntry[] workingCopy, Stream fileStreamA, Stream fileStreamB);
    }
}

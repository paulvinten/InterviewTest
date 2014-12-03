using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundsLibraryTest
{
    public interface IFileComparisonExclusion
    {

        void ApplyExclusion(FileComparisonEntry[] workingCopy, System.IO.FileStream fileStreamA, System.IO.FileStream fileStreamB);
    }
}

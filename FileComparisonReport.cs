using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundsLibraryTest
{
    class FileComparisonReport
    {
        private readonly IEnumerable<FileComparisonEntry> m_resultData;

        public FileComparisonReport(IEnumerable<FileComparisonEntry> m_resultData)
        {
            this.m_resultData = m_resultData;
        }

        public bool FilesAreSame
        {
            get
            {
                var failed = m_resultData.Where(x =>
                    !x.Equals(FileComparisonEntry.Same) ||
                    !x.Equals(FileComparisonEntry.Mitigated));

                return !failed.Any();
            }
        }
    }
}

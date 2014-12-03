using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundsLibraryTest
{
    class FileComparisonReportGenerator
    {
        private readonly IEnumerable<FileComparisonEntry> m_resultData;
        private readonly FileStream m_fileStreamA;
        private readonly FileStream m_fileStreamB;
        private readonly List<IFileComparisonExclusion> m_exclusions = new List<IFileComparisonExclusion>();

        public FileComparisonReportGenerator(FileComparisonEntry[] initialResultData, FileStream fileStreamA, FileStream fileStreamB)
        {
            m_resultData = initialResultData;
            m_fileStreamA = fileStreamA;
            m_fileStreamB = fileStreamB;

            if (m_resultData.Any(x => x.Equals(FileComparisonEntry.Unknown)))
            {
                throw new ArgumentException("data contains unknown entries");
            }
        }


        internal void AddExclusion(IFileComparisonExclusion idExclusion)
        {
            m_exclusions.Add(idExclusion);
        }

        internal FileComparisonReport Generate()
        {
            var workingCopy = m_resultData.ToArray();//take a copy of the preliminary report
            foreach (var exclusion in m_exclusions)
            {
                exclusion.ApplyExclusion(workingCopy, m_fileStreamA, m_fileStreamB);
            }

            return new FileComparisonReport(workingCopy);

        }
    }
}

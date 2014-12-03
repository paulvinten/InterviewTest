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

        internal FileComparisonReport GenerateReport(FileComparisonHandle other)
        {
            var report = new FileComparisonReport();
            //do intial binary analysis
            using(var file1Stream = m_fileInfo.OpenRead())
            using (var file2Stream = other.m_fileInfo.OpenRead())
            {
                var file1Data = ReadByteArray(file1Stream);
                var file2Data = ReadByteArray(file1Stream);

                var initialResultData = Enumerable.Repeat(FileComparisonEntry.Unknown, (int)file1Stream.Length).ToArray();

                for (var idx = 0; idx < file1Data.Length; idx++)
                {
                    if (file2Data.Length > idx)
                    {
                        initialResultData[idx] = file1Data[idx] == file2Data[idx]
                            ? FileComparisonEntry.Same
                            : FileComparisonEntry.Different;
                    }
                    else
                    {
                        initialResultData[idx] = FileComparisonEntry.Missing;
                    }
                }

                report.Data = initialResultData;

            }
            return report;
        }

        private static byte[] ReadByteArray(FileStream fileStream)
        {
            byte[] fileData;

            fileData = new byte[fileStream.Length];
            fileStream.Read(fileData, 0, (int)fileStream.Length);
            return fileData;
        }
    }
}

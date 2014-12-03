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
            //do intial binary analysis
            using(var file1Stream = m_fileInfo.OpenRead())
            using (var file2Stream = other.m_fileInfo.OpenRead())
            {

                var reportGenerator = file1Stream.Length > file2Stream.Length ? 
                    CompareDataStreams(file1Stream, file2Stream) : 
                    CompareDataStreams(file2Stream, file1Stream);

                reportGenerator.AddExclusion(new IdExclusion());

                return reportGenerator.Generate();
            }
        }

        private static FileComparisonReportGenerator CompareDataStreams(FileStream fileAStream, FileStream fileBStream)
        {

            var fileAData = ReadByteArray(fileAStream);
            var fileBData = ReadByteArray(fileBStream);

            var initialResultData = Enumerable.Repeat(FileComparisonEntry.Unknown, (int)fileAData.Length).ToArray();

            for (var idx = 0; idx < fileAData.Length; idx++)
            {
                if (fileBData.Length > idx)
                {
                    initialResultData[idx] = fileAData[idx] == fileBData[idx]
                        ? FileComparisonEntry.Same
                        : FileComparisonEntry.Different;
                }
                else
                {
                    initialResultData[idx] = FileComparisonEntry.Missing;
                }
            }

            return new FileComparisonReportGenerator(initialResultData, fileAStream, fileBStream );
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

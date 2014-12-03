using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundsLibraryTest
{
    public class IdExclusion : IFileComparisonExclusion
    {
        public void ApplyExclusion(FileComparisonEntry[] workingCopy, Stream fileStreamA, Stream fileStreamB)
        {

            //this exclusion only applies if the rest of the document is equal. If the size is different, then the document must be different so no point checking the ID
            if (fileStreamA.Length != fileStreamB.Length)
            {
                return;
            }

            //reset file stream positions.
            fileStreamA.Position = 0;
            fileStreamB.Position = 0;

            using (var streamReaderA = new StreamReader(fileStreamA))
            using (var streamReaderB = new StreamReader(fileStreamA))
            {
                var contentsA = streamReaderA.ReadToEnd();

                int streamAIdOpenIndex;
                int streamAIdCloseIndex;

                DetermineIdPositions(contentsA, out streamAIdOpenIndex, out streamAIdCloseIndex);

                var contentsB = streamReaderA.ReadToEnd();

                int streamBIdOpenIndex;
                int streamBIdCloseIndex;

                DetermineIdPositions(contentsA, out streamBIdOpenIndex, out streamBIdCloseIndex);

                if ((streamAIdOpenIndex != -1 && streamAIdCloseIndex != -1)
                    && streamAIdOpenIndex == streamBIdOpenIndex
                    && streamAIdCloseIndex == streamBIdCloseIndex)
                {
                    for (var idx = streamAIdOpenIndex; idx < streamAIdCloseIndex;idx++)
                    {
                        if (workingCopy[idx] == FileComparisonEntry.Different)
                        {
                            workingCopy[idx] = FileComparisonEntry.Mitigated;
                        }
                    }
                }
            }
        }

        private static void DetermineIdPositions(string contentsA, out int idOpenIndex, out int idCloseIndex)
        {
            idOpenIndex = -1;
            idCloseIndex = -1; 


            idOpenIndex = contentsA.IndexOf("/ID [<");
            if (idOpenIndex >= 0)
            {
                //now find closing element
                idCloseIndex = contentsA.IndexOf(">]", idOpenIndex);
                if (idCloseIndex >= 0)
                {

                }
            }
        }
    }
}
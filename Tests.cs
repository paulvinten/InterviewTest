using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundsLibraryTest
{
    [TestClass]
    class Tests
    {
        [TestMethod]
        public void CheckEqualBinsAreEqual ()
        {
            var handle1 = new FileComparisonHandle("file_0.bin");
            var handle2 = new FileComparisonHandle("file_1.bin");
        }

        [TestMethod]
        public void CheckNonExistantFile()
        {
            try
            {
                var handle1 = new FileComparisonHandle("file_0.bin");

                Assert.Fail();

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(FileNotFoundException));
            }
            
        }
    }
}

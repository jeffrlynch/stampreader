using Microsoft.VisualStudio.TestTools.UnitTesting;
using StampReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampReaderTests
{
    [TestClass()]
    public class StringToolsTests
    {
        [TestMethod()]
        public void ConvertInputToMultiFormatT1_Single()
        {
            string myInput = "1345";
            string myResult = StringTools.ConvertInputToMultiFormat(myInput);
            Assert.AreEqual("('1345')", myResult);
        }
        [TestMethod()]
        public void ConvertInputToMutliFormatT2_Multi()
        {
            string myInput = "1345;4798;873;t3544";
            string myResult = StringTools.ConvertInputToMultiFormat(myInput);
            Assert.AreEqual("('1345','4798','873','t3544')", myResult);
        }
    }
}
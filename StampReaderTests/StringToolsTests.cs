using Microsoft.VisualStudio.TestTools.UnitTesting;
using StampReader;

namespace StampReaderTests
{
    [TestClass()]
    public class StringToolsTests
    {
        [TestMethod()]
        public void ConvertInputToMultiFormatTest()
        {
            //Test for handling single stamp specified even though multi selected
            string myInput = "1345";
            string myResult = StringTools.ConvertInputToMultiFormat(myInput);
            Assert.AreEqual("('1345')", myResult);
        }

        [TestMethod()]
        public void ConvertInputToMultiFormatTest1()
        {
            //Test for handling multi stamps specified
            string myInput = "1345;4798;873;t3544";
            string myResult = StringTools.ConvertInputToMultiFormat(myInput);
            Assert.AreEqual("('1345','4798','873','t3544')", myResult);
        }
    }
}
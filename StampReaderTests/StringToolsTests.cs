using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StampReader.Tests
{
    [TestClass()]
    public class StringToolsTests
    {

        [TestMethod()]
        public void ConvertInputToStampQueryFomatMulti_T1_SingleEntry()
        {
            string myInput = "1345";
            string myResult = StringTools.ConvertInputToMultiFormat(myInput);
            Assert.AreEqual("('1345')", myResult);
        }
        [TestMethod()]
        public void ConvertInputToStampQueryFomatMulti_T2_MultiEntry()
        {
            string myInput = "1345;4798;873;t3544";
            string myResult = StringTools.ConvertInputToMultiFormat(myInput);
            Assert.AreEqual("('1345','4798','873','t3544')", myResult);
        }
    }
}
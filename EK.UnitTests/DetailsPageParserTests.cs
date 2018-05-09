using EK.SiteParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EK.UnitTests
{
    [TestClass]
    public class DetailsPageParserTests
    {
        IDetailsPageParser _detailsParser;

        [TestInitialize]
        public void InitTest()
        {
            _detailsParser = new DetailsPageParser();
        }

        [TestMethod]
        public void ParseTest()
        {
            var res = _detailsParser.ParseDetails(File.ReadAllText("detailsTestPage.html"));

        }
    }
}

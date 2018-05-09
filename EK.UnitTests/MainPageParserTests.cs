using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using EK.SiteParser;

namespace Ek.UnitTests
{
    [TestClass]
    public class MainPageParserTests
    {
        private const string BodyPattern = @"<body.*?>((?s).*?)</body>";
        IMainPageParser _mainPageParser;
        string _testPageText;

        [TestInitialize]
        public void InitTests()
        {
            _testPageText = File.ReadAllText("testPage.html");
            _mainPageParser = new MainPageParser();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var items = _mainPageParser.ParseLastMessages(_testPageText);
        }
    }
}
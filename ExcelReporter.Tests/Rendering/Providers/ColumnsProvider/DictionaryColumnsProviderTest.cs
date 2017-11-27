﻿using System.Collections.Generic;
using ExcelReporter.Rendering;
using ExcelReporter.Rendering.Providers.ColumnsProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExcelReporter.Tests.Rendering.Providers.ColumnsProvider
{
    [TestClass]
    public class DictionaryColumnsProviderTest
    {
        [TestMethod]
        public void TestGetColumnsList()
        {
            IColumnsProvider columnsProvider = new DictionaryColumnsProvider();

            var dictArray = new[]
            {
                new Dictionary<string, object> { ["Id"] = 1, ["Name"] = "One", ["IsVip"] = true },
                new Dictionary<string, object> { ["Id"] = 2, ["Name"] = "Two" },
            };

            IList<ExcelDynamicColumn> columns = columnsProvider.GetColumnsList(dictArray);

            Assert.AreEqual(3, columns.Count);

            Assert.AreEqual("Id", columns[0].Name);
            Assert.AreEqual("Id", columns[0].Caption);
            Assert.IsNull(columns[0].Width);

            Assert.AreEqual("Name", columns[1].Name);
            Assert.AreEqual("Name", columns[1].Caption);
            Assert.IsNull(columns[1].Width);

            Assert.AreEqual("IsVip", columns[2].Name);
            Assert.AreEqual("IsVip", columns[2].Caption);
            Assert.IsNull(columns[2].Width);

            dictArray = new[]
            {
                new Dictionary<string, object> { ["Id"] = 2, ["Name"] = "Two" },
                new Dictionary<string, object> { ["Id"] = 1, ["Name"] = "One", ["IsVip"] = true },
            };

            columns = columnsProvider.GetColumnsList(dictArray);

            Assert.AreEqual(2, columns.Count);

            Assert.AreEqual("Id", columns[0].Name);
            Assert.AreEqual("Id", columns[0].Caption);
            Assert.IsNull(columns[0].Width);

            Assert.AreEqual("Name", columns[1].Name);
            Assert.AreEqual("Name", columns[1].Caption);
            Assert.IsNull(columns[1].Width);
        }

        [TestMethod]
        public void TestGetColumnsListIfDictionaryIsNullOrEmpty()
        {
            IColumnsProvider columnsProvider = new DictionaryColumnsProvider();
            Assert.AreEqual(0, columnsProvider.GetColumnsList(null).Count);
            Assert.AreEqual(0, columnsProvider.GetColumnsList(new Dictionary<string, object>[0]).Count);
            Assert.AreEqual(0, columnsProvider.GetColumnsList(new[] { new Dictionary<string, object>() }).Count);
        }
    }
}
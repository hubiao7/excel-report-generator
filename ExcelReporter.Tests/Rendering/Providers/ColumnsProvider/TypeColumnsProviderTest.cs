﻿using System.Collections.Generic;
using ExcelReporter.Attributes;
using ExcelReporter.Rendering;
using ExcelReporter.Rendering.Providers.ColumnsProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExcelReporter.Tests.Rendering.Providers.ColumnsProvider
{
    [TestClass]
    public class TypeColumnsProviderTest
    {
        [TestMethod]
        public void TestGetColumnsList()
        {
            IColumnsProvider columnsProvider = new TypeColumnsProvider();
            IList<ExcelDynamicColumn> columns = columnsProvider.GetColumnsList(typeof(TestType));

            Assert.AreEqual(4, columns.Count);

            Assert.AreEqual("Column1", columns[0].Name);
            Assert.AreEqual("Column1", columns[0].Caption);
            Assert.AreEqual(typeof(string), columns[0].DataType);
            Assert.IsNull(columns[0].Width);

            Assert.AreEqual("Column2", columns[1].Name);
            Assert.AreEqual("Column Two", columns[1].Caption);
            Assert.AreEqual(typeof(int), columns[1].DataType);
            Assert.IsNull(columns[1].Width);

            Assert.AreEqual("Column3", columns[2].Name);
            Assert.AreEqual("Column Three", columns[2].Caption);
            Assert.AreEqual(typeof(decimal?), columns[2].DataType);
            Assert.AreEqual(100.5, columns[2].Width);

            Assert.AreEqual("ColumnWithBadWidth", columns[3].Name);
            Assert.AreEqual("ColumnWithBadWidth", columns[3].Caption);
            Assert.AreEqual(typeof(short), columns[3].DataType);
            Assert.IsNull(columns[3].Width);
        }

        [TestMethod]
        public void TestGetColumnsListIfTypeIsNull()
        {
            IColumnsProvider columnsProvider = new TypeColumnsProvider();
            Assert.AreEqual(0, columnsProvider.GetColumnsList(null).Count);
        }

        internal class TestType : TestTypeBase
        {
            [ExcelColumn]
            public string Column1 { get; set; }

            [ExcelColumn(Caption = "Column Two")]
            public int Column2 { get; set; }

            [ExcelColumn(Caption = "Column Three", Width = 100.5)]
            public decimal? Column3 { get; set; }

            public override string OverriddenColumn { get; set; }

            public string NotColumn { get; set; }

            [ExcelColumn]
            private string NotColumn2 { get; set; }
        }

        internal class TestTypeBase
        {
            [ExcelColumn(Width = -10)]
            public short ColumnWithBadWidth { get; set; }

            [ExcelColumn]
            public virtual string OverriddenColumn { get; set; }
        }
    }
}
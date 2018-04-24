﻿using System;
using ExcelReportGenerator.Rendering.Providers.ColumnsProviders;
using ExcelReportGenerator.Tests.CustomAsserts;
using NUnit.Framework;
using NSubstitute;

namespace ExcelReportGenerator.Tests.Rendering.Providers.ColumnsProvider
{
    
    public class ObjectColumnsProviderTest
    {
        [Test]
        public void TestGetColumnsList()
        {
            IGenericColumnsProvider<Type> typeColumsProvider = Substitute.For<IGenericColumnsProvider<Type>>();

            IColumnsProvider columnsProvider = new ObjectColumnsProvider(typeColumsProvider);
            var testObject = new TypeColumnsProviderTest.TestType();
            columnsProvider.GetColumnsList(testObject);
            typeColumsProvider.Received(1).GetColumnsList(testObject.GetType());

            typeColumsProvider.ClearReceivedCalls();

            var str = "str";
            columnsProvider.GetColumnsList(str);
            typeColumsProvider.Received(1).GetColumnsList(str.GetType());
        }

        [Test]
        public void TestGetColumnsListIfObjectIsNull()
        {
            IColumnsProvider columnsProvider = new ObjectColumnsProvider(new TypeColumnsProvider());
            Assert.AreEqual(0, columnsProvider.GetColumnsList(null).Count);
        }

        [Test]
        public void TestGetColumnsListIfTypeColumnsProviderIsNull()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => new ObjectColumnsProvider(null));
        }
    }
}
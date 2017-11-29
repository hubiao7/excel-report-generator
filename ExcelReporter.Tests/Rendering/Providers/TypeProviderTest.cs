﻿using ExcelReporter.Exceptions;
using ExcelReporter.Rendering.Providers;
using ExcelReporter.Tests.CustomAsserts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace ExcelReporter.Tests.Rendering.Providers
{
    [TestClass]
    public class TypeProviderTest
    {
        [TestMethod]
        public void TestGetType()
        {
            ITypeProvider typeProvider = new DefaultTypeProvider(Assembly.GetExecutingAssembly());

            ExceptionAssert.Throws<ArgumentException>(() => typeProvider.GetType(null));
            ExceptionAssert.Throws<ArgumentException>(() => typeProvider.GetType(string.Empty));
            ExceptionAssert.Throws<ArgumentException>(() => typeProvider.GetType(" "));

            Assert.AreSame(typeof(TestType_1), typeProvider.GetType("TestType_1"));
            Assert.AreSame(typeof(TestType_1), typeProvider.GetType("ExcelReporter.Tests.Rendering.Providers:TestType_1"));
            Assert.AreSame(typeof(TestType_1.TestType_2), typeProvider.GetType("TestType_2"));
            Assert.AreSame(typeof(TestType_1.TestType_2), typeProvider.GetType("ExcelReporter.Tests.Rendering.Providers:TestType_2"));
            Assert.AreSame(typeof(TestType_1.TestType_2), typeProvider.GetType(" ExcelReporter.Tests.Rendering.Providers : TestType_2 "));

            Assert.AreSame(typeof(TestType_3), typeProvider.GetType("ExcelReporter.Tests.Rendering.Providers:TestType_3"));
            Assert.AreSame(typeof(InnerNamespace.TestType_3), typeProvider.GetType("ExcelReporter.Tests.Rendering.Providers.InnerNamespace:TestType_3"));
            ExceptionAssert.Throws<IncorrectTemplateException>(() => typeProvider.GetType("TestType_3"), "More than one type found by template \"TestType_3\"");

            Assert.AreSame(typeof(InnerNamespace.TestType_5), typeProvider.GetType("ExcelReporter.Tests.Rendering.Providers.InnerNamespace:TestType_5"));
            Assert.AreSame(typeof(TestType_5), typeProvider.GetType(":TestType_5"));
            Assert.AreSame(typeof(TestType_5), typeProvider.GetType(":TestType_5"));
            ExceptionAssert.Throws<IncorrectTemplateException>(() => typeProvider.GetType("TestType_5"), "More than one type found by template \"TestType_5\"");

            Assert.AreSame(typeof(InnerNamespace.TestType_4), typeProvider.GetType("ExcelReporter.Tests.Rendering.Providers.InnerNamespace:TestType_4"));
            Assert.AreSame(typeof(InnerNamespace.TestType_4), typeProvider.GetType("TestType_4"));
            ExceptionAssert.Throws<TypeNotFoundException>(() => typeProvider.GetType("ExcelReporter.Tests.Rendering.Providers:TestType_4"),
                "Cannot find type by template \"ExcelReporter.Tests.Rendering.Providers:TestType_4\"");

            ExceptionAssert.Throws<IncorrectTemplateException>(() => typeProvider.GetType("ExcelReporter.Tests.Rendering.Providers:InnerNamespace:TestType_4"),
                "Type name template \"ExcelReporter.Tests.Rendering.Providers:InnerNamespace:TestType_4\" is incorrect");
        }

        private class TestType_1
        {
            public class TestType_2
            {
            }
        }
    }

    public class TestType_3
    {
    }

    namespace InnerNamespace
    {
        public class TestType_3
        {
        }

        public class TestType_4
        {
        }

        public class TestType_5
        {
        }
    }
}

public class TestType_5
{
}
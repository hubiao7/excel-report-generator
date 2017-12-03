﻿using ExcelReporter.Rendering.Providers;
using ExcelReporter.Tests.CustomAsserts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ExcelReporter.Tests.Rendering.Providers
{
    [TestClass]
    public class DefaultInstanceProviderTest
    {
        [TestMethod]
        public void TestGetInstance()
        {
            IInstanceProvider instanceProvider = new DefaultInstanceProvider();
            ExceptionAssert.Throws<InvalidOperationException>(() => instanceProvider.GetInstance(null), "Type is not specified but defaultInstance is null");

            var instance1 = (TestType_3)instanceProvider.GetInstance(typeof(TestType_3));
            var instance2 = (TestType_3)instanceProvider.GetInstance(typeof(TestType_3));
            Assert.AreSame(instance1, instance2);

            Assert.IsInstanceOfType(instanceProvider.GetInstance(typeof(TestType_5)), typeof(TestType_5));
            Assert.IsInstanceOfType(instanceProvider.GetInstance(typeof(DateTime)), typeof(DateTime));

            ExceptionAssert.Throws<MissingMethodException>(() => instanceProvider.GetInstance(typeof(FileInfo)));
            ExceptionAssert.Throws<MissingMethodException>(() => instanceProvider.GetInstance(typeof(Math)));

            var testInstance = new TestType_3();
            instanceProvider = new DefaultInstanceProvider(testInstance);

            var instance3 = (TestType_3)instanceProvider.GetInstance(null);
            instance1 = (TestType_3)instanceProvider.GetInstance(typeof(TestType_3));
            instance2 = (TestType_3)instanceProvider.GetInstance(typeof(TestType_3));
            Assert.AreSame(instance1, instance2);
            Assert.AreSame(instance1, instance3);
            Assert.AreSame(instance2, instance3);
        }
    }
}
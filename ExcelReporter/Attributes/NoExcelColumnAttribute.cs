﻿using System;

namespace ExcelReporter.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class NoExcelColumnAttribute : Attribute
    {
    }
}
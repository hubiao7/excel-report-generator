﻿using ExcelReportGenerator.Attributes;

namespace ExcelReportGenerator.Rendering.Providers.DataItemValueProviders
{
    /// <summary>
    /// Provides values from data item
    /// </summary>
    [LicenceKeyPart(L = true, R = true)]
    public interface IDataItemValueProvider
    {
        /// <summary>
        /// Get value from data item based on template
        /// </summary>
        object GetValue(string template, object dataItem);
    }
}
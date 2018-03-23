﻿using ExcelReportGenerator.Enums;
using System;
using ExcelReportGenerator.Attributes;

namespace ExcelReportGenerator.Rendering
{
    /// <summary>
    /// Describes excel dynamic column
    /// </summary>
    [LicenceKeyPart(L = true, U = true)]
    public class ExcelDynamicColumn
    {
        private string _caption;

        public ExcelDynamicColumn(string name, Type dataType = null, string caption = null)
        {
            Name = name;
            DataType = dataType;
            Caption = caption;
            AggregateFunction = AggregateFunction.NoAggregation;
            if (dataType == typeof(decimal) || dataType == typeof(decimal?))
            {
                AggregateFunction = AggregateFunction.Sum;
                DisplayFormat = "#,0.00";
            }
        }

        /// <summary>
        /// Column name from data source
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Column caption which will be displayed in excel sheet
        /// </summary>
        public string Caption
        {
            get => string.IsNullOrWhiteSpace(_caption) ? Name : _caption;
            set => _caption = value;
        }

        /// <summary>
        /// Column width if panel is vertical or row height if panel is horizontal
        /// </summary>
        public double? Width { get; set; }

        /// <summary>
        /// Column data type
        /// </summary>
        public Type DataType { get; set; }

        /// <summary>
        /// Aggregate function applied to this column
        /// </summary>
        public AggregateFunction AggregateFunction { get; set; }

        /// <summary>
        /// Display format for number and date columns
        /// </summary>
        public string DisplayFormat { get; set; }

        /// <summary>
        /// Adjust to content column width if panel is vertical or row height if panel is horizontal
        /// </summary>
        public bool AdjustToContent { get; set; }

        // Order in which the column appears in Excel (for internal use)
        internal int Order { get; set; }
    }
}
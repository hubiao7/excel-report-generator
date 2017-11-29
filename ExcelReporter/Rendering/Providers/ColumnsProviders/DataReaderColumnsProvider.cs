﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ExcelReporter.Rendering.Providers.ColumnsProviders
{
    /// <summary>
    /// Provides columns info from IDataReader
    /// </summary>
    internal class DataReaderColumnsProvider : IGenericColumnsProvider<IDataReader>
    {
        public IList<ExcelDynamicColumn> GetColumnsList(IDataReader reader)
        {
            DataTable schemaTable = reader?.GetSchemaTable();
            if (schemaTable == null)
            {
                return new List<ExcelDynamicColumn>();
            }

            return schemaTable.Rows
                .Cast<DataRow>()
                .Select(r => new ExcelDynamicColumn((string)r["ColumnName"], (Type)r["DataType"]))
                .ToList();
        }

        IList<ExcelDynamicColumn> IColumnsProvider.GetColumnsList(object reader)
        {
            return GetColumnsList((IDataReader)reader);
        }
    }
}
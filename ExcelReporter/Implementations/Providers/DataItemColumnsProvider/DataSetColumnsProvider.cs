﻿using System;
using ExcelReporter.Interfaces.Providers.DataItemColumnsProvider;
using System.Collections.Generic;
using System.Data;

namespace ExcelReporter.Implementations.Providers.DataItemColumnsProvider
{
    internal class DataSetColumnsProvider : IGenericDataItemColumnsProvider<DataSet>
    {
        private readonly IGenericDataItemColumnsProvider<DataTable> _dataTableColumnsProvider;
        private readonly string _tableName;

        public DataSetColumnsProvider(IGenericDataItemColumnsProvider<DataTable> dataTableColumnsProvider, string tableName = null)
        {
            _dataTableColumnsProvider = dataTableColumnsProvider ?? throw new ArgumentNullException(nameof(dataTableColumnsProvider), Constants.NullParamMessage);
            _tableName = tableName;
        }

        public IList<ExcelDynamicColumn> GetColumnsList(DataSet dataSet)
        {
            if (dataSet == null || dataSet.Tables.Count == 0)
            {
                return new List<ExcelDynamicColumn>();
            }

            if (string.IsNullOrWhiteSpace(_tableName))
            {
                return _dataTableColumnsProvider.GetColumnsList(dataSet.Tables[0]);
            }

            DataTable table = dataSet.Tables[_tableName];
            return table == null ? new List<ExcelDynamicColumn>() : _dataTableColumnsProvider.GetColumnsList(table);
        }

        IList<ExcelDynamicColumn> IDataItemColumnsProvider.GetColumnsList(object dataSet)
        {
            return GetColumnsList((DataSet)dataSet);
        }
    }
}
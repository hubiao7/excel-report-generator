﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace ExcelReporter.Enumerators
{
    internal class DataSetEnumerator : IEnumerator<DataRow>
    {
        private readonly IEnumerator<DataRow> _dataTableEnumerator;

        public DataSetEnumerator(DataSet dataSet, string tableName = null)
        {
            if (dataSet == null)
            {
                throw new ArgumentNullException(nameof(dataSet), Constants.NullParamMessage);
            }
            if (dataSet.Tables.Count == 0)
            {
                throw new InvalidOperationException("DataSet does not contain any table");
            }

            DataTable dataTable;
            if (!string.IsNullOrWhiteSpace(tableName))
            {
                dataTable = dataSet.Tables[tableName];
                if (dataTable == null)
                {
                    throw new InvalidOperationException($"DataSet does not contain table with name \"{tableName}\"");
                }
            }
            else
            {
                dataTable = dataSet.Tables[0];
            }

            _dataTableEnumerator = dataTable.AsEnumerable().GetEnumerator();
        }

        public DataRow Current => _dataTableEnumerator.Current;

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            return _dataTableEnumerator.MoveNext();
        }

        public void Reset()
        {
            _dataTableEnumerator.Reset();
        }

        public void Dispose()
        {
            _dataTableEnumerator.Dispose();
        }
    }
}
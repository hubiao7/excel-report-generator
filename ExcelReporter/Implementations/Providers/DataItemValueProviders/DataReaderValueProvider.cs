﻿using ExcelReporter.Interfaces.Providers;
using System;
using System.Data;
using ExcelReporter.Exceptions;
using ExcelReporter.Interfaces.Providers.DataItemValueProviders;

namespace ExcelReporter.Implementations.Providers.DataItemValueProviders
{
    public class DataReaderValueProvider : IGenericDataItemValueProvider<IDataReader>
    {
        private string _columnName;
        private IDataReader _dataReader;

        public virtual object GetValue(string columnName, IDataReader dataReader)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentException(Constants.EmptyStringParamMessage, nameof(columnName));
            }
            if (dataReader == null)
            {
                throw new ArgumentNullException(nameof(dataReader), Constants.NullParamMessage);
            }
            if (dataReader.IsClosed)
            {
                throw new InvalidOperationException("DataReader is closed");
            }

            _columnName = columnName.Trim();
            _dataReader = dataReader;

            return dataReader.GetValue(GetColumnIndex());
        }

        private int GetColumnIndex()
        {
            int colIndex = -1;
            try
            {
                colIndex = _dataReader.GetOrdinal(_columnName);
            }
            catch (IndexOutOfRangeException e)
            {
                ThrowColumnNotFoundException(e);
            }

            if (colIndex == -1)
            {
                ThrowColumnNotFoundException();
            }
            return colIndex;
        }

        private void ThrowColumnNotFoundException(Exception innerException = null)
        {
            throw new ColumnNotFoundException($"DataReader does not contain column \"{_columnName}\"", innerException);
        }

        object IDataItemValueProvider.GetValue(string columnName, object dataReader)
        {
            return GetValue(columnName, (IDataReader)dataReader);
        }
    }
}
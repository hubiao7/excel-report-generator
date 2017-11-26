﻿using ExcelReporter.Helpers;
using ExcelReporter.Interfaces.Providers.DataItemValueProviders;
using System.Data;

namespace ExcelReporter.Implementations.Providers.DataItemValueProviders
{
    internal class DefaultDataItemValueProviderFactory : IDataItemValueProviderFactory
    {
        public virtual IDataItemValueProvider Create(object data)
        {
            switch (data)
            {
                case null:
                    return new ObjectPropertyValueProvider();

                case DataRow _:
                    return new DataRowValueProvider();

                case IDataReader _:
                    return new DataReaderValueProvider();
            }

            if (TypeHelper.IsDictionaryStringObject(data.GetType()))
            {
                return new DictionaryValueProvider();
            }

            return new ObjectPropertyValueProvider();
        }
    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ExcelReporter.Helpers;

namespace ExcelReporter.Rendering.Providers.ColumnsProviders
{
    internal class ColumnsProviderFactory : IColumnsProviderFactory
    {
        public virtual IColumnsProvider Create(object data)
        {
            switch (data)
            {
                case null:
                    return null;

                case IDataReader _:
                    return new DataReaderColumnsProvider();

                case DataTable _:
                    return new DataTableColumnsProvider();

                case DataSet _:
                    return new DataSetColumnsProvider(new DataTableColumnsProvider());
            }

            Type dataType = data.GetType();
            if (TypeHelper.IsKeyValuePair(dataType))
            {
                return new KeyValuePairColumnsProvider();
            }

            Type genericEnumerable = dataType.GetInterfaces().SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            if (genericEnumerable != null)
            {
                Type genericType = genericEnumerable.GetGenericArguments()[0];
                if (TypeHelper.IsKeyValuePair(genericType))
                {
                    return new KeyValuePairColumnsProvider();
                }
                if (TypeHelper.IsDictionaryStringObject(genericType))
                {
                    return new DictionaryColumnsProvider();
                }
                return new GenericEnumerableColumnsProvider(new TypeColumnsProvider());
            }

            if (data is IEnumerable)
            {
                return new EnumerableColumnsProvider(new TypeColumnsProvider());
            }

            return new ObjectColumnsProvider(new TypeColumnsProvider());
        }
    }
}
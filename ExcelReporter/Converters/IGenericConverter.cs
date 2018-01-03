﻿namespace ExcelReporter.Converters
{
    internal interface IGenericConverter<in TIn, out TOut> : IConverter
    {
        TOut Convert(TIn input);
    }
}
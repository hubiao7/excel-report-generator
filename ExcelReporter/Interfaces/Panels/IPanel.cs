﻿namespace ExcelReporter.Interfaces.Panels
{
    internal interface IPanel
    {
        void Render();

        void Delete();

        string BeforeRenderMethodName { get; set; }

        string AfterRenderMethodName { get; set; }
    }
}
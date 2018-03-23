﻿using ClosedXML.Excel;
using ExcelReportGenerator.Enums;
using System.Collections.Generic;

namespace ExcelReportGenerator.Rendering.Panels.ExcelPanels
{
    internal interface IExcelPanel : IPanel
    {
        IExcelPanel Parent { get; set; }

        IList<IExcelPanel> Children { get; set; }

        IXLRange Range { get; }

        IXLRange ResultRange { get; }

        ShiftType ShiftType { get; set; }

        PanelType Type { get; set; }

        int RenderPriority { get; set; }

        void Render();

        IExcelPanel Copy(IXLCell cell, bool recursive = true);

        void Move(IXLCell cell);

        // Пересчитывает Range относительно родительского, а также Range'и всех Children'ов
        // Только для внутренних целей
        void RecalculateRangeRelativeParentRecursive();
    }
}
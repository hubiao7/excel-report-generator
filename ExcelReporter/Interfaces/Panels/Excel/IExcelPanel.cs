﻿using System.Collections.Generic;
using ClosedXML.Excel;
using ExcelReporter.Enums;
using ExcelReporter.Interfaces.Reports;

namespace ExcelReporter.Interfaces.Panels.Excel
{
    public interface IExcelPanel : IPanel
    {
        IExcelPanel Parent { get; set; }

        IEnumerable<IExcelPanel> Children { get; set; }

        IExcelReport Report { get; set; }

        IXLRange Range { get; }

        ShiftType ShiftType { get; set; }

        PanelType Type { get; set; }

        int RenderPriority { get; set; }

        IExcelPanel Copy(IXLCell cell, bool recursive = true);

        void Move(IXLCell cell);

        /// <summary>
        /// Пересчитывает Range относительно родительского, а также Range'и всех Children'ов
        /// Только для внутренних целей
        /// </summary>
        void RecalculateRangeRelativeParentRecursive();
    }
}
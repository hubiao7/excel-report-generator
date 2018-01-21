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

        ShiftType ShiftType { get; set; }

        PanelType Type { get; set; }

        int RenderPriority { get; set; }

        IXLRange Render();

        IExcelPanel Copy(IXLCell cell, bool recursive = true);

        void Move(IXLCell cell);

        /// <summary>
        /// Пересчитывает Range относительно родительского, а также Range'и всех Children'ов
        /// Только для внутренних целей
        /// </summary>
        void RecalculateRangeRelativeParentRecursive();
    }
}
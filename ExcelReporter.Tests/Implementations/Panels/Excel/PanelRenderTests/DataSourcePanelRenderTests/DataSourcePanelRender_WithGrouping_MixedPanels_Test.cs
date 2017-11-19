﻿using ClosedXML.Excel;
using ExcelReporter.Implementations.Panels.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExcelReporter.Tests.Implementations.Panels.Excel.PanelRenderTests.DataSourcePanelRenderTests
{
    [TestClass]
    public class DataSourcePanelRender_WithGrouping_MixedPanels_Test
    {
        //[TestMethod]
        public void TestMultipleVerticalPanelsGrouping()
        {
            var report = new TestReport();
            IXLWorksheet ws = report.Workbook.AddWorksheet("Test");
            IXLRange parentRange = ws.Range(2, 2, 8, 7);
            parentRange.AddToNamed("ParentRange", XLScope.Worksheet);

            IXLRange simpleRange1 = ws.Range(3, 3, 3, 4);
            simpleRange1.AddToNamed("simpleRange1");

            simpleRange1.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            simpleRange1.Style.Border.OutsideBorderColor = XLColor.Brown;

            IXLRange child = ws.Range(4, 2, 7, 7);
            child.AddToNamed("ChildRange", XLScope.Worksheet);

            child.Range(1, 2, 4, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            child.Range(1, 2, 4, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            child.Range(1, 2, 4, 6).Style.Border.TopBorderColor = XLColor.Red;
            child.Range(1, 2, 4, 6).Style.Border.LeftBorderColor = XLColor.Red;
            child.Range(1, 2, 4, 6).Style.Border.InsideBorder = XLBorderStyleValues.None;

            IXLRange childOfChild = ws.Range(5, 2, 6, 7);
            childOfChild.AddToNamed("ChildOfChildRange");

            childOfChild.Range(1, 3, 2, 6).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            childOfChild.Range(1, 3, 2, 6).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            childOfChild.Range(1, 3, 2, 6).Style.Border.TopBorderColor = XLColor.Green;
            childOfChild.Range(1, 3, 2, 6).Style.Border.LeftBorderColor = XLColor.Green;
            childOfChild.Range(1, 3, 2, 6).Style.Border.InsideBorder = XLBorderStyleValues.None;

            IXLRange simpleRange2 = ws.Range(6, 4, 6, 7);
            simpleRange2.AddToNamed("simpleRange2", XLScope.Worksheet);

            simpleRange2.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            simpleRange2.Style.Border.TopBorderColor = XLColor.Orange;

            ws.Cell(2, 2).Value = "{di:Name}";
            ws.Cell(2, 3).Value = "{di:Date}";

            ws.Cell(3, 3).Value = "{p:StrParam}";
            ws.Cell(3, 4).Value = "{di:Sum}";
            ws.Cell(3, 5).Value = "{p:IntParam}";

            ws.Cell(4, 3).Value = "{di:Field1}";
            ws.Cell(4, 4).Value = "{di:Field2}";
            ws.Cell(4, 5).Value = "{di:parent:Sum}";
            ws.Cell(4, 6).Value = "{di:parent:Contacts}";

            ws.Cell(5, 4).Value = "{di:Field1}";
            ws.Cell(5, 5).Value = "{di:Field2}";
            ws.Cell(5, 6).Value = "{di:parent:Field1}";
            ws.Cell(5, 7).Value = "{di:parent:parent:Contacts.Phone}";

            ws.Cell(6, 5).Value = "{p:DateParam}";
            ws.Cell(6, 6).Value = "{di:parent:Field2}";
            ws.Cell(6, 7).Value = "{di:parent:parent:Contacts.Fax}";

            //ws.Cell(1, 1).Value = "{di:Name}";
            //ws.Cell(4, 1).Value = "{di:Name}";
            //ws.Cell(1, 6).Value = "{di:Name}";
            //ws.Cell(4, 6).Value = "{di:Name}";
            //ws.Cell(3, 1).Value = "{di:Name}";
            //ws.Cell(3, 6).Value = "{di:Name}";
            //ws.Cell(1, 4).Value = "{di:Name}";
            //ws.Cell(4, 4).Value = "{di:Name}";

            var parentPanel = new ExcelDataSourcePanel("m:TestDataProvider:GetIEnumerable()", ws.NamedRange("ParentRange"), report)
            {
                BeforeRenderMethodName = "BeforeRenderParentDataSourcePanel",
                AfterRenderMethodName = "AfterRenderParentDataSourcePanelChildBottom",
            };
            var simplePanel1 = new ExcelNamedPanel(ws.Workbook.NamedRange("simpleRange1"), report)
            {
                Parent = parentPanel,
            };
            var childPanel = new ExcelDataSourcePanel("m:TestDataProvider:GetChildIEnumerable(di:Name)", ws.NamedRange("ChildRange"), report)
            {
                Parent = parentPanel,
                AfterRenderMethodName = "AfterRenderChildDataSourcePanel",
            };
            var childOfChildPanel = new ExcelDataSourcePanel("di:Children", ws.Workbook.NamedRange("ChildOfChildRange"), report)
            {
                Parent = childPanel
            };
            var simplePanel2 = new ExcelNamedPanel(ws.NamedRange("simpleRange2"), report)
            {
                Parent = childOfChildPanel,
            };

            childOfChildPanel.Children = new[] { simplePanel2 };
            childPanel.Children = new[] { childOfChildPanel };
            parentPanel.Children = new[] { childPanel, simplePanel1 };
            parentPanel.Render();

            //report.Workbook.SaveAs("test.xlsx");
        }
    }
}
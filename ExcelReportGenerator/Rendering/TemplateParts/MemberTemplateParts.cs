﻿using ExcelReportGenerator.Attributes;

namespace ExcelReportGenerator.Rendering.TemplateParts
{
    /// <summary>
    /// Represent parts from which member template consist of
    /// </summary>
    [LicenceKeyPart(R = true)]
    public class MemberTemplateParts
    {
        public MemberTemplateParts(string typeName, string memberName)
        {
            TypeName = typeName;
            MemberName = memberName;
        }

        public string TypeName { get; }

        public string MemberName { get; }
    }
}
﻿namespace ReportEngine
{
    public class HierarchicalDataItem
    {
        public object DataItem { get; set; }

        public HierarchicalDataItem Parent { get; set; }
    }
}
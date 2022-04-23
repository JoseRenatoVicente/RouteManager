using RouteManager.Domain.Entities.Base;
using System;
using System.Collections.Generic;

namespace RouteManager.Domain.Entities
{
    public class ExcelFile : EntityBase
    {
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public string FileName { get; set; }
        public List<string> Columns { get; set; }
        public List<List<string>> Table { get; set; }
    }
}

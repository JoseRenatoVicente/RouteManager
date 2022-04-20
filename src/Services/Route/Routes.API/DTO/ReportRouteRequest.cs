﻿using System.Collections.Generic;

namespace Routes.API.DTO
{
    public class ReportRouteRequest
    {
        public string NameOS { get; set; }
        public string NameBase { get; set; }
        public string NameService { get; set; }
        public string NameStreet { get; set; }
        public string NameNumber { get; set; }
        public string NameComplement { get; set; }
        public string NameDistrict { get; set; }
        public string NameCEP { get; set; }
        public string NameCity { get; set; }

        public string TypeService { get; set; }
        public string City { get; set; }

        public IEnumerable<string> ReportColumns { get; set; }
    }
}
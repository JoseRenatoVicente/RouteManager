﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RouteManagerMVC.Models
{
    public class ExcelFileViewModel
    {
        public string Id { get; set; }

        [DisplayName("Data de Envio")]
        public DateTime UploadDate { get; set; }

        [DisplayName("Nome do arquivo")]
        public string FileName { get; set; }

        [DisplayName("Colunas")]
        public List<string> Columns { get; set; }
        public List<List<string>> Table { get; set; }
    }

    public class ExcelFileUpdateViewModel
    {
        public string Id { get; set; }

        [DisplayName("Data de Envio")]
        public DateTime UploadDate { get; set; }

        [DisplayName("Nome do arquivo")]
        public string FileName { get; set; }

        [DisplayName("Colunas")]
        public List<string> Columns { get; set; }
        public List<List<string>> Table { get; set; }
    }
}

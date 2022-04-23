﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RouteManagerMVC.Models
{
    public class CityViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "O nome da cidade precisa deve ser preenchido")]
        [MinLength(4, ErrorMessage = "O nome da cidade precia ter no mínimo 4 caracteres")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Selecione um estado")]
        [DisplayName("Estado")]
        public string State { get; set; }

        public override string ToString()
        {
            return $"{Name} - {State}";
        }

        public IEnumerable<string> ListStates()
        {
            return new string[]
            {
            "AC",
"AL",
"AP",
"AM",
"BA",
"CE",
"DF",
"ES",
"GO",
"MA",
"MT",
"MS",
"MG",
"PA",
"PB",
"PR",
"PE",
"PI",
"RJ",
"RN",
"RS",
"RO",
"RR",
"SC",
"SP",
"SE",
"TO",
            };

        }
    }
}
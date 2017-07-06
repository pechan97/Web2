using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string CedulaJuridica { get; set; }

        [Required]
        public string PaginaWeb { get; set; }

        [Required]
        public string Direccion { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Sector { get; set; }
    }
}
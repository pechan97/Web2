using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Contactos
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cliente")]
        public string IdCliente { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Required]
        [Display(Name = "Número de Teléfono")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Puesto")]
        public string Puesto { get; set; }

        public List<Cliente> Cliente { get; set; }
    }
}
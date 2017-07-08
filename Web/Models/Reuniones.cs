    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Reuniones
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Cliente")]
        public string IdCliente { get; set; }
        [Display(Name = "Usuario")]
        public string IdUsuario { get; set; }

        [Required]
        [Display(Name = "Titulo de reunión")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Name = "Es Virtual")]
        public bool Virtual { get; set; }

        public List<Cliente> Cliente { get; set; }
        public List<ApplicationUser> User { get; set; }
    }
}
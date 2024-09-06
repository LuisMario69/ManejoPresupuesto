using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class TipoCuentaViewModel
    {
        public int GsIdTipoCuenta { get; set; }
        [Required(ErrorMessage ="El campo 'Nombre de cuenta' es requerido")]
        [StringLength(maximumLength: 35, MinimumLength =4, ErrorMessage ="La longitud del campo '{0}' debe estar entre {2} y {1} caracteres")]
        public string GsNombreTipoCuenta { get; set; }
        public int GsIdUsuario { get; set; }
        public int GsOrden {  get; set; }

        /*
         * [EmailAddress()]
         * [Range(minimum: 18, maximum: 89, ErrorMessage = "")] => numeros
         * [Url(ErrorMessage)]
         * [CreditCard(ErrorMessage)]
         */
    }
}

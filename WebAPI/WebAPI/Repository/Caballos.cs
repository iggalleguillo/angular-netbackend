//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Caballos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Caballos()
        {
            this.CarreraJugador = new HashSet<CarreraJugador>();
        }
    
        public int IdCaballo { get; set; }
        public int ProbApuestaInicial { get; set; }
        public int ProbApuestaFinal { get; set; }
        public int ProbGanadorInicial { get; set; }
        public int ProbGanadorFinal { get; set; }
        public string NombreCaballo { get; set; }
        public int Bonificador { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarreraJugador> CarreraJugador { get; set; }
    }
}

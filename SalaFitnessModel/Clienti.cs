namespace SalaFitnessModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Clienti")]
    public partial class Clienti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clienti()
        {
            Programares = new HashSet<Programare>();
        }

        public int ClientiId { get; set; }

        [StringLength(50)]
        public string Nume1 { get; set; }

        [StringLength(50)]
        public string Prenume1 { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Programare { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Programare> Programares { get; set; }
    }
}

namespace SalaFitnessModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Antrenori")]
    public partial class Antrenori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Antrenori()
        {
            Programares = new HashSet<Programare>();
        }

        public int AntrenoriId { get; set; }

        [StringLength(50)]
        public string Nume { get; set; }

        [StringLength(50)]
        public string Prenume { get; set; }

        public int? NrTelefon { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        public int? AniExperienta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Programare> Programares { get; set; }
    }
}

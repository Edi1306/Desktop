namespace SalaFitnessModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Programare")]
    public partial class Programare
    {
        [Key]
        public int IdProgramare { get; set; }

        public int? ClientiId { get; set; }

        public int? IdAntrenor { get; set; }

        public TimeSpan? Ora { get; set; }

        public virtual Antrenori Antrenori { get; set; }

        public virtual Clienti Clienti { get; set; }
    }
}

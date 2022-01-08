namespace SalaFitnessModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sala")]
    public partial class Sala
    {
        public int Id { get; set; }

        [Column("De La")]
        public TimeSpan? De_La { get; set; }

        [Column("Pana la")]
        public TimeSpan? Pana_la { get; set; }

        [StringLength(20)]
        public string Ziua { get; set; }
    }
}

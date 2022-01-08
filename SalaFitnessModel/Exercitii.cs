namespace SalaFitnessModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Exercitii")]
    public partial class Exercitii
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Picioare { get; set; }

        [StringLength(20)]
        public string Maini { get; set; }

        [StringLength(20)]
        public string ABDOMEN { get; set; }

        [StringLength(20)]
        public string Piept { get; set; }
    }
}

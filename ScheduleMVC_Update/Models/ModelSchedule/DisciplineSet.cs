namespace ScheduleMVC_Update.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DisciplineSet")]
    public partial class DisciplineSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DisciplineSet()
        {
            ScheduleMainSet = new HashSet<ScheduleMainSet>();
        }

        [Key]
        public int DisciplineId { get; set; }

        [DisplayName("Дисциплина")]
        [Required]
        public string TitleDiscipline { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleMainSet> ScheduleMainSet { get; set; }
    }
}

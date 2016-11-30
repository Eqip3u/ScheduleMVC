namespace ScheduleMVC_Update.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AuditorySet")]
    public partial class AuditorySet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuditorySet()
        {
            ScheduleMainSet = new HashSet<ScheduleMainSet>();
        }

        [Key]
        public int AuditoryId { get; set; }

        [DisplayName("Аудитория")]
        [Required]
        public string AuditoryName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleMainSet> ScheduleMainSet { get; set; }
    }
}

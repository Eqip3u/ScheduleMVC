namespace ScheduleMVC_Update.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LecturerSet")]
    public partial class LecturerSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LecturerSet()
        {
            ScheduleMainSet = new HashSet<ScheduleMainSet>();
        }

        [Key]
        public int LecturerId { get; set; }

        [DisplayName("Преподаватель")]
        [Required]
        public string LecturerFullName { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Телефон")]
        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleMainSet> ScheduleMainSet { get; set; }
    }
}

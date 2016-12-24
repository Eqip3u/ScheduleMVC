namespace ScheduleMVC_Update.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GroupSet")]
    public partial class GroupSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GroupSet()
        {
            ScheduleMainSet = new HashSet<ScheduleMainSet>();
        }

        [Key]
        public int GroupId { get; set; }

        [DisplayName("Номер группы")]
        [Required]
        public string GroupName { get; set; }

        [DisplayName("Факультет")]
        //[Required]
        public string DepartmentName { get; set; }

        [DisplayName("Староста")]
        public string MonitorOfTheTeam { get; set; }

        [DisplayName("Телефон старосты")]
        public string MonitorTel { get; set; }

        [DisplayName("Email старосты")]
        public string MonitorEmail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleMainSet> ScheduleMainSet { get; set; }
    }
}

namespace ScheduleMVC_Update.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ScheduleMainSet")]
    public partial class ScheduleMainSet
    {
        [Key]
        public int ScheduleId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Введите дату")]
        public DateTime Date { get; set; }

        [DisplayName("Дисциплина")]
        public int DisciplineDisciplineId { get; set; }

        [DisplayName("Преподаватель")]
        public int LecturerLecturerId { get; set; }

        [DisplayName("Группа")]
        public int GroupGroupId { get; set; }

        [DisplayName("Аудитория")]
        public int AuditoryAuditoryId { get; set; }

        [DisplayName("Пара")]
        public int PairPairId { get; set; }



        [ForeignKey("AuditoryAuditoryId")]
        public virtual AuditorySet AuditorySet { get; set; }

        [ForeignKey("DisciplineDisciplineId")]
        public virtual DisciplineSet DisciplineSet { get; set; }

        [ForeignKey("GroupGroupId")]
        public virtual GroupSet GroupSet { get; set; }

        [ForeignKey("LecturerLecturerId")]
        public virtual LecturerSet LecturerSet { get; set; }

        [ForeignKey("PairPairId")]
        public virtual PairSet PairSet { get; set; }
    }
}

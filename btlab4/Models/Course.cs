namespace btlab4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            Attendances = new HashSet<Attendance>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string LecturerId { get; set; }

        [Required(ErrorMessage = "Vị trí không được trống ")]
        [StringLength(255)]
        public string Place { get; set; }
        [Required(ErrorMessage = "Ngày giờ không đúng định dạng ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime DateTime { get; set; }
        [Required(ErrorMessage = "Mã thể loại không được trống ")]

        public int CatergoryId { get; set; }
        public string Name ;
        public string LectureName ;
        public List<Category> ListCategory = new List<Category>();

        public bool isLogin = false;
        public bool isShowGoing = false;
        public bool isShowFollow = false;



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }

        public virtual Category Category { get; set; }
    }

}

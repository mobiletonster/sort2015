using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort2015.Data.Models
{
    public partial class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(200)]
        public string AuthorName { get; set; }

        [StringLength(200)]
        public string ImageName { get; set; }

        public int? ImageWidth { get; set; }

        public int? ImageHeight { get; set; }

        [StringLength(50)]
        public string ImageOrientation { get; set; }
    }

}

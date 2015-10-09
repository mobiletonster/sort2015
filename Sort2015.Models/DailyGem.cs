namespace Sort2015.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DailyGem
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Guid { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Link { get; set; }

        public string RawDescription { get; set; }

        public string Quote { get; set; }

        [StringLength(250)]
        public string Author { get; set; }

        [StringLength(1000)]
        public string LdsOrgUrl { get; set; }

        [StringLength(1000)]
        public string Topic { get; set; }

        [StringLength(1000)]
        public string SourceRss { get; set; }

        public DateTime? PubDate { get; set; }

        [StringLength(10)]
        public string LangCode { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}

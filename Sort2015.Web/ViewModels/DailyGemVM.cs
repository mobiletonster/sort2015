using Sort2015.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort2015.Web.ViewModels
{
    public class DailyGemVM
    {
        public DailyGemVM(DailyGem dg)
        {
            this.Id = dg.Id;
            this.Guid = dg.Guid;
            this.Author = dg.Author;
            this.LangCode = dg.LangCode;
            this.LdsOrgUrl = dg.LdsOrgUrl;
            this.Link = dg.Link;
            this.ModifiedDate = dg.ModifiedDate;
            this.PubDate = dg.PubDate;
            this.Quote = dg.Quote;
            this.Title = dg.Title;
        }
        public int Id { get; set; }

        [StringLength(250)]
        public string Guid { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Link { get; set; }

        public string Quote { get; set; }

        [StringLength(250)]
        public string Author { get; set; }

        [StringLength(1000)]
        public string LdsOrgUrl { get; set; }

        public DateTime? PubDate { get; set; }

        [StringLength(10)]
        public string LangCode { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}

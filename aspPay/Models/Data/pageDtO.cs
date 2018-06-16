using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspPay.Models.Data
{   
    [Table("Table")]
    public class PageDtO
    {
        [Key]
        public int Id { get; set; }
        public string Title{ get; set; }
        public string Slug{ get; set; }
        public string Body { get; set; }
        public int Sort { get; set; }
        public bool HasSideBar { get; set; }
}
}
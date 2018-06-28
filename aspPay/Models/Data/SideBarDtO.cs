using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspPay.Models.Data
{
    [Table("SideBar")]
    public class SideBarDtO
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
    }
}
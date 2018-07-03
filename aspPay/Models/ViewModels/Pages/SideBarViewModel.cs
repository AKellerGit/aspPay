using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aspPay.Models.Data;
using System.Web.Mvc;

namespace aspPay.Models.ViewModels.Pages
{
    public class SideBarViewModel
    {
        public SideBarViewModel()
        {
                    
        }

        public SideBarViewModel(SideBarDtO row)
        {
            Id = row.Id;
            Body = row.Body;
        }
        public int Id { get; set; }
        [AllowHtml]
        public string Body { get; set; }
    }
}
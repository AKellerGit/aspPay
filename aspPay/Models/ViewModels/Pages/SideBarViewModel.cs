using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aspPay.Models.Data;

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
        public string Body { get; set; }
    }
}
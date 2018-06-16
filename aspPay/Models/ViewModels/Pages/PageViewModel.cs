using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using aspPay.Models.Data;



namespace aspPay.Models.ViewModels.Pages
{
    public class PageViewModel
    {

        public PageViewModel()
        {

        }

        public PageViewModel(PageDtO row)
        {
            Id = row.Id;
            Title = row.Title;
            Slug = row.Slug;
            Body = row.Body;
            Sort = row.Sort;
            HasSideBar = row.HasSideBar;
        }

        public int Id { get; set; }

        //notifies on /Admin/Pages/AddPage if the Title Field is empty
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        public string Slug { get; set; }

        //notifies on /Admin/Pages/AddPage if the body Field is empty
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        public string Body { get; set; }
        public int Sort { get; set; }
        public bool HasSideBar { get; set; }
    }
}
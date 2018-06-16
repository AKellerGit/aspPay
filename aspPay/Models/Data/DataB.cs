using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace aspPay.Models.Data
{
    public class DataB : DbContext 
    {
        //access Table through entity framework
        public DbSet<PageDtO>  pages { get; set; }
    }
}
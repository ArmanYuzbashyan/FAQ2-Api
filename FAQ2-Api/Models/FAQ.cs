using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FAQ2_Api.Models
{
    
    public class FAQ  
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int GroupId { get; set; }        
        
    }
    public class Group 
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<FAQ> FAQs { get; set; } = new List<FAQ> { };
       
    }
    public class Page<T>
    {
        public IEnumerable<T> Pageinfo { get; set; } = new List<T> { };
        public  int PageNumber { get; set; }
        public int PageCount { get; set; }
    }    
}


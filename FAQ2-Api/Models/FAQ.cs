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
        
        public static implicit operator IdAble (FAQ faq)
        {
            IdAble ConvertedFAQ = new IdAble() ;
            ConvertedFAQ.Id = faq.Id;
            return ConvertedFAQ;
        }
    }
    public class Group 
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<FAQ> FAQs { get; set; } = new List<FAQ> { };

        public static implicit operator IdAble(Group group)
        {
            IdAble ConvertedGroup = new IdAble();
            ConvertedGroup.Id = group.Id;
            return ConvertedGroup;
        }

    }
    public class Page
    {
        public IEnumerable<FAQ> Pageinfo { get; set; } = new List<FAQ> { };
        public  int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
    
}


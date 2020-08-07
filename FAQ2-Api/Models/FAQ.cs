using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FAQ2_Api.Models
{
    
    public class FAQ  //: IdAble
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int GroupId { get; set; }
        
    }
    public class Group //: IdAble
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<FAQ> FAQs { get; set; } = new List<FAQ> { };

    }

}


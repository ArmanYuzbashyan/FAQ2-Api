using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FAQ2_Api.Models
{
    public static class AG//all groups
    {
        public static List<Group> Groups = new List<Group>
        {
            new Group {  Id = 1, GroupName = "JOJO",
                FAQs = new List<FAQ> {new FAQ{ Id = 1, Question = "q1", Answer = "a1",GroupId = 1 },
                                      new FAQ{ Id = 2, Question = "q2", Answer = "a2",GroupId = 1 }
                } // test anelu hamar em hardcode arel, piti datark { } lini
            }
        };
        public static async Task<List<FAQ>> GetAllFAQs() 
        {   // irar em miacnum bolor group-eri mijic FAQ-ery, vorpes list  
            // controllerneri mej shata petq 
            var fAQs = new List<FAQ> { };
            await Task.Run(() =>
            {
                foreach (Group g in AG.Groups)               
                {
                    fAQs.AddRange(g.FAQs);
                };
            });
            return fAQs;
        }       
        
    }    
}

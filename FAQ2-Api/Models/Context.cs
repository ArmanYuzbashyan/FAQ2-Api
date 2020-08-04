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
            new Group {  Id = 0, GroupName = "JOJO",
                FAQs = new List<FAQ> {new FAQ{ Id = 0, Question = "q1", Answer = "a1",GroupId = 0 },
                                      new FAQ{ Id = 1, Question = "q2", Answer = "a2",GroupId = 0 }
                }
            }
        };
        public static async Task<IEnumerable<FAQ>> GetAllFAQs() 
        {   // irar em miacnum bolor group-eri mijic FAQ-ery, vorpes list                                          
            var fAQs = new List<FAQ> { };
            await Task.Run(() =>
            {
                Parallel.ForEach(AG.Groups, g =>
               
                {
                    fAQs.AddRange(g.FAQs);
                });
            });
            return fAQs;
        }
    }
}

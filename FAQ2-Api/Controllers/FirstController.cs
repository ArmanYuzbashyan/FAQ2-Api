using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FAQ2_Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FAQ2_Api.Controllers
{
    [Route("api/First")]
    [ApiController]
    public class FirstController : ControllerBase
    {
        
        public List<Group> Groups = new List<Group> {
            new Group {  Id = 1, GroupName = "JOJO",
                FAQs =new List<FAQ> {new FAQ{ Id = 1, Question = "q1", Answer = "a1",GroupId = 1  },
                        new FAQ{ Id = 2, Question = "q2", Answer = "a2",GroupId = 1  }
                }
            }
        };

       

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FAQ2_Api.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace FAQ2_Api.Models.Methods
{
    public static class GroupActions
    {
        public static List<Group> GetGroups()
        {
            return AG.Groups;
        }


        public static async Task<List<Group>> GetGroupssSearch(string search)
        {   // "search" -- filter em anum anunnerov
            var gn = new List<Group> { };
            await Task.Run(() =>
            {
                foreach (Group g in AG.Groups)
                {
                    if (g.GroupName.MySearch(search, StringComparison.CurrentCultureIgnoreCase))
                        gn.Add(g);

                    // primitive variant


                    // var lowG = g.GroupName.ToLower();
                    // var lowS = search.ToLower();
                    // if (lowG.Contains(lowS))
                    //    gn.Add(g);
                };
            });
            return gn;
        }

        public static async Task<bool> PostGroup(Group group, bool done)
        { //  post em anum trvac group-y AG.Groups-i mej

            await Task.Run(() =>
            {
                group.Id = AG.Groups.Max(x => x.Id) + 1;

                AG.Groups.Add(group);//(new Group { GroupName = group.GroupName; }); 
                done = true;         // senc piti liner
            });
            return done;
        }
        public static async Task<bool> DeleteGroup(int id, bool done)
        {   // AG.Groups-ic trvac id-ov groupy jnjum em
            await Task.Run(() =>
            {
                foreach (Group g in AG.Groups)
                {
                    if (g.Id == id)
                    {
                        AG.Groups.Remove(g);
                        done = true;
                        break;
                    }
                };
            });
            return done;
        }
        public static async Task<bool> PutGroup(int id, Group group, bool done)
        {   // Trvac id-ov group-i anunn em poxum,,
            //Questionneroi het gorc chunenq stex
            await Task.Run(() =>
            {
                foreach (Group g in AG.Groups)
                {
                    if (id == g.Id)
                    {
                        g.GroupName = group.GroupName;
                        done = true;
                        break;// NO FAQs !!!!
                    }
                };
            });
            return done;
        }
    }
}

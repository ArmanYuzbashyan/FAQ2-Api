using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FAQ2_Api.Models
{
    public static class MakeId
    {
        public static int NewId(List<FAQ> source) // generic-ov convert-i xndir em unenum
        {
            int id = 1;

            if (source.Count == 0)
            {
                return id;
            }
            foreach (FAQ s in source)
            {
                if (s.Id > id)
                {
                    id = s.Id;
                }
            }
            return id + 1;
        }
        public static int NewId(List<Group> source)
        {
            int id = 1;

            if (source.Count == 0)
            {
                return id;
            }

            foreach (Group s in source)
            {
                if (s.Id > id)
                {
                    id = s.Id;
                }
            }
            return id + 1;
        }
    }

    //public static class MakeId
    //{
    //    public static int NewId (List<IdAble> source)
    //    {
    //        int id = 1;
    //        if (source.Count == 0)
    //        {                
    //            return id;
    //        }
    //        foreach (IdAble s in source)
    //        {
    //            if (s.Id > id)
    //            {
    //                id = s.Id;
    //            }
    //        }
    //        return id;
    //    }
    //}
    //public class IdAble
    //{
    //    public int Id { get; set; }
    //}

}

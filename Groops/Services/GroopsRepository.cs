using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Groops.Models;

namespace Groops.Services
{



    public class GroopsRepository
    {

        private const string CacheKey = "GroopsStore";

        public GroopsRepository()
        {


            //cache use is only temporary
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var groopsApi = new GroopsAPI[]
                    {

                        new GroopsAPI{
                    Id=1,
                    Name = "blah blah"
                },
                new GroopsAPI{
                    Id=2,
                    Name = "cha cha"
                }

                    };

                    ctx.Cache[CacheKey] = groopsApi;
                }
            }
        }

        public GroopsAPI[] GetAllContacts()
        {

            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (GroopsAPI[])ctx.Cache[CacheKey];

            }

            return new GroopsAPI[]{
                
                new GroopsAPI{
                    Id=0,
                    Name = "Placeholder"
                }
                
            };

        }

        public bool SaveGroopsAPI(GroopsAPI oneGroopMember)
        {
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                try
                {
                    var currentData = ((GroopsAPI[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(oneGroopMember);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }


                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;

        }
    }
}
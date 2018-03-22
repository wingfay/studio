using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ZK.NetCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        public IConfiguration Configuration { get; internal set; }
        public ValuesController()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        // GET api/values
        [HttpGet]
        public List<(string, string, string)> Get()
        {

            var Sections = Configuration.GetSection("AppSettings").GetChildren();


            List<(string, string, string)> list = new List<(string, string, string)>();

            foreach(IConfigurationSection section in Sections)
            {
                string strKey = section.Key;


                foreach(IConfigurationSection sectionChild in section.GetChildren())
                {
                    list.Add((strKey, sectionChild.Key, sectionChild.Value));
                }

                
            }

            var data = Configuration.GetSection("AppSettings");

            var liste = from key in data.GetChildren()
                        from sectionChild in data.GetSection(key.Key).GetChildren()
                        select (key.Key, sectionChild.Key, sectionChild.Value);




            //var pairs =
            //    from key in Section.Cast<string>()
            //    from value in Section
            //    //select new Object { 

            //    //     key.Split(':')[0],
            //    //    key.Split(':')[1],
            //    //     value
            //    //}

            //if (pairs == null)
            //{
            //    return new List<T>();
            //}

            //return pairs.ToList();

            //return new string[] { "value1", "value2" };

            return liste.ToList(); ;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

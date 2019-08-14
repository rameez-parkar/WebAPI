using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace anotherapi.Controllers
{
    [Route("leapyear/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Send me a Greeting!";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            if (id == "hi")
                return "Hello";
            else if (id == "hello")
                return "Hi";
            return "Good Day";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] JObject[] json)
        {
            string result = "";
            foreach (var years in json)
            {
                Dictionary<string, string> data = years.ToObject<Dictionary<string, string>>();
                foreach (var current in data)
                {
                    int year = int.Parse(current.Value);
                    if(year % 400 != 0)
                    {
                        if(year % 4 == 0 && year % 100 != 0)
                        {
                            result += $"{year} is a leap year.\n";
                        }
                        else
                        {
                            result += $"{year} is not a leap year.\n";
                        }
                    }
                    else
                    {
                        result += $"{year} is a leap year.\n";
                    }
                }
            }
            return result;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

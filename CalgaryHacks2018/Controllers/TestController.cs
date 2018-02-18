using System.Collections.Generic;
using System.Web.Http;

namespace CalgaryHacks2018.Controllers
{
    public class TestController : ApiController
    {
        // GET api/<controller>
        public List<ValueObject> Get()
        {
            return Storage.getValues();
        }

        public void Get(string value)
        {
            Storage.add(value);
        }

        // POST api/<controller>
        public string Post([FromBody]string value)
        {
            Storage.add(value);
            return value;
        }

        public void Delete()
        {
            Storage.reset();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace CalgaryHacks2018.Controllers
{
    public class ParticleController : ApiController
    {

        private static readonly HttpClient client = new HttpClient();

        // GET api/<controller>
        [HttpGet]
        public List<Input> Get()
        {
            return Storage.getInputValues();
        }

        [HttpGet]
        public List<Input> Get(int number)
        {
            List<Input> allData = Storage.getInputValues();
            
            return allData.Skip(Math.Max(0, allData.Count() - number)).ToList();
            
        }

        // POST api/<controller>
        public void Post([FromBody]Input value)
        {
            cleanUpInput(value);

            Storage.add(value.distance);
            Storage.add(value);

            string slackMessage = getMessage(value);
            sendSlack(slackMessage);
        }

        private void cleanUpInput(Input value)
        {
            int distance = Math.Max(0, 155 - Int32.Parse(value.distance));
            double temperature = Double.Parse(value.temperature);
            double humidity = Double.Parse(value.humidity);
            value.distance = distance.ToString();
            value.temperature = temperature.ToString("#.##");
            value.humidity = humidity.ToString("#.##");
        }

        private string getMessage(Input value)
        {
            int distance = Int32.Parse(value.distance);
            StringBuilder message = new StringBuilder();


            if (distance > 30)
            {
                message.Append("AVALANCHE WARNING! STAY AWAY!\n\n");
            }
            message.Append("The current temperature is: " + value.temperature + "C\n" +
                "Humidity: " + value.humidity + "%\n" +
                "Snowfall: " + value.distance + "cm.\n" +
                "<http://binary-brothers.tech/|Click here> for details!");
            return message.ToString();
        }

        public void sendSlack(String message)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://hooks.slack.com/services/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");
            // Add an Accept header for JSON format.
            var httpContent = new StringContent("{\"text\" : \"" + message + "\"}" );
            client.PostAsync("", httpContent);
        }

        public class Input
        {
            public long time { get; set; }
            public string distance { get; set; }

            public string humidity { get; set; }

            public string temperature { get; set; }
        }
    }
}
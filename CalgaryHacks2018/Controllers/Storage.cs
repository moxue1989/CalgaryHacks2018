using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CalgaryHacks2018.Controllers.ParticleController;

namespace CalgaryHacks2018.Controllers
{
    public static class Storage
    {
        public static List<ValueObject> values = new List<ValueObject>();
        public static List<Input> inputValues = new List<Input>();

        public static void add(string value)
        {
            long time = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            values.Add(new ValueObject(time, value));
        }

        public static void add(Input value)
        {
            value.time = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;        
            inputValues.Add(value);
        }

        public static List<ValueObject> getValues()
        {
            return values;
        }    

        public static List<Input> getInputValues()
        {
            return inputValues;
        }

        public static void reset()
        {
            values = new List<ValueObject>();
            inputValues = new List<Input>();
        }
    }
    
    public class ValueObject
    {
        public long time { get; set; }

        public string value { get; set; }

        public ValueObject(long time, string value)
        {
            this.time = time;
            this.value = value;
        }
    }
}
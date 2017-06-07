using System;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;

using MoviesSystem.HelperClass;

namespace MoviesSystem {
    
    public class ReadJSON
    {
        static void Main()
        {
            // ... The StreamReader will free resources on its own.
            string input;
            using (StreamReader reader = new StreamReader("../../Data/moviedata.json"))
            {
                input = reader.Read();
            }
            var serializer = new JavaScriptSerializer();
            var deserializedResult = serializer.Deserialize<List<HelperClass>>(input);
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;


namespace MoviesSystem.Utils {
    
    public class ReadJSON
    {
        public static List<HelperClass> Read()
        {
            // ... The StreamReader will free resources on its own.
            string input;
            using (StreamReader reader = new StreamReader("../../../../Data/moviedata.json"))
            {
                input = reader.ReadToEnd();
            }
            var serializer = new JavaScriptSerializer() { MaxJsonLength = 86753090 };
            var deserializedResult = serializer.Deserialize<List<HelperClass>>(input);
            return deserializedResult;
        }
    }
}
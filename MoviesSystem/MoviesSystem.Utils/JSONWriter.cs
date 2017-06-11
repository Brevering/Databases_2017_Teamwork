using MoviesSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MoviesSystem.Utils
{
    public class JSONWriter
    {
        public void Write(ObservableCollection<Movie> moviesCollection)
        {
            var serializer = new JavaScriptSerializer();
            var builder = new StringBuilder();
            string json;

            foreach(var movie in moviesCollection)
            {
                json = serializer.Serialize(movie);
                builder.Append(json);
            }

            using (StreamWriter writer = new StreamWriter("../../../../Data/exportedData.json"))
            {
                writer.Write(builder);
            }
        }
    }
}

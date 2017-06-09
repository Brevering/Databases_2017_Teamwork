using System;

namespace MoviesSystem
{

    class Program
    {
        static void Main(string[] args)
        {
            var movieArray = ReadJSON.Read();

            //Console log details for movie number 1234 in the array
            Console.WriteLine("Title: " + movieArray[1234].title);
            Console.WriteLine("Year: "+movieArray[1234].year);
            Console.WriteLine("Actors: " + string.Join(", ", movieArray[1234].info.actors));
            Console.WriteLine("Director/s: " + string.Join(", ", movieArray[1234].info.directors));
        }
    }
}

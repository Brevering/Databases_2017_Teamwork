using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesSystem.Models;
using MoviesSystem.Utils;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfMovieSystem.Behavior;
using System.Windows;
using WpfMovieSystem.Helpers;

namespace WpfMovieSystem.ViewModels
{
    public class MoviesViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "MoviesViewModel";
            }
        }

        public MoviesViewModel()
        {
            var a = MoviesSystem.Utils.ReadJSON.Read();
            for (int i = 0; i < 5; i++)
            {
                Movie newMovie = new Movie();
                newMovie.Title = a[i].title;
                Description des = new Description();
                des.Summary = a[i].info.plot;
                des.Year = a[i].year;
                newMovie.Description = des;
                newMovie.Genres = new List<Genre>();
                newMovie.Rate = new Rate()
                {
                    RateValue = a[i].info.rating,
                    Id = i
                };
                MoviesCollection.Add(newMovie);
            }
        }

        private ObservableCollection<Movie> moviesCollection;
        public ObservableCollection<Movie> MoviesCollection
        {
            get
            {
                if (moviesCollection == null)
                {
                    moviesCollection = new ObservableCollection<Movie>();
                }
                return moviesCollection;
            }
            set
            {
                if (moviesCollection == null)
                {
                    moviesCollection = new ObservableCollection<Movie>();
                }
                else
                {
                    moviesCollection.Clear();
                }

                foreach (var item in value)
                {
                    moviesCollection.Add(item);
                }
            }
        }

        private ICommand loadFromDbCommand;
        public ICommand LoadFromDbCommand
        {
            get
            {
                if (this.loadFromDbCommand == null)
                {
                    this.loadFromDbCommand = new RelayCommand(this.HandleLoadFromDbCommand);
                }
                return this.loadFromDbCommand;
            }
        }

        public async void HandleLoadFromDbCommand(object parameter)
        {
            ObservableCollection<Movie> updatedMovies = await GetMoviesFromDb();
            if (updatedMovies != null)
            {
                MoviesCollection = updatedMovies;
            }
            else
            {
                ShowMessage.ShowError("There were some problems loading movies from Database!");
            }
        }

        private async Task<ObservableCollection<Movie>> GetMoviesFromDb()
        {
            return await Task.Run(() =>
            {
                ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
                using (MoviesSystemDbContext context = new MoviesSystemDbContext())
                {
                    var dbMovies = context.Movies.ToList();
                    foreach (Movie movie in dbMovies)
                    {
                        Movie newMovie = new Movie();
                        newMovie.Title = movie.Title;
                        newMovie.Rate = movie.Rate;
                        newMovie.Id = movie.Id;
                        newMovie.Genres = movie.Genres;
                        newMovie.Description = movie.Description;
                        newMovie.Actors = movie.Actors;
                        movies.Add(newMovie);
                    }
                }

                return movies;
            });
        }

        private ICommand saveToDbCommand;
        public ICommand SaveToDbCommand
        {
            get
            {
                if (this.saveToDbCommand == null)
                {
                    this.saveToDbCommand = new RelayCommand(this.HandleSaveToDbCommand);
                }
                return this.saveToDbCommand;
            }
        }

        public void HandleSaveToDbCommand(object parameter)
        {
            StringBuilder error = new StringBuilder();
            foreach (Movie movie in MoviesCollection)
            {
                try
                {
                    using (MoviesSystemDbContext context = new MoviesSystemDbContext())
                    {
                        var dbMovies = context.Movies;

                        Movie dbMovie = dbMovies.Where(
                        m => m.Title == movie.Title && m.Description.Year==movie.Description.Year)
                        .FirstOrDefault();
                        if (dbMovie == null)
                        {
                            dbMovies.Add(movie);
                        }
                        else
                        {
                            dbMovie.Actors = movie.Actors;
                            dbMovie.Description = movie.Description;
                            dbMovie.Genres = movie.Genres;
                            dbMovie.Rate = dbMovie.Rate;
                        }

                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    error.AppendFormat("{0} was not uploaded due to err:{1}{2}", movie.Title, ex.Message, Environment.NewLine);
                }                         
            }

            if (error.Length > 0)
            {
                ShowMessage.ShowError(error.ToString());
            }
        }
    }
}

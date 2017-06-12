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

        private static bool isSavingToDb = false;
        private static Object _lock = new Object();

        public MoviesViewModel()
        {
            List<HelperClass> jsonResult = MoviesSystem.Utils.JSONReader.Read();
            if (jsonResult==null)
            {
                ShowMessage.ShowError("Reading from json failed!");
                return;
            }

            for (int i = 0; i < 8; i++)
            {
                Movie newMovie = new Movie();
                newMovie.Title = jsonResult[i].title;
                Description des = new Description();
                
                des.Year = jsonResult[i].year;
                if (jsonResult[i].info == null)
                {
                    newMovie.Description = des;
                    continue;
                }

                des.Summary = jsonResult[i].info.plot;
                newMovie.Description = des;
                
                if (jsonResult[i].info.genres != null)
                {
                    foreach (string genre in jsonResult[i].info.genres)
                    {
                        Genre newGenre = new Genre();
                        newGenre.Name = genre;
                        newMovie.Genres.Add(newGenre);
                    }
                }
                
                newMovie.Rate = new Rate()
                {
                    RateValue = jsonResult[i].info.rating
                };

                if (jsonResult[i].info.actors != null)
                {
                    foreach (string actor in jsonResult[i].info.actors)
                    {
                        Actor newActor = new Actor();
                        int separatorIndex = actor.IndexOf(' ');
                        if (separatorIndex > 0)
                        {
                            newActor.FirstName = actor.Substring(0, separatorIndex);
                            newActor.LastName = actor.Substring(separatorIndex + 1);
                        }
                        else
                        {
                            newActor.FirstName = actor;
                        }
                        newMovie.Actors.Add(newActor);
                    }
                }

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
                    this.saveToDbCommand = new RelayCommand(this.HandleSaveToDbCommand,CanSaveToDb);
                }
                return this.saveToDbCommand;
            }
        }

        private bool CanSaveToDb(object parameter)
        {
            return !isSavingToDb;
        }

        public async void HandleSaveToDbCommand(object parameter)
        {
            lock (_lock)
            {
                if (isSavingToDb)
                {
                    return;
                }

                isSavingToDb = true;
            }

            await Task.Run(() =>
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
                            m => m.Title == movie.Title && m.Description.Year == movie.Description.Year)
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
                else
                {
                    ShowMessage.ShowInfo("Movies saved successfully");
                }
            });

            isSavingToDb = false;
        }

       private ICommand exportDataCommand;
       public ICommand ExportDataCommand
        {
            get
            {
                if (this.exportDataCommand == null)
                {
                    this.exportDataCommand = new RelayCommand(this.HandleExportDataCommand);
                }
                return this.exportDataCommand;
            }
        }

        private void HandleExportDataCommand(object parameter)
        {
            var writer = new PDFWriter();
            writer.Write(moviesCollection);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesSystem.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfMovieSystem.Behavior;
using System.Windows;

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
            for (int i = 0; i < 5; i++)
            {
                Movie newMovie = new Movie();
                newMovie.Title = "Title"+i;
                Description des = new Description();
                des.Summary = "Summary "+i;
                des.Year = DateTime.Now;
                newMovie.Description = des;
                newMovie.Genres = new List<Genre>();
                newMovie.Rate = new Rate()
                {
                    RateValue = i,
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
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                Window Owner = App.Current.MainWindow;
                string title = "Error";
                string err = "There were some problems loading movies from Database!";
                if (MessageBox.Show(Owner, err, title, buttons, icon) == MessageBoxResult.OK)
                {
                }
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
                        movies.Add(movie);
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
            ///TODO: Save movies in DB
        }
    }
}

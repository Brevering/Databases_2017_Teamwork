using MoviesSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfMovieSystem.Behavior;
using WpfMovieSystem.Helpers;
using WpfMovieSystem.Views;

namespace WpfMovieSystem.ViewModels
{
    public class ActorsViewModel : ViewModelBase, IPageViewModel
    {
        MoviesSystemDbContext dbContext = new MoviesSystemDbContext();

        public string Name
        {
            get
            {
                return "ActorsViewModel";
            }
        }

        private ObservableCollection<Actor> actorsCollection;
        public ObservableCollection<Actor> ActorsCollection
        {
            get
            {
                if (actorsCollection == null)
                {
                    actorsCollection = new ObservableCollection<Actor>();
                }
                return actorsCollection;
            }
            set
            {
                if (actorsCollection == null)
                {
                    actorsCollection = new ObservableCollection<Actor>();
                }
                else
                {
                    actorsCollection.Clear();
                }

                foreach (var item in value)
                {
                    actorsCollection.Add(item);
                }
            }
        }

        private ICommand openInsertActorWindowCommand;
        public ICommand OpenInsertActorWindowCommand
        {
            get
            {
                if (this.openInsertActorWindowCommand == null)
                {
                    this.openInsertActorWindowCommand = 
                        new RelayCommand(this.HandleOpenInsertActorWindowCommand);
                }
                return this.openInsertActorWindowCommand;
            }
        }

        private void HandleOpenInsertActorWindowCommand(object parameter)
        {
            InsertWindowView insertActorView = new InsertWindowView();
            insertActorView.DataContext = this;
            insertActorView.Show();
        }

        private ICommand addNewActorCommand;
        public ICommand AddNewActorCommand
        {
            get
            {
                if (this.addNewActorCommand == null)
                {
                    this.addNewActorCommand = new RelayCommand(this.HandleAddNewActorCommand);
                }
                return this.addNewActorCommand;
            }
        }

        private void HandleAddNewActorCommand(object parameter)
        {
            Window addActorWindow = parameter as InsertWindowView;
            if (addActorWindow==null)
            {
                return;
            }

            Grid newActorForm = addActorWindow.FindName("NewActorForm") as Grid;
            if (newActorForm==null)
            {
                return;
            }

            TextBox FirstNameTextBox = 
                newActorForm.FindName("FirstNameTextBox") as TextBox;
            TextBox LastNameTextBox =
                newActorForm.FindName("LastNameTextBox") as TextBox;
            TextBox MoviesTextBox =
                newActorForm.FindName("MoviesTextBox") as TextBox;
            if (MoviesTextBox==null|| LastNameTextBox==null|| FirstNameTextBox==null)
            {
                return;
            }

            try
            {
                using (MoviesSystemDbContext context = new MoviesSystemDbContext())
                {
                    var firstName = FirstNameTextBox.Text;
                    var lastName = LastNameTextBox.Text;
                    var movies = MoviesTextBox.Text.Split(',');

                    var newActor = new Actor
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Movies = new List<Movie>()
                    };
                    foreach (var movie in movies)
                    {
                        newActor.Movies.Add(LoadOrCreateMovie(context, movie));
                    }

                    context.Actors.Add(newActor);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowError(ex.Message);
                return;
            }

            addActorWindow.Close();
        }

        private static Movie LoadOrCreateMovie(MoviesSystemDbContext context, string movieTitle)
        {
            var movie = context.Movies
                            .FirstOrDefault(m => m.Title.ToLower() == movieTitle.ToLower());

            if (movie == null)
            {
                movie = new Movie
                {
                    Title = movieTitle
                };
            }

            return movie;
        }

        //private async Task<ObservableCollection<Actor>> InsertIntoDbCommand()
        //{
        //    return await Task.Run(() =>
        //    {
        //        ObservableCollection<Actor> actors = new ObservableCollection<Actor>();
        //        using (MoviesSystemDbContext context = new MoviesSystemDbContext())
        //        {
        //            var newActor = new Actor
        //            {
        //                FirstName = "FN",
        //                LastName = "LN"
        //            };
        //            actors.Add(newActor);

        //            context.Actors.Add(newActor);
        //            context.SaveChanges();
        //        }

        //        return actors;
        //    });
        //}

    }
}

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

        private ICommand loadActorsFromDbCommand;
        public ICommand LoadActorsFromDbCommand
        {
            get
            {
                if (this.loadActorsFromDbCommand == null)
                {
                    this.loadActorsFromDbCommand = new RelayCommand(this.HandleloadActorsFromDbCommand);
                }
                return this.loadActorsFromDbCommand;
            }
        }

        public async void HandleloadActorsFromDbCommand(object parameter)
        {
            ObservableCollection<Actor> updatedActors = await GetActorsFromDb();
            if (updatedActors != null)
            {
                ActorsCollection = updatedActors;
            }
            else
            {
                ShowMessage.ShowError("There were some problems loading actors from Database!");
            }
        }

        private async Task<ObservableCollection<Actor>> GetActorsFromDb()
        {
            return await Task.Run(() =>
            {
                ObservableCollection<Actor> actors = new ObservableCollection<Actor>();
                using (MoviesSystemDbContext context = new MoviesSystemDbContext())
                {
                    var dbActors = context.Actors.ToList();
                    foreach (Actor actor in dbActors)
                    {
                        Actor newActor = new Actor();
                        newActor.Id = actor.Id;
                        newActor.FirstName = actor.FirstName;
                        newActor.LastName = actor.LastName;
                        newActor.Movies = actor.Movies;
                        actors.Add(newActor);
                    }
                }

                return actors;
            });
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

        private ICommand openUpdateActorWindowCommand;
        public ICommand OpenUpdateActorWindowCommand
        {
            get
            {
                if (this.openUpdateActorWindowCommand == null)
                {
                    this.openUpdateActorWindowCommand =
                        new RelayCommand(this.HandleOpenUpdateActorWindowCommand);
                }
                return this.openUpdateActorWindowCommand;
            }
        }

        private void HandleOpenUpdateActorWindowCommand(object parameter)
        {
            UpdateWindowView updateActorView = new UpdateWindowView();
            updateActorView.DataContext = this;
            updateActorView.Show();
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

        private Movie LoadOrCreateMovie(MoviesSystemDbContext context, string movieTitle)
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

        private ICommand updateActorCommand;
        public ICommand UpdateActorCommand
        {
            get
            {
                if (this.updateActorCommand == null)
                {
                    this.updateActorCommand = new RelayCommand(this.HandleUpdateActorCommand);
                }
                return this.updateActorCommand;
            }
        }

        private void HandleUpdateActorCommand(object parameter)
        {
            Window updateActorWindow = parameter as UpdateWindowView;
            if (updateActorWindow == null)
            {
                return;
            }

            Grid updateActorForm = updateActorWindow.FindName("UpdateActorForm") as Grid;
            if (updateActorForm == null)
            {
                return;
            }

            TextBox ActorNameTextBox =
                updateActorForm.FindName("ActorNameTextBox") as TextBox;
            TextBox NewValueTextBox =
                updateActorForm.FindName("NewValueTextBox") as TextBox;
            RadioButton NameRadioButton =
                updateActorForm.FindName("UpdateNameButton") as RadioButton;
            RadioButton MoviesRadioButton =
                updateActorForm.FindName("UpdateMoviesButton") as RadioButton;
            if (ActorNameTextBox == null || NewValueTextBox == null)
            {
                return;
            }

            try
            {
                using (MoviesSystemDbContext context = new MoviesSystemDbContext())
                {
                    var actorNames = ActorNameTextBox.Text.Split(' ');
                    var firstName = actorNames[0];
                    var lastName = actorNames[1];
                    var newValue = NewValueTextBox.Text;
                    string[] newNames;
                    string[] newMovies;

                    var actorToUpdate = context.Actors
                                            .Where(
                                                    a => a.FirstName.ToLower() == firstName.ToLower() 
                                                    && a.LastName.ToLower() == lastName.ToLower()
                                                  )
                                            .FirstOrDefault();

                    actorToUpdate.Movies.Clear();

                    if ((bool)NameRadioButton.IsChecked)
                    {
                        newNames = newValue.Split(' ');
                        actorToUpdate.FirstName = newNames[0];
                        actorToUpdate.LastName = newNames[1];
                    }

                    else
                    {
                        newMovies = newValue.Split(',');
                        foreach (var newMovie in newMovies)
                        {
                            actorToUpdate.Movies.Add(LoadOrCreateMovie(context, newMovie));
                        }
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ShowMessage.ShowError(ex.Message);
                return;
            }

            updateActorWindow.Close();
        }

    }
}

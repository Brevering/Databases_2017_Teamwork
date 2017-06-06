using MoviesSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMovieSystem.Views
{
    /// <summary>
    /// Interaction logic for InsertWindowView.xaml
    /// </summary>
    public partial class InsertWindowView : Window
    {
        public InsertWindowView()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
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
                    newActor.Movies.Add(new Movie { Title = movie });
                }

                context.Actors.Add(newActor);
                context.SaveChanges();
            }
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            MoviesTextBox.Text = "";
        }
    }
}

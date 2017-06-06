using MoviesSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfMovieSystem.Behavior;

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

        //private ObservableCollection<Actor> actorsCollection;
        //public ObservableCollection<Actor> ActorsCollection
        //{
        //    get
        //    {
        //        if (actorsCollection == null)
        //        {
        //            actorsCollection = new ObservableCollection<Actor>();
        //        }
        //        return actorsCollection;
        //    }
        //    set
        //    {
        //        if (actorsCollection == null)
        //        {
        //            actorsCollection = new ObservableCollection<Actor>();
        //        }
        //        else
        //        {
        //            actorsCollection.Clear();
        //        }

        //        foreach (var item in value)
        //        {
        //            actorsCollection.Add(item);
        //        }
        //    }
        //}

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

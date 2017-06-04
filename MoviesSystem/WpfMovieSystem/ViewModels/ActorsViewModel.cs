using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMovieSystem.ViewModels
{
    public class ActorsViewModel : ViewModelBase, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "ActorsViewModel";
            }
        }

        public ActorsViewModel()
        {

        }
    }
}

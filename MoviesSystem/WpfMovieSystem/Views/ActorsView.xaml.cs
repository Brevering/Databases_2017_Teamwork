using MoviesSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfMovieSystem.ViewModels;

namespace WpfMovieSystem.Views
{
    /// <summary>
    /// Interaction logic for ActorsView.xaml
    /// </summary>
    public partial class ActorsView : UserControl
    {
        public ActorsView()
        {
            InitializeComponent();
            this.DataContext = new ActorsViewModel();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tema_2_MVP.ViewModels;

namespace Tema_2_MVP.Views
{
    public partial class PickCategory : Window
    {
        public ManagingCategoriesViewModel viewModel { get; set; }

        public MainViewModel MainView { get; set; }

        public PickCategory(MainViewModel mainView)
        {
            InitializeComponent();
            viewModel = new ManagingCategoriesViewModel();
            this.DataContext = viewModel;
            this.MainView = mainView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.MainView.Category = this.viewModel.SelectedCategory;

            Window window = this;
            window.Close();
        }
    }
}

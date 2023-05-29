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
using Tema_2_MVP.DataStorage;
using Tema_2_MVP.ViewModels;

namespace Tema_2_MVP.Views
{
    public partial class ManagingCategoriesWindow : Window
    {
        public ManagingCategoriesViewModel viewModel { get; set; }

        public ManagingCategoriesWindow()
        { 
            InitializeComponent();

            this.viewModel = new ManagingCategoriesViewModel();

            this.DataContext = viewModel;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(newCategory.Text != string.Empty)
            {
                this.viewModel.Categories.Add(newCategory.Text);

                var view = CollectionViewSource.GetDefaultView(this.viewModel.Categories);
                view.Refresh();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(this.viewModel.SelectedCategory != null)
            {
                this.viewModel.Categories.Remove(this.viewModel.SelectedCategory);
                var view = CollectionViewSource.GetDefaultView(this.viewModel.Categories);
                view.Refresh();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window window = this;
            XMLHelpers.SerializeCategories(this.viewModel.Categories);
            this.Close();
        }
    }
}

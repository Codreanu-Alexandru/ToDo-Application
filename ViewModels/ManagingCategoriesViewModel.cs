using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_2_MVP.DataStorage;

namespace Tema_2_MVP.ViewModels
{
    public class ManagingCategoriesViewModel : ViewModelBase
    {
        private List<String> categories;
        public List<String> Categories 
        { 
            get
            {
                return categories;
            }
            set
            {
                categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        private String selectedCategory;
        public String SelectedCategory 
        {
            get 
            { 
                return selectedCategory;
            }
            set 
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public ManagingCategoriesViewModel ()
        {
            Categories = XMLHelpers.Categories;
        }
    }
}

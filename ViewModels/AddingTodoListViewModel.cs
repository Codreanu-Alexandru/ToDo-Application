using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_2_MVP.DataStorage;

namespace Tema_2_MVP.ViewModels
{
    public class AddingTodoListViewModel : ViewModelBase
    {
        private string title;
        public String Title 
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private String selectedImage;
        public String SelectedImage
        {
            get
            {
                return selectedImage;
            }
            set
            {
                selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }
        private List<String> imagePaths;
        public List<String> ImagePaths
        {
            get 
            {
                return imagePaths;
            }
            set
            {
                imagePaths = value;
                OnPropertyChanged(nameof(ImagePaths));
            }
        }

        public AddingTodoListViewModel()
        {
            imagePaths = XMLHelpers.ImagePaths;
        }
    }
}

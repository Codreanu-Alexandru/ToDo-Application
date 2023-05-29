using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_2_MVP.Models;
using Task = Tema_2_MVP.Models.Task;

namespace Tema_2_MVP.ViewModels
{
    public class FindTaskViewModel : ViewModelBase
    {
        private String taskTitle;
        public String TaskTitle
        {
            get
            {
                return taskTitle;
            }
            set
            {
                taskTitle = value;
                OnPropertyChanged(nameof(TaskTitle));
            }
        }

        private ObservableCollection<TodoList> data;
        public ObservableCollection<TodoList> Data
        {
            get 
            {
                return data;
            }
            set 
            {
                data = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        private List<Finding> found;
        public List<Finding> Found
        {
            get 
            {
                return found;
            }
            set
            {
                found = value;
                OnPropertyChanged(nameof(Found));
            }
        }

        private String foundText;
        public String FoundText
        {
            get
            {
                return foundText;
            }
            set
            {
                foundText = value;
                OnPropertyChanged(nameof(FoundText));
            }
        }

        public FindTaskViewModel(ObservableCollection<TodoList> data)
        {
            this.Data = data;
            this.Found = new List<Finding>();
        }
    }
}

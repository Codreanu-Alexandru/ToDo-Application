using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema_2_MVP.Commands;
using Tema_2_MVP.DataStorage;
using Tema_2_MVP.Models;
using Task = Tema_2_MVP.Models.Task;

namespace Tema_2_MVP.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<TodoList> todoLists;
        public ObservableCollection<TodoList> TodoLists
        {
            get
            {
                return todoLists;
            }
            set
            {
                todoLists = value;
                OnPropertyChanged(nameof(TodoLists));
            }
        }

        private TodoList selectedTodoList;
        public TodoList SelectedTodoList
        {
            get
            {
                return selectedTodoList;
            }
            set
            {
                selectedTodoList = value;
                OnPropertyChanged(nameof(SelectedTodoList));
            }
        }

        private Task selectedTask;
        public Task SelectedTask
        {
            get
            {
                return selectedTask;
            }
            set
            {
                selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        private int today;
        public int Today 
        {
            get
            {
                return today;
            }
            set
            {
                today = value;
                OnPropertyChanged(nameof(Today));
            }
        }

        private int tomorrow;
        public int Tomorrow
        {
            get
            {
                return tomorrow;
            }
            set
            {
                tomorrow = value;
                OnPropertyChanged(nameof(Tomorrow));
            }
        }

        private int overdue;
        public int Overdue 
        { 
            get
            {
                return overdue;
            }
            set
            {
                overdue = value;
                OnPropertyChanged(nameof(Overdue));
            }
        }

        private int done;
        public int Done
        { 
            get
            {
                return done;
            }
            set
            {
                done = value;
                OnPropertyChanged(nameof(Done));
            }
        }

        private int toBeDone;
        public int ToBeDone 
        { 
            get
            {
                return toBeDone;
            }
            set
            {
                toBeDone = value;
                OnPropertyChanged(nameof(ToBeDone));
            }
        }

        private String category;
        public String Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public OpenAboutWindow OpenAbout { get; set; }

        public AboutCommandParamters AboutParamters { get; set; }

        public StatisticsRefreshCommand StatisticsCommand { get; set; }

        public MainViewModel()
        {
            OpenAbout = new OpenAboutWindow();
            AboutParamters = new AboutCommandParamters("Codreanu Alexandru", "10LF211");

            this.StatisticsCommand = new StatisticsRefreshCommand(this);

            Today = 0;
            Tomorrow = 0;
            Overdue = 0;
            Done = 0;
            ToBeDone = 0;
        }
    }
}

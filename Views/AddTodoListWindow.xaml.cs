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
using System.Collections.ObjectModel;
using Tema_2_MVP.Models;
using Tema_2_MVP.ViewModels;
using Task = Tema_2_MVP.Models.Task;

namespace Tema_2_MVP.Views
{
    public partial class AddTodoListWindow : Window
    {
        AddingTodoListViewModel viewModel { get;set; }

        public TodoList CurrentTodoList {  get; set; }

        public bool? Root { get; set; }

        public ObservableCollection<TodoList> TodoLists { get; set; }

        public AddTodoListWindow(TodoList todoList, bool root)
        {
            InitializeComponent();

            this.CurrentTodoList = todoList;
            this.Root = root;
            this.viewModel = new AddingTodoListViewModel();
            this.DataContext = viewModel;
        }

        public AddTodoListWindow(TodoList todoList)
        {
            InitializeComponent();

            this.CurrentTodoList = todoList;
            this.viewModel = new AddingTodoListViewModel();
            this.viewModel.Title = CurrentTodoList.Title;
            this.viewModel.SelectedImage = CurrentTodoList.Image;
            this.DataContext = viewModel;
        }

        public AddTodoListWindow(ObservableCollection<TodoList> todoLists)
        {
            InitializeComponent();

            this.TodoLists = todoLists;
            this.viewModel = new AddingTodoListViewModel();
            this.DataContext = viewModel;
        }

        private void AddTDL_Click(object sender, RoutedEventArgs e)
        {
            if(!Root.HasValue)
            {
                if(this.TodoLists ==  null)
                {
                    this.CurrentTodoList.Title = this.viewModel.Title;
                    this.CurrentTodoList.Image = this.viewModel.SelectedImage;
                }
                else
                {
                    this.TodoLists.Add(new TodoList()
                    {
                        Id = this.viewModel.Title.GetHashCode(),
                        Title = this.viewModel.Title,
                        Image = this.viewModel.SelectedImage,
                        Tasks = new ObservableCollection<Task>(),
                        SubLists = new ObservableCollection<TodoList>(),
                    });
                }
            }
            else
            {
                if (Root.Value)
                {
                    String oldTitle = this.CurrentTodoList.Title;
                    String oldImage = this.CurrentTodoList.Image;
                    int oldId = this.CurrentTodoList.Id;
                    ObservableCollection<Task> tasks = new ObservableCollection<Task>(this.CurrentTodoList.Tasks);
                    ObservableCollection<TodoList> todoLists = new ObservableCollection<TodoList>(this.CurrentTodoList.SubLists);

                    this.CurrentTodoList.Tasks.Clear();
                    this.CurrentTodoList.Image = this.viewModel.SelectedImage;
                    this.CurrentTodoList.Title = this.viewModel.Title;
                    this.CurrentTodoList.Id = this.viewModel.Title.GetHashCode();
                    this.CurrentTodoList.SubLists.Clear();
                    this.CurrentTodoList.SubLists = new ObservableCollection<TodoList>()
                {
                    new TodoList()
                    {
                        Id = oldId,
                        Title = oldTitle,
                        Image = oldImage,
                        Tasks = tasks,
                        SubLists = todoLists
                    }
                };
                }
                else
                {
                    this.CurrentTodoList.SubLists.Add(new TodoList()
                    {
                        Id = this.viewModel.Title.GetHashCode(),
                        Title = this.viewModel.Title,
                        Image = this.viewModel.SelectedImage,
                        Tasks = new ObservableCollection<Task>(),
                        SubLists = new ObservableCollection<TodoList>(),
                    });

                }
            }
            Window window = this;
            window.Close();
        }
    }
}

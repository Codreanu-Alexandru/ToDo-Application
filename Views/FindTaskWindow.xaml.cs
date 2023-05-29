using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Tema_2_MVP.Models;
using Tema_2_MVP.ViewModels;
using Task = Tema_2_MVP.Models.Task;

namespace Tema_2_MVP.Views
{
    /// <summary>
    /// Interaction logic for FindTaskWindow.xaml
    /// </summary>
    public partial class FindTaskWindow : Window
    {
        public ObservableCollection<TodoList> Data { get; set; }

        public FindTaskViewModel viewModel { get; set; }

        public FindTaskWindow(ObservableCollection<TodoList> data)
        {
            InitializeComponent();
            Data = data;
            this.viewModel = new FindTaskViewModel(data);

            this.DataContext = viewModel;
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            if(this.viewModel.TaskTitle != string.Empty)
            {
                if(this.viewModel.Found.Count > 0)
                {
                    this.viewModel.Found.Clear();
                }

                ObservableCollection<TodoList> todoLists = this.viewModel.Data;
                List<Task> allTasks = new List<Task>();
                List<TodoList> allTodoLists = new List<TodoList>();
                Stack<TodoList> stack = new Stack<TodoList>(todoLists);

                while (stack.Count > 0)
                {
                    TodoList todoList = stack.Pop();
                    allTodoLists.Add(todoList);
                    allTasks.AddRange(todoList.Tasks);

                    if (todoList.SubLists != null)
                    {
                        foreach (TodoList subList in todoList.SubLists)
                        {
                            stack.Push(subList);
                        }
                    }
                }

                List<Task> auxList = allTasks.FindAll(x => x.Title.ToLower().Contains(this.viewModel.TaskTitle.ToLower()));

                foreach(TodoList todoList in allTodoLists)
                {
                    foreach(Task task in auxList)
                    {
                        if(todoList.Tasks.Contains(task))
                        {
                            this.viewModel.Found.Add(new Finding()
                            {
                                Task = task,
                                TaskParentImage = todoList.Image,
                                TaskParentTitle = todoList.Title
                            }
                            );
                        }
                    }
                }

                var view = CollectionViewSource.GetDefaultView(this.viewModel.Found);
                view.Refresh();
                this.viewModel.FoundText = this.viewModel.Found.Count.ToString() + " items found.";
            }
            else
            {
                this.viewModel.FoundText = "0 items found";
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window window = this;
            this.Close();
        }
    }
}

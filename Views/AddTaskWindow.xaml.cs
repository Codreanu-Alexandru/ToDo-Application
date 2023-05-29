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
using Tema_2_MVP.Models;
using Tema_2_MVP.ViewModels;
using Task = Tema_2_MVP.Models.Task;

namespace Tema_2_MVP.Views
{
    public partial class AddTaskWindow : Window
    {
        public Task Task { get; set; }

        public Task CurrentTask { get; set; }

        public TodoList CurrentTodoList { get; set; }

        public AddingTaskViewModel CurrentTaskViewModel { get; set; }

        public AddTaskWindow(TodoList currentTodoList)
        {
            InitializeComponent();
            
            CurrentTaskViewModel = new AddingTaskViewModel(currentTodoList);
            this.DataContext = CurrentTaskViewModel;
            this.CurrentTodoList = currentTodoList;

        }

        public AddTaskWindow(Task currentTask)
        {
            InitializeComponent();
            CurrentTask = currentTask; 
            CurrentTaskViewModel = new AddingTaskViewModel(currentTask);
            this.DataContext = CurrentTaskViewModel;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentTaskViewModel.TitleText != String.Empty ||
                CurrentTaskViewModel.DescriptionText != String.Empty)
            {
                Task newTask = new Task()
                {
                    Title = CurrentTaskViewModel.TitleText,
                    Description = CurrentTaskViewModel.DescriptionText,
                    Category = CurrentTaskViewModel.Categories[CurrentTaskViewModel.CategoryIndex],
                    Priority = CurrentTaskViewModel.Priorities[CurrentTaskViewModel.PriorityIndex],
                    Status = CurrentTaskViewModel.Status[CurrentTaskViewModel.StatusIndex],
                    Dealine = CurrentTaskViewModel.Deadline
                };

                this.Task = newTask;

                if(CurrentTodoList != null)
                {
                    CurrentTodoList.Tasks.Add(newTask);
                }
                else
                {
                    CurrentTask.Title = this.Task.Title;
                    CurrentTask.Description = this.Task.Description;
                    CurrentTask.Status = this.Task.Status;
                    CurrentTask.Priority = this.Task.Priority;
                    CurrentTask.Category = this.Task.Category;
                    CurrentTask.Dealine = this.Task.Dealine;
                }

                Window window = this;
                window.Close();

            }

            this.Task = null;
        }
    }
}

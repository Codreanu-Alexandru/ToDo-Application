using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tema_2_MVP.Models;
using Tema_2_MVP.ViewModels;
using Tema_2_MVP.Views;
using Task = Tema_2_MVP.Models.Task;

namespace Tema_2_MVP.Commands
{
    public class StatisticsRefreshCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private MainViewModel mainView;

        public StatisticsRefreshCommand(MainViewModel mainView)
        {
            this.mainView = mainView;
        }

        public bool CanExecute(object parameter)
        {
            return mainView != null;
        }

        public void Execute(object parameter)
        {
            ObservableCollection<TodoList> todoLists = this.mainView.TodoLists;
            List<Task> allTasks = new List<Task>();
            Stack<TodoList> stack = new Stack<TodoList>(todoLists);

            while (stack.Count > 0)
            {
                TodoList todoList = stack.Pop();
                allTasks.AddRange(todoList.Tasks);

                if (todoList.SubLists != null)
                {
                    foreach (TodoList subList in todoList.SubLists)
                    {
                        stack.Push(subList);
                    }
                }
            }

            this.mainView.Done = 0;
            this.mainView.Tomorrow = 0;
            this.mainView.Overdue = 0;
            this.mainView.ToBeDone = 0;
            this.mainView.Done = 0;

            foreach(Task task in allTasks)
            {
                if(task.Status == Models.TaskStatus.Done)
                {
                    this.mainView.Done++;
                }
                else
                {
                    this.mainView.ToBeDone++;
                }

                if(task.Dealine.Date == DateTime.Today.Date)
                {
                    this.mainView.Today++;
                }

                if(task.Dealine.Date == DateTime.Today.AddDays(1).Date)
                {
                    this.mainView.Tomorrow++;
                }

                if(task.Dealine.Date < DateTime.Today.Date)
                {
                    this.mainView.Overdue++;
                }
            }
        }
    }
}

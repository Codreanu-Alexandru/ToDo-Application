using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema_2_MVP.DataStorage;
using Tema_2_MVP.Models;
using Task = Tema_2_MVP.Models.Task;
using TaskStatus = Tema_2_MVP.Models.TaskStatus;

namespace Tema_2_MVP.ViewModels
{
    public class AddingTaskViewModel
    {
        public String TitleText { get; set; }

        public String DescriptionText { get; set; }

        public ObservableCollection<String> Categories { get; set; }
        public int CategoryIndex { get; set; }

        public ObservableCollection<Priority> Priorities { get; set; }
        public int PriorityIndex { get; set; }

        public ObservableCollection<Models.TaskStatus> Status { get; set; }
        public int StatusIndex { get; set; }

        public DateTime Deadline { get; set; }

        public TodoList CurrentTodoList { get; set; }

        public AddingTaskViewModel(TodoList currentTodoList)
        {
            CurrentTodoList = currentTodoList;
            Deadline = DateTime.Now.AddDays(1);
            Priorities = new ObservableCollection<Priority>()
            {
                Priority.Low,
                Priority.Medium,
                Priority.High
            };
            Status = new ObservableCollection<Models.TaskStatus>()
            {
                TaskStatus.Created,
                TaskStatus.Ongoing,
                TaskStatus.Done
            };
            Categories = new ObservableCollection<string>(XMLHelpers.Categories);

        }

        public AddingTaskViewModel(Task task)
        {
            Priorities = new ObservableCollection<Priority>()
            {
                Priority.Low,
                Priority.Medium,
                Priority.High
            };
            Status = new ObservableCollection<Models.TaskStatus>()
            {
                TaskStatus.Created,
                TaskStatus.Ongoing,
                TaskStatus.Done
            };
            Categories = new ObservableCollection<string>(XMLHelpers.Categories);

            TitleText = task.Title;
            DescriptionText = task.Description;
            PriorityIndex = Priorities.IndexOf(task.Priority);
            StatusIndex = Status.IndexOf(task.Status);
            CategoryIndex = Categories.IndexOf(task.Category);
            Deadline = task.Dealine;
        }
    }
}

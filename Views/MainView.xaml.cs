using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tema_2_MVP.DataStorage;
using Tema_2_MVP.Models;
using Tema_2_MVP.ViewModels;
using Task = Tema_2_MVP.Models.Task;
using TaskStatus = Tema_2_MVP.Models.TaskStatus;

namespace Tema_2_MVP.Views
{
    public partial class MainView : UserControl
    {
        MainViewModel mainView;

        public MainView()
        {
            InitializeComponent();

            this.mainView = new MainViewModel();

            this.DataContext = mainView;
        }

        private void TreeTask_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TodoList todoList = e.NewValue as TodoList;
            if(todoList != null)
            {
                this.mainView.SelectedTodoList = todoList;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Task task = e.AddedItems[0] as Task;
                if (task != null)
                {
                    this.mainView.SelectedTask = task;
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            Task task = (Task)checkBox.DataContext;
            task.Status = Models.TaskStatus.Done;

            var view = CollectionViewSource.GetDefaultView(this.mainView.SelectedTodoList.Tasks);
            view.Refresh();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            this.mainView.TodoLists = XMLHelpers.Deserialize();
            ICommand command = this.mainView.StatisticsCommand;

            if(command.CanExecute(0))
            {
                command.Execute(0);
            }
            
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to save?", "Saving request", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                XMLHelpers.Serialize(this.mainView.TodoLists);
            }

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            if(this.mainView.TodoLists != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to start over? All of your previous lists will be lost.", "Deleting request", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK) 
                {
                    this.mainView.TodoLists.Clear();
                    this.mainView.TodoLists = new ObservableCollection<TodoList>();
                    MessageBox.Show("You need SAVE to also confirm the deleting of the previous lists. And to SAVE the new one", "Note");
                }

            }
            else
            {
                MessageBox.Show("No lists opened. Once you create the new lists and SAVE them the previous list will be lost.","Note");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if(this.mainView.TodoLists != null)
            {
                Save_Click(sender, e);

            }
            Environment.Exit(0);
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if(this.mainView.SelectedTodoList != null)
            {

                AddTaskWindow addTaskWindow = new AddTaskWindow(this.mainView.SelectedTodoList);
                while ((bool)addTaskWindow.ShowDialog())
                {
                }

            }
            else
            {
                MessageBox.Show("You need to have a to do list selected.");
            }
        }

        private void ManageCategories_Click(object sender, RoutedEventArgs e)
        {
            var managingCategories = new ManagingCategoriesWindow();
            managingCategories.Show();
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTask != null)
            {

                AddTaskWindow addTaskWindow = new AddTaskWindow(this.mainView.SelectedTask);
                while ((bool)addTaskWindow.ShowDialog())
                {
                }

                var view = CollectionViewSource.GetDefaultView(this.mainView.SelectedTodoList.Tasks);
                view.Refresh();

                this.mainView.SelectedTask = this.mainView.SelectedTask;
            }
            else
            {
                MessageBox.Show("You need to have a to do list selected.");
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if(this.mainView.SelectedTask != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to DELETE this task?", "Deleting request", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    this.mainView.SelectedTodoList.Tasks.Remove(this.mainView.SelectedTask);
                }
            }
            else
            {
                MessageBox.Show("You cannot get rid of something you don't have.", "Note");
            }
        }

        private void SetDoneTask_Click(object sender, RoutedEventArgs e)
        {
            if(this.mainView.SelectedTask != null)
            {
                this.mainView.SelectedTask.Status = TaskStatus.Done;

                var view = CollectionViewSource.GetDefaultView(this.mainView.SelectedTodoList.Tasks);
                view.Refresh();
            }
            else
            {
                MessageBox.Show("You cannot get finish something you don't have.", "Note");
            }
        }

        private void MoveTaskUp_Click(object sender, RoutedEventArgs e)
        {
            if(this.mainView.SelectedTask != null)
            {
                if (this.mainView.SelectedTodoList.Tasks.Count >= 2 &&
                    this.mainView.SelectedTodoList.Tasks.IndexOf(this.mainView.SelectedTask) >= 1)
                {
                    int indexOfCurrent = this.mainView.SelectedTodoList.Tasks.IndexOf(this.mainView.SelectedTask);
                    var aux = this.mainView.SelectedTodoList.Tasks[indexOfCurrent - 1];
                    this.mainView.SelectedTodoList.Tasks[indexOfCurrent - 1] = this.mainView.SelectedTask;
                    this.mainView.SelectedTodoList.Tasks[indexOfCurrent] = aux;

                }
            }
        }

        private void MoveTaskDown_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTask != null)
            {
                if (this.mainView.SelectedTodoList.Tasks.Count >= 2 &&
                    this.mainView.SelectedTodoList.Tasks.IndexOf(this.mainView.SelectedTask) < this.mainView.SelectedTodoList.Tasks.Count - 1)
                {
                    int indexOfCurrent = this.mainView.SelectedTodoList.Tasks.IndexOf(this.mainView.SelectedTask);
                    var aux = this.mainView.SelectedTodoList.Tasks[indexOfCurrent + 1];
                    this.mainView.SelectedTodoList.Tasks[indexOfCurrent + 1] = this.mainView.SelectedTask;
                    this.mainView.SelectedTodoList.Tasks[indexOfCurrent] = aux;

                }
            }
        }

        private void FindTask_Click(object sender, RoutedEventArgs e)
        {
            FindTaskWindow findTaskWindow = new FindTaskWindow(this.mainView.TodoLists);
            findTaskWindow.ShowDialog();
        }

        private void SortPriority_Click(object sender, RoutedEventArgs e)
        {
            if(this.mainView.SelectedTodoList != null)
            {
                var sortedTasks = this.mainView.SelectedTodoList.Tasks.OrderBy(x =>
                {
                    if(x.Priority == Priority.Low)
                    {
                        return -1;
                    }
                    else if(x.Priority == Priority.High)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                });

                var sortedObservableTasks = new ObservableCollection<Task>(sortedTasks);
                this.mainView.SelectedTodoList.Tasks.Clear();
                foreach(var task in sortedObservableTasks)
                {
                    this.mainView.SelectedTodoList.Tasks.Add(task);
                }
                var view = CollectionViewSource.GetDefaultView(this.mainView.SelectedTodoList.Tasks);
                view.Refresh();
            }
        }

        private void SortDealine_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTodoList != null)
            {
                var sortedTasks = this.mainView.SelectedTodoList.Tasks.OrderBy(x =>
                {
                    return x.Dealine;
                });

                var sortedObservableTasks = new ObservableCollection<Task>(sortedTasks);
                this.mainView.SelectedTodoList.Tasks.Clear();
                foreach (var task in sortedObservableTasks)
                {
                    this.mainView.SelectedTodoList.Tasks.Add(task);
                }
                var view = CollectionViewSource.GetDefaultView(this.mainView.SelectedTodoList.Tasks);
                view.Refresh();
            }
        }

        /*Note to self :
         When uploading to Git, make one function for all these filters*/
        private void FilterDone_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTodoList != null)
            {
                var original = new ObservableCollection<Task>();

                foreach(var task in this.mainView.SelectedTodoList.Tasks)
                {
                    original.Add(task);
                }
                var filteredTasks = this.mainView.SelectedTodoList.Tasks.Where(x => x.Status == TaskStatus.Done);

                var filteredObservableTasks = new ObservableCollection<Task>(filteredTasks);
                this.mainView.SelectedTodoList.Tasks.Clear();
                foreach (var task in filteredObservableTasks)
                {
                    this.mainView.SelectedTodoList.Tasks.Add(task);
                }
                var view = CollectionViewSource.GetDefaultView(this.mainView.SelectedTodoList.Tasks);
                view.Refresh();

                this.mainView.SelectedTodoList.Tasks = original;
            }
        }

        private void FilterOverdue_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTodoList != null)
            {
                var original = new ObservableCollection<Task>();

                foreach (var task in this.mainView.SelectedTodoList.Tasks)
                {
                    original.Add(task);
                }
                var filteredTasks = this.mainView.SelectedTodoList.Tasks.Where(x => x.Dealine.Date < DateTime.Today.Date);

                var filteredObservableTasks = new ObservableCollection<Task>(filteredTasks);
                this.mainView.SelectedTodoList.Tasks.Clear();
                foreach (var task in filteredObservableTasks)
                {
                    this.mainView.SelectedTodoList.Tasks.Add(task);
                }
                var view = CollectionViewSource.GetDefaultView(this.mainView.SelectedTodoList.Tasks);
                view.Refresh();

                this.mainView.SelectedTodoList.Tasks = original;
            }
        }

        private void FilterUndone_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTodoList != null)
            {
                var original = new ObservableCollection<Task>();

                foreach (var task in this.mainView.SelectedTodoList.Tasks)
                {
                    original.Add(task);
                }
                var filteredTasks = this.mainView.SelectedTodoList.Tasks.Where(
                    x => x.Dealine.Date < DateTime.Today.Date && x.Status != TaskStatus.Done);

                var filteredObservableTasks = new ObservableCollection<Task>(filteredTasks);
                this.mainView.SelectedTodoList.Tasks.Clear();
                foreach (var task in filteredObservableTasks)
                {
                    this.mainView.SelectedTodoList.Tasks.Add(task);
                }
                var view = CollectionViewSource.GetDefaultView(this.mainView.SelectedTodoList.Tasks);
                view.Refresh();

                this.mainView.SelectedTodoList.Tasks = original;
            }
        }

        private void FilterFuture_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTodoList != null)
            {
                var original = new ObservableCollection<Task>();

                foreach (var task in this.mainView.SelectedTodoList.Tasks)
                {
                    original.Add(task);
                }
                var filteredTasks = this.mainView.SelectedTodoList.Tasks.Where(
                    x => x.Dealine.Date > DateTime.Today.Date && x.Status != TaskStatus.Done);

                var filteredObservableTasks = new ObservableCollection<Task>(filteredTasks);
                this.mainView.SelectedTodoList.Tasks.Clear();
                foreach (var task in filteredObservableTasks)
                {
                    this.mainView.SelectedTodoList.Tasks.Add(task);
                }
                var view = CollectionViewSource.GetDefaultView(this.mainView.SelectedTodoList.Tasks);
                view.Refresh();

                this.mainView.SelectedTodoList.Tasks = original;
            }
        }

        private void TDLDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTodoList != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to DELETE this list?", "Deleting request", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    this.mainView.TodoLists.Remove(this.mainView.SelectedTodoList);
                }
            }
            else
            {
                MessageBox.Show("You cannot get rid of something you don't have.", "Note");
            }
        }

        private void TDLMoveUp_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTodoList != null)
            {
                if (this.mainView.TodoLists.Count >= 2 &&
                    this.mainView.TodoLists.IndexOf(this.mainView.SelectedTodoList) >= 1)
                {
                    int indexOfCurrent = this.mainView.TodoLists.IndexOf(this.mainView.SelectedTodoList);
                    var aux = this.mainView.TodoLists[indexOfCurrent - 1];
                    this.mainView.TodoLists[indexOfCurrent - 1] = this.mainView.SelectedTodoList;
                    this.mainView.TodoLists[indexOfCurrent] = aux;

                }
            }
        }

        private void TDLMoveDown_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTodoList != null)
            {
                if (this.mainView.TodoLists.Count >= 2 &&
                    this.mainView.TodoLists.IndexOf(this.mainView.SelectedTodoList) <= this.mainView.TodoLists.Count - 1)
                {
                    int indexOfCurrent = this.mainView.TodoLists.IndexOf(this.mainView.SelectedTodoList);
                    var aux = this.mainView.TodoLists[indexOfCurrent + 1];
                    this.mainView.TodoLists[indexOfCurrent + 1] = this.mainView.SelectedTodoList;
                    this.mainView.TodoLists[indexOfCurrent] = aux;

                }
            }
        }

        private void TDLAddRoot_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTodoList != null)
            {

                AddTodoListWindow addTodoList = new AddTodoListWindow(this.mainView.SelectedTodoList, true);
                while ((bool)addTodoList.ShowDialog())
                {
                }

                var view = CollectionViewSource.GetDefaultView(this.mainView.TodoLists);
                view.Refresh();

            }
            else
            {
                AddTodoListWindow addTodoList = new AddTodoListWindow(this.mainView.TodoLists);
                while ((bool)addTodoList.ShowDialog())
                {
                }

                var view = CollectionViewSource.GetDefaultView(this.mainView.TodoLists);
                view.Refresh();
            }
        }

        private void TDLAddSubList_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTodoList != null)
            {

                AddTodoListWindow addTodoList = new AddTodoListWindow(this.mainView.SelectedTodoList, false);
                while ((bool)addTodoList.ShowDialog())
                {
                }

                var view = CollectionViewSource.GetDefaultView(this.mainView.TodoLists);
                view.Refresh();
            }
            else
            {
                MessageBox.Show("Cannot add to what is not there.", "Note");
            }
        }

        private void TDLEdit_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTodoList != null)
            {

                AddTodoListWindow addTodoList = new AddTodoListWindow(this.mainView.SelectedTodoList);
                while ((bool)addTodoList.ShowDialog())
                {
                }

                var view = CollectionViewSource.GetDefaultView(this.mainView.TodoLists);
                view.Refresh();
            }
            else
            {
                MessageBox.Show("Cannot change what is not there.", "Note");
            }
        }

        private void FilterGroup_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainView.SelectedTodoList != null)
            {
                if (this.mainView.SelectedTodoList != null)
                {

                    PickCategory pickCategory = new PickCategory(this.mainView);
                    while ((bool)pickCategory.ShowDialog())
                    {
                    }

                    var original = new ObservableCollection<Task>();

                    foreach (var task in this.mainView.SelectedTodoList.Tasks)
                    {
                        original.Add(task);
                    }
                    var filteredTasks = this.mainView.SelectedTodoList.Tasks.Where(x => x.Category == this.mainView.Category);

                    var filteredObservableTasks = new ObservableCollection<Task>(filteredTasks);
                    this.mainView.SelectedTodoList.Tasks.Clear();
                    foreach (var task in filteredObservableTasks)
                    {
                        this.mainView.SelectedTodoList.Tasks.Add(task);
                    }
                    var view = CollectionViewSource.GetDefaultView(this.mainView.SelectedTodoList.Tasks);
                    view.Refresh();

                    this.mainView.SelectedTodoList.Tasks = original;

                }
                else
                {
                    MessageBox.Show("You need to have a to do list selected.");
                }

            }
        }
    }
}

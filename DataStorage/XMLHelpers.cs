using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;
using Tema_2_MVP.Models;
using Task = Tema_2_MVP.Models.Task;

namespace Tema_2_MVP.DataStorage
{
    static class XMLHelpers
    {
        static public List<String> Categories { get; set; }

        static public List<String> ImagePaths { get; set; }

        static XMLHelpers()
        {
            //Categories = PopulateCategories();
            //SerializeCategories(Categories);
            Categories = DeserializeCategories();
            ImagePaths = new List<string>
            {
                "../Images/1.png",
                "../Images/2.png",
                "../Images/3.png",
                "../Images/4.png",
                "../Images/5.png",
                "../Images/6.png",
                "../Images/7.png",
                "../Images/8.png",
                "../Images/9.png",
                "../Images/10.png",

            };

        }

        static public List<String> PopulateCategories()
        {
            return new List<string>()
            {
                 "Homework",
                "School",
                "Work",
                "Home",
                "Events",
                "Meetings",
                "Special",
                "Seasonal",
                "Others"
            };
        }

        static public void SerializeCategories(List<String> categories)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<String>));
            using (var writer = new StreamWriter("Categories.xml"))
            {
                xmlSerializer.Serialize(writer, categories);
            }
        }

        static public List<String> DeserializeCategories()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<String>));
            List<String> categories = new List<String>();
            using (var reader = new StreamReader("Categories.xml"))
            {
                categories = (List<String>)xmlSerializer.Deserialize(reader);
            }

            return categories;
        }

        static public ObservableCollection<TodoList> Populate()
        {
            ObservableCollection<TodoList> toDoLists = new ObservableCollection<TodoList>()
            {
                new TodoList()
                {
                    Id = 0,
                    Title = "First thing",
                    Image = "../Images/1.png",
                    Tasks = new ObservableCollection<Task>()
                    {
                        new Task()
                        {
                            Priority = Priority.Low,
                            Title = "First task of First thing",
                            Description = "This is a hardcoded description.",
                            Status = Models.TaskStatus.Created,
                            Dealine = DateTime.Now.AddDays(2),
                            Category = Categories[0]
                        },
                        new Task()
                        {
                            Priority = Priority.Medium,
                            Title = "Second task of First thing",
                            Description = "This is yet another hardcoded description.",
                            Status = Models.TaskStatus.Ongoing,
                            Dealine = DateTime.Now.AddDays(2),
                            Category = Categories[1]
                        }
                    },
                    SubLists = new ObservableCollection<TodoList>()
                    {
                        new TodoList()
                        {
                            Id = 1,
                            Title = "Second thing",
                            Image = "../Images/8.png",
                            Tasks = new ObservableCollection<Task>()
                            {
                                new Task()
                                {
                                    Priority = Priority.High,
                                    Title = "First task of Second thing",
                                    Description = "This is a hardcoded description. 3",
                                    Status = Models.TaskStatus.Created,
                                    Dealine = DateTime.Now.AddDays(-2),
                                    Category = Categories[1]
                                },
                                new Task()
                                {
                                    Priority = Priority.Low,
                                    Title = "Second task of Second thing",
                                    Description = "This is yet another hardcoded description. 3",
                                    Status = Models.TaskStatus.Done,
                                    Dealine = DateTime.Now.AddDays(-2),
                                    Category = Categories[1]
                                }
                            }
                        }
                    }
                },
                new TodoList()
                {
                    Id = 1,
                    Title = "Third thing",
                    Image = "../Images/6.png",
                    Tasks = new ObservableCollection<Task>()
                    {
                        new Task()
                        {
                            Priority = Priority.Low,
                            Title = "First task of Third thing",
                            Description = "This is a hardcoded description. 2",
                            Status = Models.TaskStatus.Created,
                            Dealine = DateTime.Now,
                            Category = Categories[8]
                        },
                        new Task()
                        {
                            Priority = Priority.Medium,
                            Title = "Second task of Third thing",
                            Description = "This is yet another hardcoded description. 2",
                            Status = Models.TaskStatus.Done,
                            Dealine = DateTime.Now,
                            Category = Categories[8]
                        }
                    }
                },
                new TodoList()
                {
                    Id = 2,
                    Title = "Fourth thing",
                    Image = "../Images/5.png",
                    Tasks = new ObservableCollection<Task>()
                    {
                        new Task()
                        {
                            Priority = Priority.Low,
                            Title = "First task of Fourth thing",
                            Description = "This is a hardcoded description.",
                            Status = Models.TaskStatus.Created,
                            Dealine = DateTime.Now,
                            Category = Categories[1]
                        },
                        new Task()
                        {
                            Priority = Priority.Medium,
                            Title = "Second task of Fourth thing",
                            Description = "This is yet another hardcoded description.",
                            Status = Models.TaskStatus.Ongoing,
                            Dealine = DateTime.Now,
                            Category =  Categories[0]
                        }
                    },
                    SubLists = new ObservableCollection<TodoList>()
                    {
                        new TodoList()
                        {
                            Id = 3,
                            Title = "Fifth thing",
                            Image = "../Images/9.png",
                            Tasks = new ObservableCollection<Task>()
                            {
                                new Task()
                                {
                                    Priority = Priority.High,
                                    Title = "First task of Fifth thing",
                                    Description = "This is a hardcoded description. 3",
                                    Status = Models.TaskStatus.Created,
                                    Dealine = DateTime.Now,
                                    Category =  Categories[3]
                                },
                                new Task()
                                {
                                    Priority = Priority.Low,
                                    Title = "Second task of Fifth thing",
                                    Description = "This is yet another hardcoded description. 3",
                                    Status = Models.TaskStatus.Done,
                                    Dealine = DateTime.Now,
                                    Category =  Categories[1]
                                }
                            },
                            SubLists = new ObservableCollection<TodoList>
                            {
                                new TodoList()
                                {
                                    Id = 3,
                                    Title = "Sixth thing",
                                    Image = "../Images/7.png",
                                    Tasks = new ObservableCollection<Task>()
                                    {
                                        new Task()
                                        {
                                            Priority = Priority.High,
                                            Title = "First task of Sixth thing",
                                            Description = "This is a hardcoded description. 3",
                                            Status = Models.TaskStatus.Created,
                                            Dealine = DateTime.Now,
                                            Category =  Categories[0]
                                        },
                                        new Task()
                                        {
                                            Priority = Priority.Low,
                                            Title = "Second task of Sixth thing",
                                            Description = "This is yet another hardcoded description. 3",
                                            Status = Models.TaskStatus.Done,
                                            Dealine = DateTime.Now,
                                            Category =  Categories[0]
                                        }
                                    }
                                }


                            }
                        }
                    }
                }
            };
            return toDoLists;
        }

        static public void Serialize(ObservableCollection<TodoList> todoLists)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<TodoList>));
            using (var writer = new StreamWriter("GroupList.xml"))
            {
                xmlSerializer.Serialize(writer, todoLists);
            }
        }

        static public ObservableCollection<TodoList> Deserialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer (typeof(ObservableCollection<TodoList>));
            ObservableCollection<TodoList> allData = new ObservableCollection<TodoList>();
            using (var reader = new StreamReader("GroupList.xml"))
            {
                allData = (ObservableCollection<TodoList>) xmlSerializer.Deserialize(reader);
            }

            return allData;
        }
    }
}

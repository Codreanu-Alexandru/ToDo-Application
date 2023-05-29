using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tema_2_MVP.Models
{
    [Serializable]
    public class TodoList
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public String Title { get; set; }
        [XmlAttribute]
        public String Image { get; set; }
        [XmlArray]
        public ObservableCollection<TodoList> SubLists { get; set; }
        [XmlArray]
        public ObservableCollection<Task> Tasks { get; set; }

    }
}

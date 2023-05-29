using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tema_2_MVP.Models
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum TaskStatus
    {
        Created,
        Ongoing,
        Done
    }

    [Serializable]
    public class Task
    {
        [XmlAttribute]
        public Priority Priority { get; set; }
        [XmlAttribute]
        public string Title { get; set; }
        [XmlAttribute]
        public string Description { get; set; }
        [XmlAttribute]
        public TaskStatus Status { get; set; }
        [XmlAttribute]
        public DateTime Dealine { get; set; }
        [XmlAttribute]
        public String Category { get; set; }
    }
}

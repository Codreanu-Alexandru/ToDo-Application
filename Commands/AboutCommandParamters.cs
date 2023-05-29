using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_2_MVP.Commands
{
    public class AboutCommandParamters
    {
        public String Name { get; set; }
        public String Group { get; set; }

        public AboutCommandParamters(String name, String group)
        {
            this.Name = name;
            this.Group = group;
        }
    }
}

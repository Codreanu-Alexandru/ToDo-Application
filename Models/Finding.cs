using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_2_MVP.Models
{
    public class Finding
    {
        public Task Task { get; set; }
        public string TaskParentTitle { get; set; }
        public string TaskParentImage { get; set; }
    }
}

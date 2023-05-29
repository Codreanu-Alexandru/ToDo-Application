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
using Tema_2_MVP.Commands;

namespace Tema_2_MVP.Views
{
    public partial class AboutWindow : Window
    {
        public AboutWindow(AboutCommandParamters Parameter)
        {
            InitializeComponent();
            Nume.Text = Parameter.Name;
            Grupa.Text = Parameter.Group;
        }
    }
}

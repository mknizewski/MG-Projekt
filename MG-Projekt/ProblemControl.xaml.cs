using MG_Projekt.BOL.Interfaces;
using MG_Projekt.Infrastructure.Factories;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MG_Projekt
{
    /// <summary>
    /// Interaction logic for ProblemControl.xaml
    /// </summary>
    public partial class ProblemControl : UserControl, IControl
    {
        public ProblemControl()
        {
            InitializeComponent();
        }

        public bool CheckPermission()
        {
            return true;
        }

        public void LinkToMethod()
        {
            MainWindow mainWindow = ControlFactory.GetMainWindowInstance();
            mainWindow.CheckingMethod = CheckPermission;
        }
    }
}

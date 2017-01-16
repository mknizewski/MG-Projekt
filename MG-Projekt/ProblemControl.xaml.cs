using MG_Projekt.BOL.Interfaces;
using MG_Projekt.Infrastructure.Factories;
using System.Windows.Controls;

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
            LinkToMethod();
        }

        public bool CheckPermission()
        {
            return true;
        }

        public void LinkToMethod()
        {
            MainWindow mainWindow = ControlFactory.GetMainWindowInstance();
            mainWindow.CheckingSectionMethod = CheckPermission;
        }
    }
}
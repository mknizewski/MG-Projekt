using MG_Projekt.BOL.Interfaces;
using MG_Projekt.Infrastructure.Factories;
using System.Windows;
using System.Windows.Controls;

namespace MG_Projekt
{
    /// <summary>
    /// Interaction logic for ParametersControl.xaml
    /// </summary>
    public partial class ParametersControl : UserControl, IControl
    {
        public ParametersControl()
        {
            InitializeComponent();
            LinkToMethod();
        }

        public bool CheckPermission()
        {
            MessageBox.Show("Nie mozna kurde");
            return false;
        }

        public void LinkToMethod()
        {
            MainWindow mainWindow = ControlFactory.GetMainWindowInstance();
            mainWindow.CheckingSectionMethod = CheckPermission;
        }
    }
}
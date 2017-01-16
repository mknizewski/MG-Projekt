using MG_Projekt.BOL.Interfaces;
using MG_Projekt.Infrastructure.Factories;
using System.Windows;
using System.Windows.Controls;

namespace MG_Projekt
{
    /// <summary>
    /// Interaction logic for AlgorithmControl.xaml
    /// </summary>
    public partial class AlgorithmControl : UserControl, IControl
    {
        public AlgorithmControl()
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
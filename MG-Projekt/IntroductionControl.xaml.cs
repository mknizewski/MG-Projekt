using MG_Projekt.BOL.Interfaces;
using System.Windows.Controls;
using MG_Projekt.Infrastructure.Factories;
using System.Windows;

namespace MG_Projekt
{
    /// <summary>
    /// Interaction logic for IntroductionControl.xaml
    /// </summary>
    public partial class IntroductionControl : UserControl, IControl
    {
        public IntroductionControl()
        {
            InitializeComponent();
            LinkToMethod();
        }

        public void LinkToMethod()
        {
            MainWindow mainWindow = ControlFactory.GetMainWindowInstance();
            mainWindow.CheckingMethod = CheckPermission;
        }

        public bool CheckPermission()
        {
            return true;
        }
    }
}

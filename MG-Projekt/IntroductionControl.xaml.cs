using MG_Projekt.BOL.Interfaces;
using MG_Projekt.Infrastructure.Factories;
using System.Windows.Controls;

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
            mainWindow.CheckingSectionMethod = CheckPermission;
        }

        public bool CheckPermission()
        {
            return true;
        }
    }
}
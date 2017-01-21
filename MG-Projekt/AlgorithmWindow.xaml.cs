using MG_Projekt.BOL.Managers;
using System.Windows;
using System.ComponentModel;
using MG_Projekt.Infrastructure.Factories;
using System.Threading;

namespace MG_Projekt
{
    /// <summary>
    /// Interaction logic for AlgorithmWindow.xaml
    /// </summary>
    public partial class AlgorithmWindow : Window
    {
        private ParametersManager _paremetersManager;

        public AlgorithmWindow(ParametersManager paramentersManager)
        {
            InitializeComponent();

            this._paremetersManager = paramentersManager;
        }

        public void Calculate()
        {
            Thread.Sleep(10000);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MainWindow mainWindow = ControlFactory.GetMainWindowInstance();

            mainWindow.CurrentControl = ControlsType.Parameters;
            mainWindow.CheckSection();
            
            base.OnClosing(e);
        }
    }
}

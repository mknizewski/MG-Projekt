using MG_Projekt.BOL.Managers;
using System.Windows;
using System.ComponentModel;
using MG_Projekt.Infrastructure.Factories;

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

        protected override void OnClosing(CancelEventArgs e)
        {
            MainWindow mainWindow = ControlFactory.GetMainWindowInstance();

            mainWindow.CurrentControl = ControlsType.Parameters;
            mainWindow.CheckSection();
            
            base.OnClosing(e);
        }
    }
}

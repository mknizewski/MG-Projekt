using MG_Projekt.BOL.Managers;
using MG_Projekt.BOL.Managers.FactoryManager;
using MG_Projekt.Infrastructure.Factories;
using System.ComponentModel;
using System.Windows;

namespace MG_Projekt
{
    /// <summary>
    /// Interaction logic for AlgorithmWindow.xaml
    /// </summary>
    public partial class AlgorithmWindow : Window
    {
        private ParametersManager _paremetersManager;
        private AlgorithmManager _algorithmManager;

        public AlgorithmWindow(ParametersManager paramentersManager)
        {
            InitializeComponent();

            this._paremetersManager = paramentersManager;
            this._algorithmManager = ManagerFactory.GetManager<AlgorithmManager>();
        }

        public void Calculate()
        {
            _algorithmManager.ParametersManager = _paremetersManager;
            _algorithmManager.CalculatePossibleSolution();
        }

        public void DisplayResults()
        {
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
using MG_Projekt.BOL.Managers;
using MG_Projekt.Infrastructure.Factories;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MG_Projekt
{
    public delegate bool CheckIfUserCanGo();

    public partial class MainWindow : Window
    {
        private ControlsType _currentControl;
        public CheckIfUserCanGo CheckingSectionMethod;

        private const int SwitchModule = 1;

        public ControlsType CurrentControl
        {
            get { return _currentControl; }
            set { _currentControl = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            InitalizeView();
            CheckSection();
        }

        private void InitalizeView()
        {
            IntroductionControl control = ControlFactory.GetControl<IntroductionControl>();

            this.DynamicControl.Content = control;
            this.SectionLabel.Content = ControlFactory.GetSection(ControlsType.Introduction);
            this._currentControl = ControlsType.Introduction;
        }

        public void CheckSection()
        {
            this.PervButton.IsEnabled = _currentControl != ControlsType.Introduction;
            this.NextButton.IsEnabled = _currentControl != ControlsType.Alghoritm;
        }

        private void SwitchView(ToSection toSection)
        {
            if (toSection == ToSection.Next)
                _currentControl = (ControlsType)(_currentControl + SwitchModule);
            else
                _currentControl = (ControlsType)(_currentControl - SwitchModule);

            if (_currentControl == ControlsType.Alghoritm)
            {
                BackgroundWorker worker = new BackgroundWorker();
                ParametersControl paramentersControl = (ParametersControl)this.DynamicControl.Content;
                ParametersManager paramentersManager = paramentersControl.ParametersManager;
                AlgorithmWindow algorithmWindow = new AlgorithmWindow(paramentersManager);

                worker.DoWork += (o, ea) =>
                {
                    algorithmWindow.Calculate();
                };

                worker.RunWorkerCompleted += (o, ea) =>
                {
                    this.BusyIndicator.IsBusy = false;
                    algorithmWindow.Show();
                };

                this.BusyIndicator.IsBusy = true;
                worker.RunWorkerAsync();
            }
            else if (_currentControl == ControlsType.Parameters)
            {
                this.DynamicControl.Content = ControlFactory.GetControlByEnum(_currentControl);
                this.SectionLabel.Content = ControlFactory.GetSection(_currentControl);
                this.NextButton.Content = "Algorytm";
            }
            else
            {
                this.DynamicControl.Content = ControlFactory.GetControlByEnum(_currentControl);
                this.SectionLabel.Content = ControlFactory.GetSection(_currentControl);
                this.NextButton.Content = "Następny";
            }

            CheckSection();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckingSectionMethod())
                SwitchView(ToSection.Next);
        }

        private void PervButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchView(ToSection.Perv);
        }
    }

    public enum ToSection
    {
        Next,
        Perv
    }
}
using MG_Projekt.Infrastructure.Factories;
using System.Windows;

namespace MG_Projekt
{
    public delegate bool CheckIfUserCanGo();

    public partial class MainWindow : Window
    {
        private ControlsType _currentControl;
        public CheckIfUserCanGo CheckingSectionMethod;

        private const int SwitchModule = 1;

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

        private void CheckSection()
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

            this.DynamicControl.Content = ControlFactory.GetControlByEnum(_currentControl);
            this.SectionLabel.Content = ControlFactory.GetSection(_currentControl);
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
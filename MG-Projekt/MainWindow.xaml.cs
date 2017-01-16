using MG_Projekt.Infrastructure.Factories;
using System.Windows;

namespace MG_Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ControlsType _currentControl;

        public MainWindow()
        {
            InitializeComponent();
            InitalizeView();
            CheckSection();
        }

        private void InitalizeView()
        {
            this.DynamicControl.Content = ControlFactory.GetControl<IntroductionControl>();
            this.SectionLabel.Content = ControlFactory.GetSection(ControlsType.Introduction);
            this._currentControl = ControlsType.Introduction;
        }

        private void CheckSection()
        {
            this.PervButton.IsEnabled = _currentControl != ControlsType.Introduction;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PervButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

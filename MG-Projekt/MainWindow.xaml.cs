using MG_Projekt.Infrastructure.Factories;
using System.Windows;

namespace MG_Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitalizeView();
        }

        private void InitalizeView()
        {
            this.DynamicControl = ControlFactory.GetControl<IntroductionControl>();
            this.SectionLabel.Content = ControlFactory.GetSection(ControlsType.Introduction);
        }
    }
}

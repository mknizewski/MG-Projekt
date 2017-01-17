using MG_Projekt.BOL.Interfaces;
using MG_Projekt.BOL.Models;
using MG_Projekt.Infrastructure.Factories;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MG_Projekt
{
    /// <summary>
    /// Interaction logic for ParametersControl.xaml
    /// </summary>
    public partial class ParametersControl : UserControl, IControl
    {
        private List<DeliveryCords> _deliveryCordsList;
        public IList<DeliveryCords> DeliveryCords
        {
            get
            { return _deliveryCordsList; }
        }

        public ParametersControl()
        {
            InitializeComponent();
            InitializeView();
            LinkToMethod();
        }

        private void InitializeView()
        {
            _deliveryCordsList = new List<DeliveryCords>();
            this.CordListBox.ItemsSource = DeliveryCords;
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

        private void AddCord_Click(object sender, RoutedEventArgs e)
        {
            int x = int.Parse(XCords.Text);
            int y = int.Parse(YCords.Text);

            DeliveryCords cords = new DeliveryCords(x, y);
            _deliveryCordsList.Add(cords);

            this.CordListBox.Items.Add(cords);     
        }

        private void DeleteCord_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
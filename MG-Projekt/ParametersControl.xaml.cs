using MG_Projekt.BOL.Interfaces;
using MG_Projekt.BOL.Managers;
using MG_Projekt.BOL.Managers.FactoryManager;
using MG_Projekt.BOL.Models;
using MG_Projekt.BOL.Resources.Messages;
using MG_Projekt.Infrastructure;
using MG_Projekt.Infrastructure.Factories;
using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MG_Projekt
{
    /// <summary>
    /// Interaction logic for ParametersControl.xaml
    /// </summary>
    public partial class ParametersControl : UserControl, IControl
    {
        private const int NoSelected = -1;

        private ParametersManager _parametersManager;

        public ParametersControl()
        {
            InitializeComponent();
            InitializeView();
            LinkToMethod();
        }

        private void InitializeView()
        {
            _parametersManager = ManagerFactory.GetManager<ParametersManager>();
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
            try
            {
                int x = int.Parse(XCords.Text);
                int y = int.Parse(YCords.Text);

                this.AddCoords(x, y);

                this.XCords.Text = string.Empty;
                this.YCords.Text = string.Empty;
            }
            catch (FormatException)
            {
                MessageBox.Show(
                    MessageDictionary.IncorrectCords,
                    MessageDictionary.ErrorDialogCapiton,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void AddCoords(int x, int y)
        {
            Coordinate cords = Coordinate.GetInstance(x, y);
            _parametersManager.DeliveryCoordinates.Add(cords);
            
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = cords.ToString();

            this.CordListBox.Items.Add(listBoxItem);
        }

        private void DeleteCord_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = this.CordListBox.SelectedIndex;

            if (selectedIndex != NoSelected)
            {
                this.CordListBox.Items.RemoveAt(selectedIndex);
                _parametersManager.DeliveryCoordinates.RemoveAt(selectedIndex);
            }
        }

        private void FromFileCord_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = MessageDictionary.OpenFileFilter;

            if (fileDialog.ShowDialog() == true)
            {
                StreamReader streamReader = new StreamReader(fileDialog.FileName);
                string line = string.Empty;

                try
                {
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] split = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int x = int.Parse(split[0]);
                        int y = int.Parse(split[1]);

                        this.AddCoords(x, y);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        MessageDictionary.IncorrectFile,
                        MessageDictionary.ErrorDialogCapiton,
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                streamReader.Close();
                streamReader.Dispose();
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SendDataToManager();
                _parametersManager.CalculateCosts();
                ShowCostsDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    MessageDictionary.ErrorDialogCapiton,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void ShowCostsDataGrid()
        {
            // Visibility
            this.CostDataGrid.Visibility = Visibility.Visible;

            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();

            // Columns Bulider
            _parametersManager.CostsList.ForEach(x =>
            {
                dt.Columns.Add(x.DeliveryCoordinate.ToString());
                dr[x.DeliveryCoordinate.ToString()] = x.Cost;
            });

            dt.Rows.Add(dr);
            this.CostDataGrid.AutoGenerateColumns = true;
            this.CostDataGrid.CanUserAddRows = false;
            this.CostDataGrid.CanUserSortColumns = false;
            this.CostDataGrid.DataContext = dt;
        }

        private void SendDataToManager()
        {
            this._parametersManager.DeliveryCount = this.DeliveryCountTextBox.Text.ToInt(this.DeliveryCountLabel);
            this._parametersManager.PetrolCost = this.PetrolPriceTextBox.Text.ToDouble(this.PetrolPriceLabel);
            this._parametersManager.PetrolUsage = this.PetrolUsageTextBox.Text.ToDouble(this.PetrolUsageLabel);
            this._parametersManager.Trucks = this.TrucksTextBox.Text.ToInt(this.TrucksLabel);

            this._parametersManager.Temparature = this.TemperatureTextBox.Text.ToDouble(this.TemperatureLabel);
            this._parametersManager.Delta = this.DeltaTextBox.Text.ToDouble(this.DeltaLabel);
            this._parametersManager.IterationCount = this.IterationTextBox.Text.ToInt(this.IterationLabel);

            // TODO: Obsługa wyjątku
            int x = int.Parse(this.SenderPointXTextBox.Text);
            int y = int.Parse(this.SenderPointYTextBox.Text);
            this._parametersManager.SenderCoordiante = Coordinate.GetInstance(x, y);
        }
    }
}
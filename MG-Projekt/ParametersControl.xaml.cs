using MG_Projekt.BOL.FactoryManager;
using MG_Projekt.BOL.Interfaces;
using MG_Projekt.BOL.Managers;
using MG_Projekt.BOL.Models;
using MG_Projekt.BOL.Resources.Messages;
using MG_Projekt.Infrastructure;
using MG_Projekt.Infrastructure.Factories;
using Microsoft.Win32;
using System;
using System.Data;
using System.IO;
using System.Linq;
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
        private bool _isCalculated;

        public ParametersManager ParametersManager
        {
            get { return _parametersManager; }
            set { _parametersManager = value; }
        }

        public ParametersControl()
        {
            InitializeComponent();
            InitializeView();
            LinkToMethod();
        }

        private void InitializeView()
        {
            _parametersManager = ManagerFactory.GetManager<ParametersManager>();
            _isCalculated = false;
        }

        public bool CheckPermission()
        {
            if (!_isCalculated)
                MessageBox.Show(
                    MessageDictionary.IsNotCalculated,
                    MessageDictionary.ErrorDialogCapiton,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

            return _isCalculated;
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
                int request = int.Parse(RequestTextBox.Text);

                this.AddDeliveryCoords(x, y, request);

                this.XCords.Text = string.Empty;
                this.YCords.Text = string.Empty;
                this.RequestTextBox.Text = string.Empty;
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

        private void AddDeliveryCoords(int x, int y, int request)
        {
            DeliveryCoordinate deliveryCoordinate = new DeliveryCoordinate(x, y, request);
            _parametersManager.DeliveryCoordinates.Add(deliveryCoordinate);

            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = deliveryCoordinate.ToString();

            this.DeliveryCordListBox.Items.Add(listBoxItem);
        }

        private void AddSenderCoords(int x, int y, int limit)
        {
            SenderCooridante senderCoordinate = new SenderCooridante(x, y, limit);
            _parametersManager.SenderCoordiantes.Add(senderCoordinate);

            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = senderCoordinate.ToString();

            this.SenderCordListBox.Items.Add(listBoxItem);
        }

        private void DeleteCord_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = this.DeliveryCordListBox.SelectedIndex;

            if (selectedIndex != NoSelected)
            {
                this.DeliveryCordListBox.Items.RemoveAt(selectedIndex);
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
                        int request = int.Parse(split[2]);

                        this.AddDeliveryCoords(x, y, request);
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
                ShowLimits();

                this.TargetFunctionTextBlock.Text = _parametersManager.DisplayFunction();
                this._isCalculated = _parametersManager.CheckDemondAndSupply();
                this.DemandSupplyAlert.Visibility = _isCalculated ? Visibility.Hidden : Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    MessageDictionary.ErrorDialogCapiton,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                this._isCalculated = false;
            }
        }

        private void ShowLimits()
        {
            this.DeliversLimitTextBox.Inlines.Clear();
            this.SendersLimitTextBox.Inlines.Clear();

            string[] deliveryLimits = _parametersManager.DisplayDeliversLimit();
            string[] senderLimits = _parametersManager.DisplaySendersLimit();

            for (int i = 0; i < deliveryLimits.Length; i++)
                this.DeliversLimitTextBox.Inlines.Add(deliveryLimits[i] + Environment.NewLine);

            for (int i = 0; i < senderLimits.Length; i++)
                this.SendersLimitTextBox.Inlines.Add(senderLimits[i] + Environment.NewLine);
        }

        private void ShowCostsDataGrid()
        {
            this.CostDataGrid.Visibility = Visibility.Visible;

            int senderCount = _parametersManager.CostsList.GetLength(0);
            int deliveryCount = _parametersManager.CostsList.GetLength(1);
            DataTable dt = new DataTable();
            dt.Columns.Add(@"Nadawcy \ Odbiorcy");

            for (int i = 0; i < senderCount; i++)
            {
                DataRow dataRow = dt.NewRow();
                for (int j = 0; j < deliveryCount; j++)
                {
                    if (!dt.Columns.Contains("O" + j.ToString()))
                        dt.Columns.Add("O" + j.ToString());

                    Costs cost = _parametersManager.CostsList[i, j];
                    dataRow["O" + j.ToString()] = cost.Cost;
                    dataRow[@"Nadawcy \ Odbiorcy"] = "N" + i.ToString();
                }

                if (!dt.Columns.Contains("Ai"))
                    dt.Columns.Add("Ai");

                dataRow["Ai"] = _parametersManager.SenderCoordiantes[i].Limit;
                dt.Rows.Add(dataRow);
            }

            DataRow biRow = dt.NewRow();
            biRow[@"Nadawcy \ Odbiorcy"] = "Bi";
            for (int i = 0; i < deliveryCount; i++)
                biRow["O" + i.ToString()] = _parametersManager.DeliveryCoordinates[i].Request;

            int demand = _parametersManager.DeliveryCoordinates.Sum(x => x.Request);
            int supply = _parametersManager.SenderCoordiantes.Sum(x => x.Limit);

            biRow["Ai"] = $@"{demand} \ {supply}";

            dt.Rows.Add(biRow);
            this.CostDataGrid.DataContext = dt;
        }

        private void SendDataToManager()
        {
            this._parametersManager.PetrolCost = this.PetrolPriceTextBox.Text.ToDouble(this.PetrolPriceLabel);
            this._parametersManager.PetrolUsage = this.PetrolUsageTextBox.Text.ToDouble(this.PetrolUsageLabel);

            this._parametersManager.Temparature = this.TemperatureTextBox.Text.ToDouble(this.TemperatureLabel);
            this._parametersManager.Delta = this.DeltaTextBox.Text.ToDouble(this.DeltaLabel);
            this._parametersManager.IterationCount = this.IterationTextBox.Text.ToInt(this.IterationLabel);
        }

        private void AddSenderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int x = int.Parse(SenderPointXTextBox.Text);
                int y = int.Parse(SenderPointYTextBox.Text);
                int limit = int.Parse(LimitTextBox.Text);

                this.AddSenderCoords(x, y, limit);

                this.SenderPointXTextBox.Text = string.Empty;
                this.SenderPointYTextBox.Text = string.Empty;
                this.LimitTextBox.Text = string.Empty;
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

        private void DeleteSenderButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = this.SenderCordListBox.SelectedIndex;

            if (selectedIndex != NoSelected)
            {
                this._parametersManager.SenderCoordiantes.RemoveAt(selectedIndex);
                this.SenderCordListBox.Items.RemoveAt(selectedIndex);
            }
        }

        private void SenderFromFileButton_Click(object sender, RoutedEventArgs e)
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
                        int limit = int.Parse(split[2]);

                        this.AddSenderCoords(x, y, limit);
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
    }
}
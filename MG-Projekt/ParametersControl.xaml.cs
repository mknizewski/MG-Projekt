using MG_Projekt.BOL.Interfaces;
using MG_Projekt.BOL.Models;
using MG_Projekt.BOL.Resources.Messages;
using MG_Projekt.Infrastructure.Factories;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
            DeliveryCords cords = new DeliveryCords(x, y);
            _deliveryCordsList.Add(cords);

            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.Content = cords.DispCords;

            this.CordListBox.Items.Add(listBoxItem);
        }

        private void DeleteCord_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = this.CordListBox.SelectedIndex;

            if (selectedIndex != NoSelected)
            {
                this.CordListBox.Items.RemoveAt(selectedIndex);
                _deliveryCordsList.RemoveAt(selectedIndex);
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
    }
}
using MG_Projekt.BOL.Interfaces;
using MG_Projekt.BOL.Models;
using MG_Projekt.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace MG_Projekt
{
    /// <summary>
    /// Interaction logic for ProblemControl.xaml
    /// </summary>
    public partial class ProblemControl : UserControl, IControl
    {
        private const string NameFile = @"Content\Genesis.txt";

        public ProblemControl()
        {
            InitializeComponent();
            LinkToMethod();
            LoadFile();
        }

        private void LoadFile()
        {
            this.GenesisOfProblemTextBlock.Text = File.ReadAllText(string.Concat(
                AppDomain.CurrentDomain.BaseDirectory,
                NameFile));
        }

        public bool CheckPermission()
        {
            return true;
        }

        public void LinkToMethod()
        {
            MainWindow mainWindow = ControlFactory.GetMainWindowInstance();
            mainWindow.CheckingSectionMethod = CheckPermission;
        }
    }
}
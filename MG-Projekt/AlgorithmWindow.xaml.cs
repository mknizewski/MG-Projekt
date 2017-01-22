using MG_Projekt.BOL.Managers;
using MG_Projekt.BOL.Managers.FactoryManager;
using MG_Projekt.BOL.Models;
using MG_Projekt.Infrastructure.Factories;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Series;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
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
            Thread.Sleep(3000);
            _algorithmManager.ParametersManager = _paremetersManager;
            _algorithmManager.CalculatePossibleSolution();
        }

        public void DisplayResults()
        {
            DrawMap();
            BulidGrid();
        }

        private void DrawMap()
        {
            Solution bestSolution = _algorithmManager.BestSolution;
            int senders = bestSolution.X.GetLength(0);
            int delivers = bestSolution.X.GetLength(1);

            PlotViewModel plotViewModel = new PlotViewModel();

            for (int i = 0; i < senders; i++)
            {
                for (int j = 0; j < delivers; j++)
                {
                    if (bestSolution.X[i, j] != 0)
                    {
                        SenderCooridante sender = _paremetersManager.SenderCoordiantes[i];
                        DeliveryCoordinate delivery = _paremetersManager.DeliveryCoordinates[j];

                        LineSeries series = new LineSeries();
                        DataPoint dataPoint = new DataPoint(sender.X, sender.Y);
                        DataPoint dataPoint2 = new DataPoint(delivery.X, delivery.Y);
                        List<DataPoint> dataPointList = new List<DataPoint>();
                        dataPointList.Add(dataPoint);
                        dataPointList.Add(dataPoint2);

                        PointAnnotation senderAnnotation = new PointAnnotation();
                        senderAnnotation.Text = $"N{i}";
                        senderAnnotation.X = sender.X;
                        senderAnnotation.Y = sender.Y;

                        PointAnnotation deliveryAnnotation = new PointAnnotation();
                        deliveryAnnotation.Text = $"O{j}";
                        deliveryAnnotation.X = delivery.X;
                        deliveryAnnotation.Y = delivery.Y;

                        series.Title = $"N{i} do O{j} T = {bestSolution.X[i, j]}";
                        series.ItemsSource = dataPointList;
                        plotViewModel.MyModel.Series.Add(series);
                        plotViewModel.MyModel.Annotations.Add(senderAnnotation);
                        plotViewModel.MyModel.Annotations.Add(deliveryAnnotation);
                    }
                }
            }

            this.Plot.DataContext = plotViewModel;
        }

        private void BulidGrid()
        {
            Solution bestSolution = _algorithmManager.BestSolution;
            DataTable dataTable = new DataTable();
            int senders = bestSolution.X.GetLength(0);
            int delivers = bestSolution.X.GetLength(1);

            dataTable.Columns.Add(@"Nadawcy \ Odbiorcy");

            for (int i = 0; i < senders; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int j = 0; j < delivers; j++)
                {
                    if (!dataTable.Columns.Contains("O" + j.ToString()))
                        dataTable.Columns.Add("O" + j.ToString());

                    dataRow["O" + j.ToString()] = bestSolution.X[i, j];
                    dataRow[@"Nadawcy \ Odbiorcy"] = "N" + i.ToString();
                }

                dataTable.Rows.Add(dataRow);
            }

            this.CostLabel.Content = string.Format(this.CostLabel.Content.ToString(), bestSolution.TargetFunction());
            this.DriverKilometersLabel.Content = string.Format(this.DriverKilometersLabel.Content.ToString(), _algorithmManager.GetTotalKilometers());
            this.SolutionDataGrid.DataContext = dataTable;
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
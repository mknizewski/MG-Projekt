using MG_Projekt.BOL.Managers;
using MG_Projekt.BOL.Models;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MG_Projekt
{
    /// <summary>
    /// Interaction logic for SolutionsWindow.xaml
    /// </summary>
    public partial class SolutionsWindow : Window
    {
        private AlgorithmManager _algorithmManager;

        private const string CostPattern = "Koszt całkowity: {0} zł";
        private const string DriverKilometersPattern = "Przejechane: {0} km";
        private const string SolutionLabelPattern = "Rozwiązanie: {0}";

        public SolutionsWindow(AlgorithmManager algorithmManager)
        {
            InitializeComponent();
            InitializeView(algorithmManager);
            DisplayAllSolutions();
        }

        private void InitializeView(AlgorithmManager algorithmManager)
        {
            this._algorithmManager = algorithmManager;
        }

        private void DisplayAllSolutions()
        {
            for (int i = 0; i < _algorithmManager.Solutions.Count; i++)
            {
                Solution solution = _algorithmManager.Solutions[i];
                Binding binding = new Binding();
                Label headerLabel = new Label { Content = string.Format(SolutionLabelPattern, i + 1), FontSize = 16 };
                DataGrid dataGrid = new DataGrid { CanUserAddRows = false, CanUserDeleteRows = false, CanUserSortColumns = false };
                Label costLabel = new Label { Content = string.Format(CostPattern, _algorithmManager.GetTotalCostBySolution(solution)), FontSize = 14 };
                Label drivernLabel = new Label { Content = string.Format(DriverKilometersPattern, _algorithmManager.GetTotalKilometersBySolution(solution)), FontSize = 14 };

                dataGrid.SetBinding(DataGrid.ItemsSourceProperty, binding);
                dataGrid.DataContext = BuildGrid(solution);

                this.Panel.Children.Add(headerLabel);
                this.Panel.Children.Add(dataGrid);
                this.Panel.Children.Add(costLabel);
                this.Panel.Children.Add(drivernLabel);
            }
        }

        private DataTable BuildGrid(Solution solution)
        {
            DataTable dataTable = new DataTable();
            int senders = solution.X.GetLength(0);
            int delivers = solution.X.GetLength(1);

            dataTable.Columns.Add(@"Nadawcy \ Odbiorcy");

            for (int i = 0; i < senders; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                for (int j = 0; j < delivers; j++)
                {
                    if (!dataTable.Columns.Contains("O" + j.ToString()))
                        dataTable.Columns.Add("O" + j.ToString());

                    dataRow["O" + j.ToString()] = solution.X[i, j];
                    dataRow[@"Nadawcy \ Odbiorcy"] = "N" + i.ToString();
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}
using OxyPlot;

namespace MG_Projekt
{
    public class PlotViewModel
    {
        public PlotViewModel()
        {
            this.MyModel = new PlotModel { Title = "Mapa dostawy" };
        }

        public PlotModel MyModel
        {
            get;
            private set;
        }
    }
}

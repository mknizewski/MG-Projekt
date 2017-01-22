using MG_Projekt.BOL.Interfaces;
using MG_Projekt.BOL.Resources.Section;
using System.Windows.Controls;

namespace MG_Projekt.Infrastructure.Factories
{
    /// <summary>
    /// Fabryka kontrolek
    /// </summary>
    public static class ControlFactory
    {
        public static T GetControl<T>() where T : IControl, new()
        {
            return new T();
        }

        public static Control GetControlByEnum(ControlsType controlsType)
        {
            switch (controlsType)
            {
                case ControlsType.Introduction:
                    return GetControl<IntroductionControl>();

                case ControlsType.Problem:
                    return GetControl<ProblemControl>();

                case ControlsType.Parameters:
                    return GetControl<ParametersControl>();

                default:
                    return null;
            }
        }

        public static MainWindow GetMainWindowInstance()
        {
            return (MainWindow)App.Current.MainWindow;
        }

        public static string GetSection(ControlsType controlType)
        {
            switch (controlType)
            {
                case ControlsType.Introduction:
                    return SectionDictionary.Introduction;

                case ControlsType.Problem:
                    return SectionDictionary.Problem;

                case ControlsType.Parameters:
                    return SectionDictionary.Parameters;

                case ControlsType.Alghoritm:
                    return SectionDictionary.Algorithm;

                default:
                    return string.Empty;
            }
        }
    }

    public enum ControlsType
    {
        Introduction,
        Problem,
        Parameters,
        Alghoritm
    }
}
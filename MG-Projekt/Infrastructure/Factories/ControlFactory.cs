using MG_Projekt.BOL.Resources.Section;
using System.Windows.Controls;

namespace MG_Projekt.Infrastructure.Factories
{
    /// <summary>
    /// Fabryka kontrolek
    /// </summary>
    public static class ControlFactory
    {
        public static T GetControl<T>() where T : Control, new()
        {
            return new T();
        }

        public static string GetSection(ControlsType controlType)
        {
            switch (controlType)
            {
                case ControlsType.Introduction:
                    return SectionDictionary.Introduction;
                default:
                    return string.Empty;
            }
        }
    }

    public enum ControlsType
    {
        Introduction
    }
}

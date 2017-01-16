using MG_Projekt.BOL.Resources.Factory;
using System.Resources;

namespace MG_Projekt.BOL.Resources.Section
{
    public static class SectionDictionary
    {
        private static ResourceManager _sectionResource = ResourceFactory.GetResourceManager<SectionResource>();

        public static string Introduction
        {
            get
            {
                return _sectionResource.GetString(nameof(Introduction));
            }
        }

        public static string Problem
        {
            get
            {
                return _sectionResource.GetString(nameof(Problem));
            }
        }

        public static string Parameters
        {
            get
            {
                return _sectionResource.GetString(nameof(Parameters));
            }
        }

        public static string Algorithm
        {
            get
            {
                return _sectionResource.GetString(nameof(Algorithm));
            }
        }
    }
}

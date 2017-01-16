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
    }
}

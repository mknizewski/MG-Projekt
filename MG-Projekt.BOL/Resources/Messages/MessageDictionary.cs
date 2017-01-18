using MG_Projekt.BOL.Resources.Factory;
using System.Resources;

namespace MG_Projekt.BOL.Resources.Messages
{
    public static class MessageDictionary
    {
        private static ResourceManager _resourceManager = ResourceFactory.GetResourceManager<MessageResource>();

        public static string IncorrectFile = _resourceManager.GetString(nameof(IncorrectFile));
        public static string IncorrectCords = _resourceManager.GetString(nameof(IncorrectCords));
        public static string OpenFileFilter = _resourceManager.GetString(nameof(OpenFileFilter));
        public static string ErrorDialogCapiton = _resourceManager.GetString(nameof(ErrorDialogCapiton));

        public static string PersonalizedExceptionMessage(string label)
        {
            return string.Format(_resourceManager.GetString(nameof(PersonalizedExceptionMessage)), label);
        }
    }
}
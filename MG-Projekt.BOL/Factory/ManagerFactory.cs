namespace MG_Projekt.BOL.FactoryManager
{
    public static class ManagerFactory
    {
        public static T GetManager<T>() where T : new()
        {
            return new T();
        }
    }
}
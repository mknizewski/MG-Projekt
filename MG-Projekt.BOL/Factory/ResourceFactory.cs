﻿using System.Resources;

namespace MG_Projekt.BOL.Factory
{
    public static class ResourceFactory
    {
        public static ResourceManager GetResourceManager<T>()
        {
            return new ResourceManager(typeof(T));
        }
    }
}
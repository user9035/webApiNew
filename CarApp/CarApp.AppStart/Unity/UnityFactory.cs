using System;
using Microsoft.Practices.Unity;

namespace CarApp.AppStart.Unity
{
    public static class UnityContainerFactory
    {
        public static IUnityContainer Create()
        {
            var container = new UnityContainer();
            UnityConfigurationManager.RegisterComponents(container);
            return container;
        }
    }
}

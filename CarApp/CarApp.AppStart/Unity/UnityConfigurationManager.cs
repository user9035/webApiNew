using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Repository;
using Domain.Entity;
using Domain.Interface;
using Microsoft.Practices.Unity;

namespace CarApp.AppStart.Unity
{
    internal static class UnityConfigurationManager
    {
        internal static void RegisterComponents(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException();

            container.RegisterType<IFeedRepository, FeedRepository>();
        }
    }
}
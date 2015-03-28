using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarShare.Domain.Abstract;
using CarShare.Domain.Concrete;
using System.Web.Mvc;
using Ninject;
using System.Web.Routing;
using System.Web.Security;

namespace CarShare.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
            //ninjectKernel.Inject(Membership.Provider);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IUserRepository>().To<SQLUserRepository>();

            //ninjectKernel.Bind<IUserRepository>().To<EFUserRepository>();
            ninjectKernel.Inject(Membership.Provider);
        }

        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
    }
}
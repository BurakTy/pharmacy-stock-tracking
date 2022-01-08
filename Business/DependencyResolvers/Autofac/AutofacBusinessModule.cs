using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClamService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<StokDepoManager>().As<IStokDepoService>();
            builder.RegisterType<EfStokDepoDal>().As<IStokDepoDal>();

            builder.RegisterType<StokKartManager>().As<IStokKartService>();
            builder.RegisterType<EfStokKartDal>().As<IStokKartDal>();

            builder.RegisterType<StokCikisManager>().As<IStokCikisService>();
            builder.RegisterType<EfStokCikisDal>().As<IStokCikisDal>();

            builder.RegisterType<StokCikisDetayManager>().As<IStokCikisDetayService>();
            builder.RegisterType<EfStokCikisDetayDal>().As<IStokCikisDetayDal>();

            builder.RegisterType<StokEnvanterManager>().As<IStokEnvanterService>();
            builder.RegisterType<EfStokEnvanterDal>().As<IStokEnvanterDal>();

            builder.RegisterType<StokFaturaManager>().As<IStokFaturaService>();
            builder.RegisterType<EfStokFaturaDal>().As<IStokFaturaDal>();
            builder.RegisterType<EfStokFaturaDetayDal>().As<IStokFaturaDetayDal>();

            builder.RegisterType<StokBirimManager>().As<IStokBirimService>();
            builder.RegisterType<EfStokBirimDal>().As<IStokBirimDal>();

            builder.RegisterType<StokRafManager>().As<IStokRafService>();
            builder.RegisterType<EfStokRafDal>().As<IStokRafDal>();

            builder.RegisterType<PdfManager>().As<IPdfService>();


            builder.RegisterType<StokHastaManager>().As<IStokHastaService>();
            builder.RegisterType<EfStokHastaDal>().As<IStokHastaDal>();
            builder.RegisterType<EfStokIlacCikisDal>().As<IStokIlacCikisDal>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}

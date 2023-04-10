using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;

namespace StudentManage
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //���cookie����
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {

                });

            // ���session����
            services.AddSession(options =>
            {
                // ����1��Session����
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
            });
        }

        /// <summary>
        /// autoFacʹ����UseServiceProviderFactory
        /// ����д�����������Ȼ����
        /// ��ConfigureServices����֮�����
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //����κ�Autofacģ���ע�ᡣ
            //������ConfigureServices֮����õģ�����
            //�ڴ˴�ע�Ὣ������ConfigureServices��ע������ݡ�
            //�ڹ�������ʱ������á�UseServiceProviderFactory��new AutofacServiceProviderFactory��������`���򽫲�����ô˷���
            //var basePath = ApplicationEnvironment.ApplicationBasePath;
            try
            {
                //Service�Ǽ̳нӿڵ�ʵ�ַ����������
                var assemblys = Assembly.Load("Repository");
                //IDependency ��һ���ӿڣ�����Ҫʵ������ע��Ľ�ڶ�Ҫ�̳иýӿڣ�
                var baseType = typeof(IDependencyRepository);
                builder.RegisterAssemblyTypes(assemblys)
                    .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
                    .AsImplementedInterfaces().InstancePerLifetimeScope();
                // ������Ҫ����ע��
                //builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
                //builder.RegisterType<JwtAppService>().As<IJwtAppService>();
            }
            catch (Exception e)
            {
                Console.WriteLine("ע��ʧ�ܣ�" + e);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Login}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "areas", "StudentManage",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });
        }
    }
}

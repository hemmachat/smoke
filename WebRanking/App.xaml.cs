using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using WebRanking.Interfaces;
using WebRanking.ViewModels;

namespace WebRanking
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IViewMainWindowViewModel, MainWindow>();
            container.RegisterType<IViewMainWindowViewModel, MainWindowViewModel>();
            container.RegisterType<ISearchService, ManualSearchService>();
            container.Resolve<MainWindow>().Show();
        }
    }
}

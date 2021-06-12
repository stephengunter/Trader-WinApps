using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using ApplicationCore;
using ApplicationCore.Helpers;
using OrderMaker.Test;

namespace OrderMaker
{
    static class Program
    {
        static readonly NLog.ILogger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.ThreadException +=
            new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            // Add handler to handle the exception raised by additional threads
            AppDomain.CurrentDomain.UnhandledException +=
            new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var settings = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                                                .AppSettings.Settings;

            string mode = settings[AppSettingsKey.Mode].Value;

            if (mode.EqualTo("BasicTest")) Application.Run(new BasicTestForm());
            else if (mode.EqualTo("ApiTest")) Application.Run(new APITestForm());
            else if (mode.EqualTo("StrategyTest")) Application.Run(new StrategyTestForm());
            else Application.Run(new Main());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // All exceptions thrown by the main thread are handled over this method

            _logger.Error(e.Exception);

            throw e.Exception;
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // All exceptions thrown by additional threads are handled in this method
            var ex = e.ExceptionObject as Exception;
            _logger.Error(ex);

            //throw ex;

            // Suspend the current thread for now to stop the exception from throwing.
            //Thread.CurrentThread.Suspend();
        }

    }
}

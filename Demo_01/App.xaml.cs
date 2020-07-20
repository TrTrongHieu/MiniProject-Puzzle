using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Demo_01
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Nguoi dung truy cap lan cuoi
        /// </summary>
        public static string LastUser
        {
            get
            {
                return ConfigurationManager.AppSettings["last_name"];
            }
            set
            {
                Save("last_name", value);
            }
        }

        // <summary>
        // do rong thay doi lan cuoi
        // </summary>
        public static int LastWidth
        {
            get
            {
                return int.Parse(
                    ConfigurationManager.AppSettings["last_width"]);
            }
            set
            {
                Save("last_width", value.ToString());
            }
        }
        public static void Save(string key, string value)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}

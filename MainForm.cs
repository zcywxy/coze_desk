using CefSharp.WinForms;
using CefSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Coze
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            var cache = Path.GetFullPath("cefcache");
            if (!System.IO.Directory.Exists(cache))
            {
                System.IO.Directory.CreateDirectory(cache);
            }

            var settings = new CefSettings();
            settings.CachePath = cache;
            settings.IgnoreCertificateErrors = true;
            settings.PersistSessionCookies = true;
            settings.Locale = "zh-CN";
            settings.CefCommandLineArgs.Add("--ignore-urlfetcher-cert-requests", "1");
            settings.CefCommandLineArgs.Add("--ignore-certificate-errors", "1");
            settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:123.0) Gecko/20100101 Firefox/123.0";
            Cef.Initialize(settings);
            InitializeComponent();
            chromeBrowser.MenuHandler = new CustomContextMenuHandler();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chromeBrowser.LoadUrlAsync("https://www.coze.com/space/");
        }
    }
}

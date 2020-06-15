using SingleInstancing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace log4jDigger
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            SingleInstanceTracker tracker = null;
            try
            {
                tracker = new SingleInstanceTracker("LogViewUtil", new SingleInstanceEnforcerRetriever(GetSingleInstanceEnforcer), args);
                if (tracker.IsFirstInstance)
                    Application.Run((MainForm)tracker.Enforcer);
                else
                    tracker.SendMessageToFirstInstance(args);
            }
            catch (SingleInstancingException ex)
            {
                if (!WorkaroundForInitialInstance(args))
                    MessageBox.Show(String.Join(Environment.NewLine, args) + Environment.NewLine + ex.ToString());
            }
            finally
            {
                if (tracker != null)
                    tracker.Dispose();
            }
        }


        public static String WorkaroundForInitialInstancePath
        {
            get
            {
                return Path.Combine(Path.GetTempPath(), "WorkaroundForInitialInstancePath");
            }
        }

        /// <summary>
        /// TODO: wenn über Shell Extension initial mehrere Dateien geöffnet werden klappt das mit dem Mutex nicht 
        /// </summary>
        private static bool WorkaroundForInitialInstance(string[] args)
        {
            if (args.Length == 2 && args[0] == "W")
            {
                Boolean wait = true;
                while (wait)
                {
                    try
                    {
                        File.AppendAllText(WorkaroundForInitialInstancePath, args[1] + Environment.NewLine);
                        wait = false;
                    }
                    catch (Exception)
                    {

                    }
                }
                return true;
            }
            return false;
        }

        private static ISingleInstanceEnforcer GetSingleInstanceEnforcer(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            return new MainForm() { Args = args };
        }
    }
}

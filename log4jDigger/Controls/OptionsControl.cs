using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace log4jDigger.Controls
{
    public partial class OptionsControl : UserControl
    {
        public OptionsControl()
        {
            InitializeComponent();
            buttonRegister.Text = FileShellExtension.IsRegistered() ? "Unregister ShellExtensions" : "Register ShellExtensions";
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileShellExtension.IsRegistered())
                    FileShellExtension.UnRegister();
                else
                    FileShellExtension.Register();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Please run Application as local Admin");
            }

            buttonRegister.Text = FileShellExtension.IsRegistered() ? "Unregister ShellExtensions" : "Register ShellExtensions";
        }

    }
}

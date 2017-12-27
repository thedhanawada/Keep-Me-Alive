using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

        }
        protected void Displaynotify()
        {
            try
            {
                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(@"favicon.ico"));
                notifyIcon1.Text = "Keeping your Display Alive";
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = "Keeping Your Display Alive";
                notifyIcon1.BalloonTipText = "The Application is now active.";
                notifyIcon1.ShowBalloonTip(100);
            }
            catch (Exception ex1)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ForceSystemAwake();
        }
        public static void ForceSystemAwake()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.EXECUTION_STATE.ES_CONTINUOUS |
                                                  NativeMethods.EXECUTION_STATE.ES_DISPLAY_REQUIRED |
                                                  NativeMethods.EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                                                  NativeMethods.EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
        }

        public static void ResetSystemDefault()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.EXECUTION_STATE.ES_CONTINUOUS);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetSystemDefault();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Displaynotify();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(Control.MousePosition);

        }

        private void keepMeAliveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ForceSystemAwake();
        }

        private void setMeFreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetSystemDefault();
        }

        private void openApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 h = new Form1();
            h.Show();
        }

        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Application is currently inactive. Please run the .exe again to keep the application active.", "Keep me Alive");
            if (System.Windows.Forms.Application.MessageLoop)
            {
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                System.Environment.Exit(1);
            }

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The 'Keep me Alive' App will help you to keep your display switched on without any interuption for infinite team just with a mouseclick. Done with your meeting? Restore back to normal in a second.", "About - Keep me Alive");
        }
    }
    internal static partial class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001

            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }
    }

}

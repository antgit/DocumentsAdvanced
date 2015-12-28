using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace Documents2012
{
    public partial class SplashScreen1 : SplashScreen
    {
        public SplashScreen1()
        {
            InitializeComponent();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
            SplashScreenCommand command = (SplashScreenCommand)cmd;
            if (command == SplashScreenCommand.SetInfo)
            {
                //int pos = (int)arg;
                //marqueeProgressBarControl1. = pos;
                string info = (string)arg;
                labelControl2.Text = info;
            }

        }

        #endregion

        public enum SplashScreenCommand
        {
            SetInfo
        }
    }
}
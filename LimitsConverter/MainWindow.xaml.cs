using System;
using System.IO;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace LimitsConverter.Dialogs
{
    public partial class LimitsConverter : Window
    {
        public LimitsConverter()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Dat files (*.dat)|*.dat";
            openFileDialog.FilterIndex = 2;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    ProgramStatus lc = new ProgramStatus(openFileDialog.FileName);
                    version.Text = lc.Version.ToString();
                    serialNumber.Text = lc.SerialNumber;
                    time.Text = lc.Time.ToString();
                    counter.Text = lc.Counter.ToString();

                    StringBuilder sb = new StringBuilder();

                    foreach (var item in lc.ActiveProgramms)
                    {

                        sb.Append(item + " ");
                    }
                    activeProgramms.Text = sb.ToString();
                }
                catch(ArgumentException)
                {
                    version.Text = "Zła wersja";
                }

            }
        }

        private void btnCopyToClipboardClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(activeProgramms.Text);
        }
    }
}
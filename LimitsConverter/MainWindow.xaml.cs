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

                LogicForLimitsConverter lc = new LogicForLimitsConverter(openFileDialog.FileName);
                version.Text = lc.Version;
                serialNumber.Text = lc.SerialNumber;
                time.Text = lc.Time;
                counter.Text = lc.Counter;
                activeProgramms.Text = lc.ActiveProgramms;

                
            }
        }

        private void btnCopyToClipboardClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(activeProgramms.Text);
        }
    }
}
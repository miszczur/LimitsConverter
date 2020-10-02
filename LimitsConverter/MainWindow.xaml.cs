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

                LogicForLimitsConverter lm = new LogicForLimitsConverter(openFileDialog.FileName);
                version.Text = lm.Version;
                serialNumber.Text = lm.SerialNumber;
                time.Text = lm.Time;
                counter.Text = lm.Counter;
                activeProgramms.Text = lm.ActiveProgramms;

                
            }
        }
    }
}
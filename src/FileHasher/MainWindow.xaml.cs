using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;

using RLib.Extensions;

namespace FileHasher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HashTextBox.Text = string.Empty;
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "All files (*.*)|*.*" };

            bool? result = dialog.ShowDialog(this);
            if (result == true)
            {
                string fileName = dialog.FileName;
                FilePathTextBox.Text = fileName;
            }
        }

        private void CommandBinding_CanExecuteCutOrPaste(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Paste)
            {
                e.CanExecute = false;
                e.Handled = true;
            }
        }

        private void ComputeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FilePathTextBox.Text)) return;

            var file = new FileInfo(FilePathTextBox.Text);
            string hash;
            if (MD5RadioButton.IsChecked ?? false)
            {
                hash = file.ComputeMD5Hash();
            }
            else
            {
                hash = file.ComputeNodeId();
            }
            HashTextBox.Text = hash;
        }
    }
}

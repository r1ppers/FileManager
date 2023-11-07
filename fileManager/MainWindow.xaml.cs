using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using WinForms = System.Windows.Forms;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.IO;

namespace fileManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            chooseDirButton.Click += (s, e) => { GetDirection(); };
            sendDataButton.Click += (s, e) => { Manage(); };
        }

        void GetDirection()
        {
            WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();
            WinForms.DialogResult result = dialog.ShowDialog();
            if(result == WinForms.DialogResult.OK)
            {
                directionTextBox.Text = dialog.SelectedPath;
            }
        }

        void Manage()
        {
            string path = directionTextBox.Text;
            var arr = Directory.GetFiles(path);
            string add = " Folder"; // addition for folder name
            string specChar = @"\";
            foreach (var file in arr)
            {
                var folderName = Path.GetFileName(file);
                folderName = Path.GetExtension(folderName) + add; // getting folder name
                if (!Directory.Exists(path + specChar + folderName))
                {
                    Directory.CreateDirectory(path + specChar + folderName);
                }
                var newPath = path + specChar + folderName;
                File.Move(file, newPath + specChar + Path.GetFileName(file));
            }
        }
    }
}

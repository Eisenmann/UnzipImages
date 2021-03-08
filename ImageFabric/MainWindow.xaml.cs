using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Forms;

namespace ImageFabric
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

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            UnzipImage unz = new UnzipImage();
            MoveFiles mf = new MoveFiles();
              
             //create temp dir
            mf.CreateFolderTemp();
            string tmpOutFolder = txtBoxOutputFolder.Text;
            string inputFolder = txtBoxInputFolder.Text;
            string app7ZipPath = txtBox7ZipPath.Text;

            List<FileAndDir> filesList = new List<FileAndDir>();
            filesList = mf.CollectFiles(inputFolder, ".zip .jpg .png");

            foreach(FileAndDir fdr in filesList)
            {
            unz.Unzip(fdr.fileName, app7ZipPath, tmpOutFolder);
            }
           
            List<FileAndDir> filesList2 = new List<FileAndDir>();
            filesList2 = mf.CollectFiles("temp", ".zip .jpg .png .jpeg");

            foreach (FileAndDir fdr in filesList2)
            {
                mf.MoveAllFiles(tmpOutFolder, filesList2);
            }

             //delete temp dir
            mf.DeleteFolderTemp();
        }

        private void btnBrowseInputFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.ShowDialog();
            txtBoxInputFolder.Text = fbd.SelectedPath;
        }

        private void btnBrowseOutputFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.ShowDialog();
            txtBoxOutputFolder.Text = fbd.SelectedPath;
        }

        private void btnBrowse7ZipFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.ShowDialog();

            txtBox7ZipPath.Text = ofd.FileName;
        }

    }
}

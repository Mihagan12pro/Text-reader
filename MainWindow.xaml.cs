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
using System.IO;
using Microsoft.Win32;

using System.Speech;
namespace Text_reader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddTextFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openTextFile = new OpenFileDialog();

            openTextFile.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            openTextFile.ShowDialog();


            TextFilesPathTb.Text = openTextFile.FileName;   

        }

        private void AddTextFileTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextFilesPathTb.Text.EndsWith(".txt") && File.Exists(TextFilesPathTb.Text) )
            {
                AddTextForReadingTb.Text = File.ReadAllText(TextFilesPathTb.Text);
            }
        }

        private void AddTextForReadingTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AddTextForReadingTb.Text.Length>0)
            {
                PlayPauseBtn.IsEnabled = true;
                SaveInMp3Btn.IsEnabled = true;

                return;
            }
            PlayPauseBtn.IsEnabled = false;
            SaveInMp3Btn.IsEnabled = false;
        }
    }
}

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
using System.Speech.Synthesis;
namespace Text_reader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();
        bool isPaused = false;


        string currentTextForSpeack = "";
        public MainWindow()
        {
            InitializeComponent();

            

            
        }

        //private void Synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        //{
        //    PlayPauseResumeBtn.Content = "Play";
        //}

        //private void AddTextFileBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openTextFile = new OpenFileDialog();

        //    openTextFile.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

        //    openTextFile.ShowDialog();


        //    TextFilesPathTb.Text = openTextFile.FileName;   

        //}

        //private void AddTextFileTb_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (TextFilesPathTb.Text.EndsWith(".txt") && File.Exists(TextFilesPathTb.Text) )
        //    {
        //        AddTextForReadingTb.Text = File.ReadAllText(TextFilesPathTb.Text);
        //    }
        //}

        //private void AddTextForReadingTb_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (AddTextForReadingTb.Text.Length>0)
        //    {
        //        PlayPauseResumeBtn.IsEnabled = true;
        //        SaveInMp3Btn.IsEnabled = true;

        //        return;
        //    }
        //    PlayPauseResumeBtn.IsEnabled = false;
        //    SaveInMp3Btn.IsEnabled = false;
        //}

        //private void PlayPauseResumeBtn_Click(object sender, RoutedEventArgs e)
        //{
          
               




        //    if (isPaused)
        //    {
        //        synth.Resume();
        //        isPaused = false;
        //        PlayPauseResumeBtn.Content = "Pause";

                
        //    }
        //    else
        //    {
        //        if (synth.State == SynthesizerState.Speaking)
        //        {
        //            synth.Pause();
        //            isPaused = true;
        //            PlayPauseResumeBtn.Content = "Resume";
        //        }
        //        else
        //        {
        //            if (PlayPauseResumeBtn.Content == "Play")
        //            {
        //                currentTextForSpeack = AddTextForReadingTb.Text;

        //                var a = currentTextForSpeack;
        //            }

                  


        //            synth.SetOutputToDefaultAudioDevice();
        //            synth.SpeakAsync(AddTextForReadingTb.Text);
        //            PlayPauseResumeBtn.Content = "Pause";
        //        }
        //    }
        //}

        
       
    }
}

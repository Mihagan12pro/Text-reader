using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Speech;
using System.Speech.Synthesis;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using NAudio.Wave;
using NAudio.Lame;
using Microsoft.Win32;
namespace Text_reader
{
    internal class Speaker
    {
        private static Speaker speaker;

        private static SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        private string originText;

        private string originPath;

        private bool isPlay = true;

        public Speaker()
        {
            MainWindow.SaveInMp3PropBtn.IsEnabled = true;
            MainWindow.VolumeControllPropSlr.IsEnabled = true;



            MainWindow.AddTextForReadingPropTb.TextChanged -= MainWindow.AddTextForReadingTb_TextAdded;
            MainWindow.TextFilesPathPropTb.TextChanged -= MainWindow.TextFilesPathTb_TextChanged;
            MainWindow.PlayPauseResumePropBtn.Click -= MainWindow.PlayPauseResumeBtn_Click;

         
            MainWindow.PlayPauseResumePropBtn.Click += PausePlay_Click;

            MainWindow.VolumeControllPropSlr.ValueChanged += VolumeControllSlr_ValueChanged;

            string text = MainWindow.AddTextForReadingPropTb.Text;
            string filePath = "output.wav";

            originText = text;

            originPath = MainWindow.TextFilesPathPropTb.Text;

            MainWindow.SaveInMp3PropBtn.Click += SaveInMp3PropBtn_Click;

           

            AddAudioToAudioMiaEl();


            MainWindow.TextFilesPathPropTb.TextChanged += TextFilesPathTb_PathChanged;

            MainWindow.AudioPropMiaEl.Play();

            MainWindow.AudioPropMiaEl.MediaEnded += AudioMiaEl_MediaEnded;

            MainWindow.PlayPropSlr.ValueChanged += PlayPropSlr_ValueChanged;

            MainWindow.AddTextForReadingPropTb.TextChanged += AddTextForReadingTb_CheckChanges;

            speaker = this;


            MainWindow.AudioPropMiaEl.Volume = MainWindow.VolumeControllPropSlr.Maximum;

            MainWindow.VolumeValuePropTb.Text = Convert.ToString(MainWindow.AudioPropMiaEl.Volume) + "%";
        }


        private  void TextFilesPathTb_PathChanged(object sender, RoutedEventArgs e)
        {
            if (originPath != MainWindow.TextFilesPathPropTb.Text && File.Exists(MainWindow.TextFilesPathPropTb.Text))
            {
                isPlay = false;
                AddAudioToAudioMiaEl();
                MainWindow.PlayPauseResumePropBtn.Content = "Play";

                MainWindow.AddTextForReadingPropTb.Text = File.ReadAllText(MainWindow.TextFilesPathPropTb.Text);

                MainWindow.AudioPropMiaEl.Stop();


            }
        }

        private static void AddAudioToAudioMiaEl()
        {
           


            if(File.Exists("output.wav")){

                File.Delete("output.wav");

                MainWindow.AudioPropMiaEl.Source = null;

            }



            synthesizer.SetOutputToWaveFile("output.wav");
            synthesizer.Speak(MainWindow.AddTextForReadingPropTb.Text);
            synthesizer.SetOutputToDefaultAudioDevice();

            FileInfo file = new FileInfo("output.wav");

            MainWindow.PlayPropSlr.IsEnabled = true;

            MainWindow.AudioPropMiaEl.Source = new Uri(file.FullName);



            

        }

        private void AddTextForReadingTb_CheckChanges(object sender, RoutedEventArgs e)
        {
            if(MainWindow.AddTextForReadingPropTb.Text != originText)
            {
                MainWindow.PlayPauseResumePropBtn.Content = "Play";
                MainWindow.PlayPropSlr.Value = 0;
                MainWindow.AudioPropMiaEl.Stop();

                isPlay = false;

                if (MainWindow.AddTextForReadingPropTb.Text !="")
                {
                    


                    AddAudioToAudioMiaEl();

                    originText = MainWindow.AddTextForReadingPropTb.Text;

                    return;
                    
                }
                MainWindow.AddTextForReadingPropTb.TextChanged += MainWindow.AddTextForReadingTb_TextAdded;
                MainWindow.TextFilesPathPropTb.TextChanged += MainWindow.TextFilesPathTb_TextChanged;
                MainWindow.PlayPauseResumePropBtn.Click += MainWindow.PlayPauseResumeBtn_Click;


                MainWindow.PlayPauseResumePropBtn.IsEnabled = false;
                MainWindow.PlayPropSlr.IsEnabled = false;
                MainWindow.SaveInMp3PropBtn.IsEnabled = false;










                MainWindow.PlayPauseResumePropBtn.Click -= PausePlay_Click;

               

                MainWindow.SaveInMp3PropBtn.Click -= SaveInMp3PropBtn_Click;

                MainWindow.AudioPropMiaEl.Source = null;

                
                if (File.Exists("output.wav"))
                {
                    File.Delete("output.wav");
                }


              

                MainWindow.AudioPropMiaEl.MediaEnded -= AudioMiaEl_MediaEnded;

                MainWindow.PlayPropSlr.ValueChanged -= PlayPropSlr_ValueChanged;

                MainWindow.AddTextForReadingPropTb.TextChanged -= AddTextForReadingTb_CheckChanges;

                speaker = null;
            }
        }
       
        private void PausePlay_Click(object sender, RoutedEventArgs e)
        {
            if (isPlay)
            {
                isPlay = false;

                MainWindow.PlayPauseResumePropBtn.Content = "Resume";

                MainWindow.AudioPropMiaEl.Pause();

                return;
            }
            isPlay = true;

            MainWindow.PlayPauseResumePropBtn.Content = "Pause";

            MainWindow.AudioPropMiaEl.Play();
        }

        private  void PlayPropSlr_ValueChanged(object sender, RoutedEventArgs e)
        {
            MainWindow.AudioPropMiaEl.Position = TimeSpan.FromSeconds( MainWindow.PlayPropSlr.Value);
        }
        private static void VolumeControllSlr_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (MainWindow.VolumeControllPropSlr.Value<=1)
            {
                MainWindow.VolumeControllPropSlr.Value = 0;

                MainWindow.AudioPropMiaEl.Volume = 0;

                MainWindow.VolumeValuePropTb.Text = "0%";

                return;
            }


            MainWindow.AudioPropMiaEl.Volume = MainWindow.VolumeControllPropSlr.Value / 100.0;

            MainWindow.VolumeValuePropTb.Text = Convert.ToString(Math.Round( MainWindow.VolumeControllPropSlr.Value,0)) + "%";
        }

        private  void AudioMiaEl_MediaEnded(object sender, RoutedEventArgs e)
        {
            

            MainWindow.AudioPropMiaEl.Position = System.TimeSpan.FromSeconds(0);



            MainWindow.PlayPauseResumePropBtn.Content = "Play";

            MainWindow.AudioPropMiaEl.Pause();

            isPlay = false;


        }


        private void SaveInMp3PropBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("output.wav"))
            {
                synthesizer.SetOutputToWaveFile("output.wav");
                synthesizer.Speak(MainWindow.AddTextForReadingPropTb.Text);
                synthesizer.SetOutputToDefaultAudioDevice();
            }

            using (AudioFileReader wav = new AudioFileReader("output.wav"))
            {
              
              
                LameMP3FileWriter mp3 = new LameMP3FileWriter("output.mp3",wav.WaveFormat,LAMEPreset.STANDARD);

                wav.CopyTo(mp3);

                mp3.Close();

                wav.Close();
            }

            File.Delete("output.wav");


            SaveFileDialog saveMP3 = new SaveFileDialog();

            saveMP3.FileName = "output.mp3";
            saveMP3.Filter = "Video files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (saveMP3.ShowDialog()==true)
            {
                if (File.Exists(saveMP3.FileName))
                {
                    File.Delete(saveMP3.FileName);
                }

                File.Move("output.mp3",saveMP3.FileName);
                
            }
        }

    }
}

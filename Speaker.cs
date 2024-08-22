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
using Text_reader.Database_operations;


namespace Text_reader
{
    internal class Speaker
    {
        private static Speaker speaker;

        private static SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        private string originText;

        private string originPath;

        private bool isPlay = true;


        public SettingsCurrentTable SettingsCurrent { get; private set; }

        public static Speaker CurrentSpeaker { get; private set; }

        public Speaker()
        {
            SettingsCurrent = new SettingsCurrentTable();
            CurrentSpeaker = this;

            MainWindow.SaveInMp3PropBtn.IsEnabled = true;
            MainWindow.VolumeControllPropSlr.IsEnabled = true;
            MainWindow.MaximizeSpeedPropBtn.IsEnabled = true;
            MainWindow.MinimizeSpeedPropBtn.IsEnabled = true;



            MainWindow.AddTextForReadingPropTb.TextChanged -= MainWindow.AddTextForReadingTb_TextAdded;
            MainWindow.TextFilesPathPropTb.TextChanged -= MainWindow.TextFilesPathTb_TextChanged;
            MainWindow.PlayPauseResumePropBtn.Click -= MainWindow.PlayPauseResumeBtn_Click;

         
         

            

            string text = MainWindow.AddTextForReadingPropTb.Text;
            string filePath = "output.wav";

            originText = text;

            originPath = MainWindow.TextFilesPathPropTb.Text;

         

           

            AddAudioToAudioMiaEl();

            MainWindow.SaveInMp3PropBtn.Click += SaveInMp3PropBtn_Click;
            MainWindow.TextFilesPathPropTb.TextChanged += TextFilesPathTb_PathChanged;
            MainWindow.PlayPauseResumePropBtn.Click += PausePlay_Click;
            MainWindow.AudioPropMiaEl.MediaEnded += AudioMiaEl_MediaEnded;
            MainWindow.PlayPropSlr.ValueChanged += PlayPropSlr_ValueChanged;
            MainWindow.AddTextForReadingPropTb.TextChanged += AddTextForReadingTb_CheckChanges;




            MainWindow.AudioPropMiaEl.Play();


            speaker = this;


            MainWindow.AudioPropMiaEl.Volume = MainWindow.VolumeControllPropSlr.Maximum;

            MainWindow.VolumeValuePropTb.Text = Convert.ToString(MainWindow.AudioPropMiaEl.Volume) + "%";
        }


        private  void TextFilesPathTb_PathChanged(object sender, RoutedEventArgs e)
        { 
                if (originPath != MainWindow.TextFilesPathPropTb.Text && File.Exists(MainWindow.TextFilesPathPropTb.Text))
                {
                    DestructSpeaker();

                    MainWindow.AddTextForReadingPropTb.Text = File.ReadAllText(MainWindow.TextFilesPathPropTb.Text);

                    AddAudioToAudioMiaEl();
                }
            
        }

        private static void CreateWavFile()
        {
            SettingsCurrentTable settings = new SettingsCurrentTable();


            synthesizer.Volume = Convert.ToInt32(settings.GetData()[1]);
  
            synthesizer.Rate =Convert.ToInt32( settings.GetData()[2]);



            synthesizer.SelectVoice(settings.GetData()[0].ToString());
         

            synthesizer.SetOutputToWaveFile("output.wav");
            synthesizer.Speak(MainWindow.AddTextForReadingPropTb.Text);
            synthesizer.SetOutputToDefaultAudioDevice();




        }

        private static void AddAudioToAudioMiaEl()
        {
           


            if(File.Exists("output.wav"))
            {

                File.Delete("output.wav");

                MainWindow.AudioPropMiaEl.Source = null;

            }



            CreateWavFile();

            FileInfo file = new FileInfo("output.wav");

            MainWindow.PlayPropSlr.IsEnabled = true;

            MainWindow.AudioPropMiaEl.Source = new Uri(file.FullName);



            

        }

        private void AddTextForReadingTb_CheckChanges(object sender, RoutedEventArgs e)
        {
            if (MainWindow.AddTextForReadingPropTb.Text != originText)
            {

                DestructSpeaker();
                AddAudioToAudioMiaEl();

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
                CreateWavFile();
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

        public void DestructSpeaker()
        {
          


            MainWindow.SaveInMp3PropBtn.Click -= SaveInMp3PropBtn_Click;
            MainWindow.TextFilesPathPropTb.TextChanged -= TextFilesPathTb_PathChanged;
            MainWindow.PlayPauseResumePropBtn.Click -= PausePlay_Click;
            MainWindow.AudioPropMiaEl.MediaEnded -= AudioMiaEl_MediaEnded;
            MainWindow.PlayPropSlr.ValueChanged -= PlayPropSlr_ValueChanged;
            MainWindow.AddTextForReadingPropTb.TextChanged -= AddTextForReadingTb_CheckChanges;

            MainWindow.AudioPropMiaEl.Source = null;


            MainWindow.SaveInMp3PropBtn.IsEnabled = false;
            MainWindow.VolumeControllPropSlr.IsEnabled = false;
            MainWindow.MaximizeSpeedPropBtn.IsEnabled = false;
            MainWindow.MinimizeSpeedPropBtn.IsEnabled = false;



            MainWindow.AddTextForReadingPropTb.TextChanged += MainWindow.AddTextForReadingTb_TextAdded;
            MainWindow.TextFilesPathPropTb.TextChanged += MainWindow.TextFilesPathTb_TextChanged;
            MainWindow.PlayPauseResumePropBtn.Click += MainWindow.PlayPauseResumeBtn_Click;

            if (File.Exists("output.wav"))
            {
                File.Delete("output.wav");
            }

            //MainWindow.AudioPropMiaEl.Source = null;

            CurrentSpeaker = null;
        }

    }
}

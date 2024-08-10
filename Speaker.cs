﻿using System;
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

        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        private string originText;

        private bool isPlay = true;

        public Speaker()
        {
            MainWindow.AddTextForReadingPropTb.TextChanged -= MainWindow.AddTextForReadingTb_TextAdded;
            MainWindow.TextFilesPathPropTb.TextChanged -= MainWindow.TextFilesPathTb_TextChanged;
            MainWindow.PlayPauseResumePropBtn.Click -= MainWindow.PlayPauseResumeBtn_Click;

            MainWindow.SaveInMp3PropBtn.IsEnabled = true;
            MainWindow.PlayPauseResumePropBtn.Click += PausePlay_Click;

            string text = MainWindow.AddTextForReadingPropTb.Text;
            string filePath = "output.wav";

            originText = text;

            MainWindow.SaveInMp3PropBtn.Click += SaveInMp3PropBtn_Click;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            synthesizer.SetOutputToWaveFile(filePath);
            synthesizer.Speak(text);
            synthesizer.SetOutputToDefaultAudioDevice();

            FileInfo file = new FileInfo(filePath);

            MainWindow.PlayPropSlr.IsEnabled = true;

            MainWindow.AudioPropMiaEl.Source = new Uri(file.FullName);

           

            MainWindow.AudioPropMiaEl.Play();

            MainWindow.AudioPropMiaEl.MediaEnded += AudioMiaEl_MediaEnded;

            MainWindow.PlayPropSlr.ValueChanged += PlayPropSlr_ValueChanged;
        }

        private void (object sender, RoutedEventArgs e)
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
            MainWindow.AudioPropMiaEl.Position =TimeSpan.FromSeconds( MainWindow.PlayPropSlr.Value);
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

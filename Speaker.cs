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
namespace Text_reader
{
    internal class Speaker
    {

        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();




        public Speaker()
        {
            MainWindow.AddTextForReadingPropTb.TextChanged -= MainWindow.AddTextForReadingTb_TextChanged;
            MainWindow.TextFilesPathPropTb.TextChanged -= MainWindow.TextFilesPathTb_TextChanged;
            MainWindow.PlayPauseResumePropBtn.Click -= MainWindow.PlayPauseResumeBtn_Click;

            string text = MainWindow.AddTextForReadingPropTb.Text;
            string filePath = "output.wav";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            synthesizer.SetOutputToWaveFile(filePath);
            synthesizer.Speak(text);
            synthesizer.SetOutputToDefaultAudioDevice();

            FileInfo file = new FileInfo(filePath);

   

            MainWindow.AudioPropMiaEl.Source = new Uri(file.FullName);

           

            MainWindow.AudioPropMiaEl.Play();
        }





    }
}

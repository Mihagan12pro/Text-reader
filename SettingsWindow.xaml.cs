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
using System.Windows.Shapes;
using System.Speech.Synthesis;



using Database;
//using System.Data.SQLite;
namespace Text_reader
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();



            for(int i=0;i<101;i++)
            {
                SetVolumeCB.Items.Add(Convert.ToString(i));
            }

            for(double i=0.5;i<2.5;i+=0.5)
            {
                SetSpeedCB.Items.Add(Convert.ToString(i));
            }


            SpeechSynthesizer speech = new SpeechSynthesizer();
            foreach(var voice in speech.GetInstalledVoices())
            {
                SetVoiceCB.Items.Add(voice.VoiceInfo.Name); 
            }


            BaseDefaultTable baseDefaultTable = new BaseDefaultTable();


            //var a = defaultTable.GetVoiceSettings();
            //var b = a;
        }
    }
}

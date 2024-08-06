using Microsoft.Win32;
using System.Net;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;

using System.IO;
using System.Windows.Threading;
using System;
namespace Text_reader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private DispatcherTimer timer;

        public static TextBox TextFilesPathPropTb { get; private set; }
        public static TextBox AddTextForReadingPropTb { get; private set; }

        public static Button AddTextFilePropBtn { get; private set; }
        public static Button SettingsPropBtn { get; private set; }
        public static Button SaveInMp3PropBtn { get; private set; }
        public static Button PlayPauseResumePropBtn { get; private set; }

        public static Slider PlayPropSlr { get; private set; }

        public static MediaElement AudioPropMiaEl { get; private set; }

        public MainWindow()
        {
            InitializeComponent();


            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(10);

            timer.Tick += new EventHandler(Timer_Tick);

            timer.Start();

            foreach (UIElement ui in MainGrid.Children)
            {


                if (ui is Button)
                {
                    Button button = (Button)ui;

                    switch (button.Name)
                    {
                        case "AddTextFileBtn":
                            AddTextFilePropBtn = AddTextFileBtn;
                            break;

                        case "SettingsBtn":
                            SettingsPropBtn = SettingsBtn;
                            break;

                        case "SaveInMp3Btn":
                            SaveInMp3PropBtn = SaveInMp3Btn;
                            break;
                        case "PlayPauseResumeBtn":
                            PlayPauseResumePropBtn = PlayPauseResumeBtn;
                            break;

                    }
                }
                else if (ui is Slider)
                {
                    Slider slider = (Slider)ui;

                    switch (slider.Name)
                    {
                        case "PlaySlr":
                            PlayPropSlr = PlaySlr;
                            break;
                    }
                }
                else if (ui is TextBox)
                {
                    TextBox textBox = (TextBox)ui;

                    switch (textBox.Name)
                    {
                        case "AddTextForReadingTb":
                            AddTextForReadingPropTb = AddTextForReadingTb;
                            break;
                        case "TextFilesPathTb":
                            TextFilesPathPropTb = TextFilesPathTb;
                            break;
                    }
                }

                else if (ui is MediaElement)
                {
                    MediaElement media = (MediaElement)ui;

                    switch(media.Name)
                    {
                        case "AudioMiaEl":
                            AudioPropMiaEl = media;
                            break;
                    }
                    //case "AudioMiaEl":
                    //   AudioPropMiaEl = AudioMiaEl;
                    //   break;

                }
            }


            AddTextFileBtn.Click += AddTextFileBtn_Click;
            TextFilesPathTb.TextChanged += TextFilesPathTb_TextChanged;
            AddTextForReadingTb.TextChanged += AddTextForReadingTb_TextChanged;
            PlayPauseResumeBtn.Click += PlayPauseResumeBtn_Click;

            AudioPropMiaEl.MediaOpened += AudioPropMiaEl_MediaOpened;

            AudioMiaEl.LoadedBehavior = MediaState.Manual;

           //AudioPropMiaEl.MediaEnded += AudioMiaEl_MediaEnded;



        }



        private void AudioPropMiaEl_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (AudioPropMiaEl.NaturalDuration.HasTimeSpan)
            {
                PlayPropSlr.Maximum = AudioPropMiaEl.NaturalDuration.TimeSpan.TotalSeconds;
            }
        }
       

        public  void Timer_Tick(object sender, EventArgs e)
        {
            PlayPropSlr.Value = AudioPropMiaEl.Position.TotalSeconds;
        }





        public static void PlayPauseResumeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AddTextForReadingPropTb.Text.Length >0)
            {
                Speaker speaker = new Speaker();

                PlayPauseResumePropBtn.Content = "Pause";
            }
        }

        public static void AddTextFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openTextFile = new OpenFileDialog();

            openTextFile.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            openTextFile.ShowDialog();


            TextFilesPathPropTb.Text = openTextFile.FileName;

        }

        public static void TextFilesPathTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextFilesPathPropTb.Text.EndsWith(".txt") && File.Exists(TextFilesPathPropTb.Text))
            {
                AddTextForReadingPropTb.Text = File.ReadAllText(TextFilesPathPropTb.Text);
            }
        }

        public static void AddTextForReadingTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AddTextForReadingPropTb.Text.Length > 0)
            {
                PlayPauseResumePropBtn.IsEnabled = true;
                SaveInMp3PropBtn.IsEnabled = true;

                return;
            }
            PlayPauseResumePropBtn.IsEnabled = false;
            SaveInMp3PropBtn.IsEnabled = false;
        }

        private void SaveInMp3Btn_Click(object sender, RoutedEventArgs e)
        {

        }


        //private void Synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        //{
        //    PlayPauseResumeBtn.Content = "Play";
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

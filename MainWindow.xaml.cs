using Microsoft.Win32;
using System.Net;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;

using System.IO;
using System.Windows.Threading;
using System;
using System.Data.SQLite;
using Text_reader.Database_operations;
using System.ComponentModel;

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

        public static TextBox VolumeValuePropTb { get; private set; }

        public static Button MinimizeSpeedPropBtn { get; private set; }
        public static Button MaximizeSpeedPropBtn { get; private set; }

        public static Slider VolumeControllPropSlr { get; private set; }

        public static TextBox SpeedControlPropTb { get; private set; }


        public MainWindow()
        {
            InitializeComponent();

            
           



            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(1);

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




                        case "MinimizeSpeedBtn":
                            MinimizeSpeedPropBtn = MinimizeSpeedBtn;

                            MinimizeSpeedBtn.IsEnabled = false;
                            break;
                        case "MaximizeSpeedBtn":
                            MaximizeSpeedPropBtn = MaximizeSpeedBtn;
                            MaximizeSpeedBtn.IsEnabled = false;
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

                        case "VolumeControllSlr":
                            VolumeControllPropSlr = VolumeControllSlr;

                            

                            VolumeControllPropSlr.IsEnabled = false;
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
                        case "VolumeValueTb":
                            VolumeValuePropTb = VolumeValueTb;
                            break;
                        case "SpeedControlTb":
                            SpeedControlPropTb = SpeedControlTb;

                            SpeedControlTb.IsEnabled = false;

                            SpeedControlTb.TextChanged += SpeedControlTb_TextChanged; ;

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

                else if (ui is Label)
                {
                    Label label = (Label)ui;

                }
            }


            AddTextFileBtn.Click += AddTextFileBtn_Click;
            TextFilesPathTb.TextChanged += TextFilesPathTb_TextChanged;
            AddTextForReadingTb.TextChanged += AddTextForReadingTb_TextAdded;
            PlayPauseResumeBtn.Click += PlayPauseResumeBtn_Click;
            MainWindow.VolumeControllPropSlr.ValueChanged += VolumeControllSlr_ValueChanged;


            AudioPropMiaEl.MediaOpened += AudioPropMiaEl_MediaOpened;

            AudioMiaEl.LoadedBehavior = MediaState.Manual;

           //AudioPropMiaEl.MediaEnded += AudioMiaEl_MediaEnded;



        }

        private void SpeedControlTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            AudioMiaEl.SpeedRatio = Convert.ToDouble(SpeedControlTb.Text);
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

        public static void AddTextForReadingTb_TextAdded(object sender, TextChangedEventArgs e)
        {
            if (AddTextForReadingPropTb.Text.Length > 0)
            {
                
                PlayPauseResumePropBtn.IsEnabled = true;

                return;
            }
            PlayPauseResumePropBtn.IsEnabled = false;
            SaveInMp3PropBtn.IsEnabled = false;
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();

            


            settingsWindow.ShowDialog();
        }

        private void MinimizeSpeedBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(SpeedControlTb.Text)>0.5)
            {
                double content = Convert.ToDouble(SpeedControlTb.Text);

                content -= 0.5;

                SpeedControlTb.Text = Convert.ToString( content);
            }
        }

        private void MaximizeSpeedBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(SpeedControlTb.Text) < 2.0)
            {
                double content = Convert.ToDouble(SpeedControlTb.Text);

                content += 0.5;

                SpeedControlTb.Text = Convert.ToString(content);
            }
        }
        private static void VolumeControllSlr_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (MainWindow.VolumeControllPropSlr.Value <= 1)
            {
                MainWindow.VolumeControllPropSlr.Value = 0;

                MainWindow.AudioPropMiaEl.Volume = 0;

                MainWindow.VolumeValuePropTb.Text = "0%";

                return;
            }


            MainWindow.AudioPropMiaEl.Volume = MainWindow.VolumeControllPropSlr.Value / 100.0;

            MainWindow.VolumeValuePropTb.Text = Convert.ToString(Math.Round(MainWindow.VolumeControllPropSlr.Value, 0)) + "%";
        }



        private void Closing_MainWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source="+"..\\..\\Database operations\\settings.db");


            sQLiteConnection.Open();

            using (SQLiteCommand dropCommand = new SQLiteCommand(sQLiteConnection))
            {
                dropCommand.CommandText = "DROP TABLE IF EXISTS current";


                dropCommand.ExecuteNonQuery();
            }


            sQLiteConnection.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //base.OnClosing(e);

            
            if (File.Exists("output.wav"))
            {
                try
                {
                    File.Delete("output.wav");
                }
                catch (Exception ex)
                {
                }
            }
            if (File.Exists("..\\..\\Database operations\\settings.db"))
            {
                SQLiteConnection connection = new SQLiteConnection("Data Source="+"..\\..\\Database operations\\settings.db");

                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = "DROP TABLE IF EXISTS current ";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}

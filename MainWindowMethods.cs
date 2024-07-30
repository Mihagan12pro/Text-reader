using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.IO;

namespace Text_reader
{
    internal static class MainWindowMethods
    {
        private static List<UIElement> uiList = new List<UIElement>();

        public static List<UIElement> UiList { get { return uiList; } }

        private static TextBox AddTextForReadingTb, TextFilesPathTb;
        private static Button AddTextFileBtn, SettingsBtn,  SaveInMp3Btn, PlayPauseResumeBtn;
        private static Slider PlaySlr;

        public static void InitiliazeStaticFilelds()
        {
            if (uiList.Count > 0)
            {
                foreach (var ui in uiList)
                {
                    if (ui is TextBox)
                    {
                        TextBox textbox = (TextBox)ui;

                        switch (textbox.Name)
                        {
                            case "AddTextForReadingTb":
                                AddTextForReadingTb = textbox;
                                break;
                            case "TextFilesPathTb":
                                TextFilesPathTb = textbox;
                                break;
                        }
                    }
                    else if (ui is Button)
                    {
                        Button button = (Button)ui;

                        switch (button.Name)
                        {
                            case "AddTextFileBtn":
                                AddTextFileBtn = button;
                                break;

                            case "SettingsBtn":
                                SettingsBtn = button;
                                break;

                            case "SaveInMp3Btn":
                                SaveInMp3Btn = button;
                                break;
                            case "PlayPauseResumeBtn":
                                PlayPauseResumeBtn = button;
                                break;
                        }
                    }
                    else if (ui is Slider)
                    {
                        Slider slider = (Slider)ui;

                        switch (slider.Name)
                        {

                            case "PlaySlr":
                                PlaySlr = slider;
                                break;

                        }
                    }

                }
            }
        }

        public static void AddTextFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openTextFile = new OpenFileDialog();

            openTextFile.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            openTextFile.ShowDialog();


            TextFilesPathTb.Text = openTextFile.FileName;

        }

        public static  void TextFilesPathTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextFilesPathTb.Text.EndsWith(".txt") && File.Exists(TextFilesPathTb.Text))
            {
                AddTextForReadingTb.Text = File.ReadAllText(TextFilesPathTb.Text);
            }
        }

        public static void AddTextForReadingTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AddTextForReadingTb.Text.Length > 0)
            {
                PlayPauseResumeBtn.IsEnabled = true;
                SaveInMp3Btn.IsEnabled = true;

                return;
            }
            PlayPauseResumeBtn.IsEnabled = false;
            SaveInMp3Btn.IsEnabled = false;
        }

    }
}

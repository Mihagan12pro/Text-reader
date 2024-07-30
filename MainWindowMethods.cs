using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

    }
}

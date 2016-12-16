using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System.Threading;
using Mfrc522Lib;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RFID_Reader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Main Page Constructor
        /// </summary>
        public MainPage()
        {
            //initialize form elements
            this.InitializeComponent();

            //hide the picture
            imgRFID.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        /// <summary>
        /// RFID Presence Function
        /// </summary>
        private async void RFID_Presence()
        {
            //initialize the RFID Reader
            Mfrc522 mfrc = new Mfrc522();
            await mfrc.InitIO();

            //loop
            while (true)
            {
                //error handling
                try
                {
                    //verify if the tag is present
                    if (mfrc.IsTagPresent())
                    {
                        //show the picture
                        imgRFID.Visibility = Windows.UI.Xaml.Visibility.Visible;

                        //read the UUID and show in UI
                        var uid = mfrc.ReadUid();
                        txtUUID.Text = uid.ToString();

                        //rfid controller halt state
                        mfrc.HaltTag();
                    }
                    else
                    {
                        //reset the uuid
                        txtUUID.Text = String.Empty;

                        //hide the picture
                        imgRFID.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                }
                catch { }

                //sleep
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1));
            }       
        }

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            //RFID Check
            RFID_Presence();
        }
    }
}

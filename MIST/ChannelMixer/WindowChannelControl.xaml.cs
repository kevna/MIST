using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace ApplicationMist
{
    /// <summary>
    /// Interaction logic for WindowChannelControl.xaml
    /// </summary>
    public partial class WindowChannelControl : SurfaceWindow
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WindowChannelControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Custom function for showing the window so that we can first dynamically populate it from the model.
        /// 
        /// We populate it right before it's shown to avoid it being out of date.
        /// </summary>
        public void Open()
        {
            // Hold a reference for the current channel strip as we make each one
            SingleChannelStrip ChannelStrip;

            // Loop through the model's list of channels
            foreach (ChannelItem CurrentChannel in App.ChannelController)
            {
                // Create a new channel strip with the current channel
                ChannelStrip = new SingleChannelStrip(CurrentChannel);

                // Add the new channel strip to the mixer panel in the window
                MixerPanel.Children.Add(ChannelStrip);
            }

            // Finished populating the controls, now we can show the window
            this.Show();
        }
        
    }
}
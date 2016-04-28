using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Windows;

namespace ApplicationMist
{
    /// <summary>
    /// Interaction logic for WindowChannelSequencer.xaml
    /// </summary>
    public partial class WindowChannelSequencer : SurfaceWindow
    {
        private WindowChannelControl internalMixerWindow;
        public WindowChannelControl MixerWindow
        {
            get
            {
                // If we've created a MixerWindow object and it's loaded then we can use that.
                if (internalMixerWindow != null && internalMixerWindow.IsLoaded)
                {
                    return internalMixerWindow;
                }

                // We don't have an existing MixerWindow object (or the previous one was closed)
                // So we need to create a new one.
                internalMixerWindow = new WindowChannelControl();

                return internalMixerWindow;
            }
        }

        public WindowChannelSequencer()
        {
            InitializeComponent();
        }

        private void MixerButton_Click(object sender, RoutedEventArgs e)
        {
            // Show the mixer window we've created
            MixerWindow.Open();
        }

        private void ChannelButton_Click(object sender, RoutedEventArgs e)
        {
            ChannelItem NewChannel = new ChannelItem();

            App.ChannelController.AddChannel(NewChannel);

            SequencerPanel.Children.Add(new WholeSequencerRow(NewChannel));
        }
    }
}

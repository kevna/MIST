using Microsoft.Surface.Presentation.Controls;
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

namespace ApplicationMist
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SingleChannelStrip : UserControl
    {
        /// <summary>
        /// Create a channel strip, with an associated channel for it to edit.
        /// </summary>
        /// <param name="Channel">A ChannelItem model that this control will be linked to.</param>
        public SingleChannelStrip(ChannelItem Channel)
        {
            InitializeComponent();
            LinkedChannel = Channel;

            VolumeSlider.Value = Channel.Volume;
            OnButton.IsChecked = Channel.Audiable;
            BalancePot.Value = LinkedChannel.Balance;
        }

        /// <summary>
        /// Toggle the Audiable state of the linked channel model (unmuted or not).
        /// </summary>
        /// <param name="sender">Sender of the click event.</param>
        /// <param name="e">Event from button click.</param>
        private void OnButton_Click(object sender, RoutedEventArgs e)
        {
            // If the channel is audiable we'll toggle it off
            if (LinkedChannel.Audiable)
            {
                LinkedChannel.Audiable = false;
                OnButton.IsChecked = false;
            }
            // The channel isn't audiable so we'll toggle it back on
            else
            {
                LinkedChannel.Audiable = true;
                OnButton.IsChecked = true;
            }
        }

        /// <summary>
        /// Adjust the volume of the linked channel model.
        /// </summary>
        /// <param name="sender">Sender of the value change event,</param>
        /// <param name="e">Event from value change.</param>
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Set the channel's volume property
            LinkedChannel.Volume = (int)VolumeSlider.Value;
        }

        /// <summary>
        /// Adjust the balance of the linked channel model.
        /// </summary>
        /// <param name="sender">Sender of the value change event.</param>
        /// <param name="e">Event from value change.</param>
        private void BalancePot_ValueChanged(object sender, EventArgs e)
        {
            // Set the channel's balance property
            LinkedChannel.Balance = BalancePot.Value;
        }
    }
}

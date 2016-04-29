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
using Microsoft.Surface.Presentation;
using System.Windows.Forms;

namespace ApplicationMist
{

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SingleSequencerBar : System.Windows.Controls.UserControl
    {
        protected List<Rectangle> Soundbytes;

        protected Rectangle InteractedSound;

        protected Point LastPoint;

        protected int SoundLength;

        protected SolidColorBrush SoundFill;

        public SolidColorBrush BarBrush
        {
            get { return SoundFill; }
            set
            {
                SoundFill = value;

                foreach (Rectangle CurrentSound in Soundbytes)
                {
                    CurrentSound.Fill = SoundFill;
                }
            }
        }

        /// <summary>
        /// Time of gesture start which we can use to identify a tap gesture.
        /// </summary>
        private int TapTime;

        public SingleSequencerBar()
        {
            InitializeComponent();
            Soundbytes = new List<Rectangle>();
            SoundFill = SurfaceColors.Accent1Brush;
            SoundLength = 120;
        }

        private Rectangle AddSound(double Xposition)
        {
            Rectangle NewSound = new Rectangle
            {
                Width = SoundLength,
                Height = ControlArea.ActualHeight - 2,
                VerticalAlignment = VerticalAlignment.Stretch,
                Fill = SoundFill,
            };

            Soundbytes.Add(NewSound);

            ControlArea.Children.Add(NewSound);

            Canvas.SetTop(NewSound, 0);
            Canvas.SetLeft(NewSound, Xposition);

            double EndPosition = Xposition + NewSound.ActualWidth;
            if (ControlArea.MinWidth < EndPosition)
            {
                ControlArea.MinWidth = EndPosition;
            }

            return NewSound;
        }

        private void MoveSound(Rectangle MovingSound, double PositionChange)
        {
            double NewPostition = Canvas.GetLeft(InteractedSound) + PositionChange;

            // Stop the movement pushing the sound ahead of the start of the track;
            if (NewPostition < 0)
            {
                NewPostition = 0;
            }

            Canvas.SetLeft(InteractedSound, NewPostition);
        }

        private void RemoveSound(Rectangle TakenSound)
        {
            ControlArea.Children.Remove(InteractedSound);

            Soundbytes.Remove(InteractedSound);
        }

        /// <summary>
        /// This handler is called when a TouchDevice is first recognized.
        /// </summary>
        /// <param name="sender">the element raising the TouchDownEvent</param>
        /// <param name="e">information about this TouchDownEvent</param>
        private void OnTouchDown(object sender, TouchEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("TouchDown", "Event", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            // Grab the entire input area so that we don't lose contact with the gesture.
            e.TouchDevice.Capture(ControlArea);

            // Store the timestampt to identify a tap gesture.
            TapTime = e.Timestamp;

            // Get the contact position
            LastPoint = e.GetTouchPoint(ControlArea).Position;

            // Make sure the interacted object is null before we try to find it.
            InteractedSound = null;

            // Find the individual instance we're interacting with.
            foreach (Rectangle CurrentSound in Soundbytes)
            {
                // If the rectangle starts before our contact point and ends after it then it's the point we've contacted on.
                if ((Canvas.GetLeft(CurrentSound) <= LastPoint.X)
                    && ((Canvas.GetLeft(CurrentSound) + CurrentSound.ActualWidth) >= LastPoint.X))
                {
                    // Store the contacted object before we drop out of the loop.
                    InteractedSound = CurrentSound;
                    break;
                }
            }
            
            // If we don't currently have an interacted sound then we'll add one
            /*if (InteractedSound == null)
            {
                InteractedSound = AddSound(LastPoint.X);
            }*/

            //System.Windows.Forms.MessageBox.Show("TouchDown " + Canvas.GetLeft(InteractedSound), "Event", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// This handler is called when a TouchDevice is first recognized.
        /// </summary>
        /// <param name="sender">the element raising the TouchMoveEvent</param>
        /// <param name="e">information about this TouchMoveEvent</param>
        private void OnTouchMove(object sender, TouchEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("TouchMove", "Event", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Point CurrentPoint = e.GetTouchPoint(ControlArea).Position;

            if (InteractedSound != null)
            {
                double DiffX = CurrentPoint.X - LastPoint.X;

                MoveSound(InteractedSound, DiffX);
            }

            LastPoint = CurrentPoint;
        }

        /// <summary>
        /// This handler is called when a TouchDevice is first recognized.
        /// </summary>
        /// <param name="sender">the element raising the TouchUpEvent</param>
        /// <param name="e">information about this TouchUpEvent</param>
        private void OnTouchUp(object sender, TouchEventArgs e)
        {
            // If we tapped for less than 200miliseconds then we'll add or remove
            if ((e.Timestamp - TapTime) <= 200)
            {
                //System.Windows.Forms.MessageBox.Show("TouchUp", "Event", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // Check if we tapped an existing instance, if so remove it
                if (InteractedSound != null)
                {
                    RemoveSound(InteractedSound);
                }
                // We didn't tap an existing instance so we'll add one instead
                else
                {
                    Point CurrentPoint = e.GetTouchPoint(ControlArea).Position;

                    AddSound(CurrentPoint.X);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    // This delegate should allow us to use ValueChangedEventHandler instead of having a raw event which contains no data
    // Since an environment bug breaks when using ValueChangedEventArgs (despite it being a system class)
    // We're currently using EventArgs as a temporary work-around.
    //public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs e);

    /// <summary>
    /// Interaction logic for SurfaceBalanceDial.xaml
    /// </summary>
    public partial class SurfaceControlBalanceDial : System.Windows.Controls.UserControl
    {

        /// <summary>
        /// The returnable value (int -100 to 100) as a clean value for use with the model.
        /// </summary>
        public int Value
        {
            // Scale the angle from ±115 to ±100 and round it neatly (round retuns decimal so we cast it to int)
            get { return (int)Math.Round((Angle / 115) * 100); }

            // Scale the given value from ±100 to ±115 for the angle
            // using a float in the devision is implicit rather than explicit casting
            // don't need to check limits here as we know they're handled by the Angle property
            set { Angle = (value / 100f) * 115; }
        }

        /// <summary>
        /// Internal property that wraps the rotation angle of the control to add appropriate limits
        /// </summary>
        protected double Angle
        {
            // Nothing special, just get the rotation angle from the transformation
            get { return DialRotation.Angle; }

            // Check limits on the value to make sure they're within bounds
            set
            {
                // If it's less than the minimum (or greater than 180 from our start point) then we're at the minimum
                if (value < -115 || value > 215)
                {
                    value = -115;
                }
                // If it's greater than the maximum we'll cut it off there
                else if (value > 115)
                {
                    value = 115;
                }

                // Set the rotation angle for the display transformation
                DialRotation.Angle = value;

                // Create an event to trigger ValueChanged in the container classes
                // We do this here rather than on value because we're still changing the value if we only use the internal property
                SurfaceControlBalanceDial_ValueChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Centrepoint for rotation gestures so that we know where the gesture is relative to.
        /// </summary>
        private Point PivotCentre;

        /// <summary>
        /// Time of gesture start which we can use to identify a tap gesture.
        /// </summary>
        private int TapTime;

        public SurfaceControlBalanceDial()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Helper function to calculate the angle between the origin of the gesture and it's current position.
        /// </summary>
        /// <param name="Position">Current position of the contact point used for the gesture.</param>
        /// <param name="Centre">Original position of the contact point for the gesture.</param>
        /// <returns>Angle between start and current positions in the gesture (in degrees).</returns>
        private double CalcAngle(Point Position, Point Centre)
        {
            double DiffX = Position.X - Centre.X;
            double DiffY = Position.Y - Centre.Y;

            // Math Arctan function returns radians but we'll need degrees for the transformation angle
            // By converting here we can keep the 90 degree correction (from vertical to horizontal) inside
            // but still have it in a whole-number (rather than π/2 rads)
            return (Math.Atan2(DiffY, DiffX) * (180 / Math.PI)) + 90;
        }

        /// <summary>
        /// Touch gesture begins, we'll store the information we need to follow the gesture.
        /// </summary>
        /// <param name="sender">The element raising the TouchDownEvent,</param>
        /// <param name="e">Information about this TouchDownEvent.</param>
        private void OnTouchDown(object sender, TouchEventArgs e)
        {
            // Grab the entire input area so that we don't lose contact with the gesture.
            e.TouchDevice.Capture(ControlArea);

            // Store the centrepoint of the rotation gesture
            PivotCentre = e.GetTouchPoint(ControlArea).Position;

            // Store the timestampt to identify a tap gesture
            TapTime = e.Timestamp;

            // Expand the control to indicate to the user that the gesture is working.
            DialSwell.Margin = new Thickness(10);
        }

        /// <summary>
        /// As the gesture progresses we track it's movement.
        /// </summary>
        /// <param name="sender">The element raising the TouchMoveEvent.</param>
        /// <param name="e">Information about this TouchMoveEvent.</param>
        private void OnTouchMove(object sender, TouchEventArgs e)
        {
            // Get the current position of the touch event
            Point PivotPosition = e.GetTouchPoint(ControlArea).Position;

            // Calculate the movement angle, offset it from the previous position.
            Angle += CalcAngle(PivotPosition, PivotCentre);
        }

        /// <summary>
        /// Gesture is over, no we can find out if it was a quick tap to reset.
        /// </summary>
        /// <param name="sender">The element raising the TouchUpEvent.</param>
        /// <param name="e">Information about this TouchUpEvent.</param>
        private void OnTouchUp(object sender, TouchEventArgs e)
        {
            // If we tapped for less than 200miliseconds then we reset the balance to a perfect 50:50 split
            if ((e.Timestamp - TapTime) <= 200)
            {
                Angle = 0;
            }

            // Shrink the control to indicate to the user that the gesture is over.
            DialSwell.Margin = new Thickness(20);
        }

        // Add Event handling for container classes to react to the changing value
        // Using raw EventArgs is a bad idea, however there is currently an environment bug
        // which is preventing the use of the correct ValueChangedEventArgs
        public event EventHandler ValueChanged;
        protected void SurfaceControlBalanceDial_ValueChanged(object sender, EventArgs e)
        {
            //bubble the event up to the parent
            if (this.ValueChanged != null)
                this.ValueChanged(this, e);
        }
    }
}

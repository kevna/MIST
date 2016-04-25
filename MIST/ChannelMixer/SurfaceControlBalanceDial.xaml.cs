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
    /// <summary>
    /// Interaction logic for SurfaceBalanceDial.xaml
    /// </summary>
    public partial class SurfaceControlBalanceDial : System.Windows.Controls.UserControl
    {
        public SurfaceControlBalanceDial()
        {
            InitializeComponent();
        }

        private Point PivotCentre;

        private int TapTime;

        private double CalcAngle(Point Position, Point Centre)
        {
            double DiffX = Position.X - Centre.X;
            double DiffY = Position.Y - Centre.Y;

            return (Math.Atan2(DiffY, DiffX) * (180 / Math.PI)) + 90;
        }

        /// <summary>
        /// This handler is called when a TouchDevice is first recognized.
        /// </summary>
        /// <param name="sender">the element raising the TouchDownEvent</param>
        /// <param name="e">information about this TouchDownEvent</param>
        private void OnTouchDown(object sender, TouchEventArgs e)
        {
            //Acquire TouchDevice capture so ActiveArea will receive all events for this TouchDevice.
            //The LostTouchCapture event is used here for notification that this TouchDevice has been
            //completely removed. LostTouchCapture is raised after the TouchUp event.
            //The TouchDevice will still be counted when calling UIElement.TouchesOver even when both the
            //TouchUp and LostTouchCapture events are raised. But when calling UIElement.TouchesCaptured,
            //the TouchDevice will not be counted when the LostTouchCapture event is raised, while it
            //will be when the Touchup event is raised. Therefore to update the statistics, it is more
            //appropriate to use UIElement.Captured during the LostTouchCapture event.
            e.TouchDevice.Capture(ControlArea);

            PivotCentre = e.GetTouchPoint(ControlArea).Position;

            TapTime = e.Timestamp;

            DialSwell.Margin = new Thickness(10);
        }

        /// <summary>
        /// This handler is called when a TouchDevice is first recognized.
        /// </summary>
        /// <param name="sender">the element raising the TouchMoveEvent</param>
        /// <param name="e">information about this TouchMoveEvent</param>
        private void OnTouchMove(object sender, TouchEventArgs e)
        {
            Point PivotPosition = e.GetTouchPoint(ControlArea).Position;

            double rotation = DialRotation.Angle + CalcAngle(PivotPosition, PivotCentre);

            if (rotation < -115 || rotation > 215)
            {
                rotation = -115;
            }
            else if (rotation > 115)
            {
                rotation = 115;
            }

            DialRotation.Angle = rotation;
        }

        /// <summary>
        /// This handler is called when a TouchDevice is first recognized.
        /// </summary>
        /// <param name="sender">the element raising the TouchUpEvent</param>
        /// <param name="e">information about this TouchUpEvent</param>
        private void OnTouchUp(object sender, TouchEventArgs e)
        {
            // If we tapped for less than 200miliseconds then we reset the balance to a perfect 50:50 split
            if ((e.Timestamp - TapTime) <= 200)
            {
                DialRotation.Angle = 0;
            }

            DialSwell.Margin = new Thickness(20);
        }
    }
}

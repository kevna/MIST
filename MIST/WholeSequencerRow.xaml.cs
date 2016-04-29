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

namespace ApplicationMist
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class WholeSequencerRow : UserControl
    {
        protected ChannelItem channelModel;

        public WholeSequencerRow(ChannelItem channel, int RowNumber)
        {
            InitializeComponent();
            channelModel = channel;
            SequencerBar.BarBrush = RowColour(RowNumber);
        }

        protected SolidColorBrush RowColour(int RowNumber)
        {
            if (RowNumber > 4)
            {
                RowNumber = RowNumber % 4;
            }

            SolidColorBrush Colour = SurfaceColors.Accent1Brush;

            switch(RowNumber)
            {
                case 2:
                    Colour = SurfaceColors.Accent2Brush;
                    break;
                case 3:
                    Colour = SurfaceColors.Accent3Brush;
                    break;
                case 4:
                    Colour = SurfaceColors.Accent4Brush;
                    break;
            }

            return Colour;
        }

    }
}

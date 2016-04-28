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
    public partial class WholeSequencerRow : UserControl
    {
        protected ChannelItem channelModel;

        public WholeSequencerRow(ChannelItem channel)
        {
            InitializeComponent();
            channelModel = channel;
        }


    }
}

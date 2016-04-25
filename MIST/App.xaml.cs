using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace ApplicationMist
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static ChannelList ChannelController;

        public App()
        {
            ChannelController = new ChannelList();
        }
    }
}
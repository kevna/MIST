﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationMist
{
    public class ChannelList : IEnumerable
    {

        protected List<ChannelItem> Channels;

        public ChannelList()
        {
            Channels = new List<ChannelItem>();
            AddChannel();
        }

        /// <summary>
        /// Add a new channel item to the list.
        /// </summary>
        /// <param name="Channel">ChannelItem object to add to the list.</param>
        /// <returns>Index for the newly added channel.</returns>
        public int AddChannel(ChannelItem Channel)
        {
            // Add the new ChannelItem to the channel list
            Channels.Add(Channel);

            return Channels.LastIndexOf(Channel);
        }

        /// <summary>
        /// Add a new channel item to the list.
        /// 
        /// Overloaded to add a new channel using the filename argument.
        /// </summary>
        /// <param name="FileName">Name of the soundbyte file to load.</param>
        /// <returns>Index for the newly added channel.</returns>
        public int AddChannel(String FileName)
        {
            return AddChannel(new ChannelItem(FileName));
        }

        /// <summary>
        /// Add a new channel item to the list.
        /// 
        /// Overloaded to add a new blank channel.
        /// </summary>
        /// <returns>Index for the newly added channel.</returns>
        public int AddChannel()
        {
            return AddChannel(new ChannelItem());
        }

        /// <summary>
        /// Get the ChannelItem from the list by index.
        /// 
        /// Allows accessing the channels in the model to update them.
        /// </summary>
        /// <param name="ChannelID">Index of the ChannelItem to load.</param>
        /// <returns>ChannelItem at given index.</returns>
        public ChannelItem GetChannel(int ChannelID)
        {
            return Channels[ChannelID];
        }

        public IEnumerator GetEnumerator()
        {
            return Channels.GetEnumerator();
        }
    }
}

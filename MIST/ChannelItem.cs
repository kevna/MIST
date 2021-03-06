﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ApplicationMist
{
    public class ChannelItem
    {
        public bool Audiable { get; set; }

        protected SortedSet<TimeSpan> PlaybackPoints;

        /// <summary>
        /// Internal variable for Volume property.
        /// </summary>
        private float VolumeLevel;

        /// <summary>
        /// Volume property with limit enforcement on mutator (setter).
        /// 
        /// allows getting and setting volume with more natural 0-100 value (internally uses 0.0f-1.0f)
        /// </summary>
        public int Volume
        {
            // Simple accessor, we don't need to do anything special getting the volume level
            get { return (int)(100 * VolumeLevel); }

            // Mutator with limits for volume level (can't be over 1 (100%) or under 0 (0%))
            set
            {
                float volume = (value / 100f);
                // Upper limit 1.0f (100%)
                if (volume > 1.0f)
                {
                    volume = 1.0f;
                }
                // Lower limit 0.0f (0%)
                else if (volume < 0.0f)
                {
                    volume = 0.0f;
                }

                // Set the volume level
                VolumeLevel = volume;
                
                //OnPropertyChanged("Volume");
            }
        }

        /// <summary>
        /// Internal variable for Balance property.
        /// </summary>
        private float BalanceValue;

        /// <summary>
        /// Balance property with limit enforcement on mutator (setter).
        /// </summary>
        public int Balance
        {
            // Simple accessor, we don't need to do anything special getting the balance value
            get { return (int)(100 * BalanceValue); }

            // Mutator with limits for balance value (can't be over 1 (full right) or under -1 (full left))
            set
            {
                float balance = (value / 100f);
                // Upper limit 1.0f (full right)
                if (balance > 1.0f)
                {
                    balance = 1.0f;
                }
                // Lower limit -1.0f (full left)
                else if (balance < -1.0f)
                {
                    balance = -1.0f;
                }

                // Set the balance value
                BalanceValue = balance;
            }
        }

        /// <summary>
        /// Song object for the soundbyte connected to this channel.
        /// </summary>
        //protected SoundEffect ChannelSoundbyte;

        /// <summary>
        /// List of times to start channel playback.
        /// </summary>
        protected List<TimeSpan> ChannelPlaybackTimes;

        /// <summary>
        /// Create ChannelItem without attaching a soundbyte.
        /// </summary>
        public ChannelItem()
        {
            PlaybackPoints = new SortedSet<TimeSpan>();
            Volume = 100;
            Balance = 0;
            Audiable = true;
            //ChannelSoundbyte = null;
        }

        /// <summary>
        /// Create ChannelItem initialised with a soundbyte.
        /// </summary>
        /// <param name="FileName">Name of the soundbyte file to load.</param>
        public ChannelItem(String FileName)
        {
            PlaybackPoints = new SortedSet<TimeSpan>();
            Volume = 100;
            Balance = 0;
            Audiable = true;
            LoadSoundbyte(FileName);
        }

        /// <summary>
        /// Load a soundbyte file into this channel item.
        /// </summary>
        /// <param name="FileName">Name of the soundbyte file to load.</param>
        public void LoadSoundbyte(String FileName)
        {
            // Load the soundbyte into the channel item
            // ChannelSoundbyte = Content.Load<SoundEffect>(FileName);
        }

        /// <summary>
        /// Playback the soundbyte in this channel (if there is a soundbyte to play).
        /// 
        /// Returns Boolean false rather than throwing error if playback is unsuccessful
        /// so that playback can occur normally if players have channels without assigned soundbytes.
        /// </summary>
        /// <param name="Volume">The volume to playback the current soundbyte.</param>
        /// <param name="Pan">The balance to use playing back the current soundbyte.</param>
        /// <returns>True if playing back successfully, false in case of error</returns>
        public Boolean Play()
        {
            // Don't have soundbyte to play back
            /*if (null == ChannelSoundbyte)
            {
                // Sound is not playing if there's no sound to play.
                return false;
            }*/

            // SoundEffect.Play returns true if playing correctly, else false
            //return ChannelSoundbyte.Play(Volume, 0.0f, Balance);
            return false;
        }
        
    }
}

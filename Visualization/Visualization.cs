using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Visualization
{
    public class Sounds
    {
        public static void PlayMusic(Song song,float volume)
        {
            MediaPlayer.Play(song);
            MediaPlayer.Volume = volume;
        }
        public static void StopMusic()
        {
            MediaPlayer.Stop();
        }
        public static float ChangeVolume(float old,float change)
        {
            if ((old + change >= 0.0f) && (old + change <= 1.0f))
            {
                old = old + change;
            }
            else if (old + change < 0.0)
            {
                old = 0.0f;
            }
            else if (old + change > 1.0)
            {
                old = 1.0f;
            }

            
           MediaPlayer.Volume = old;
           return old;
        }
    }
}

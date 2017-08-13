using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Sage_Engine
{


    public class SoundHandler
    {
        protected AudioEngine engine;
        protected SoundBank soundBank;
        protected WaveBank waveBank;
        protected Dictionary<String, Cue> soundLibrary = new Dictionary<String, Cue>();



        public void playSound(String name)
        {

            if (!soundLibrary[name].IsPlaying)
            {
                soundLibrary[name] = soundBank.GetCue(name);

                Cue c = soundLibrary[name];
                c.Play();
            }

        }

        public void pauseSound(String name)
        {
            if (soundLibrary[name].IsPlaying)
            {
                soundLibrary[name].Pause();
            }
        }

        public void resumeSound(String name)
        {

            soundLibrary[name].Resume();
        }
    }

}
using Diploma2015.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diploma2015.GameLogic
{
    public class SoundEffects : Screen
    {
        SoundEffects windBackgr;

        Dictionary<string, SoundEffects> sounds = new Dictionary<string, SoundEffects>();

        public void LoadContent()
        {
            windBackgr = content.Load<SoundEffects>("assets/audio/backgroundSounds/WindBackgrSound");
        }

        public void AddSounds()
        {
            sounds.Add("wind", windBackgr);

        }
    }
}

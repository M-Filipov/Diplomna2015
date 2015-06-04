using Diploma2015.Screens;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diploma2015.GameLogic
{
    public class SoundManager
    {
        private Song windBackgr;
        public Song clickSound;

        public Dictionary<string, Song> sounds = new Dictionary<string, Song>();

        public SoundManager(ContentManager content)
        {
            LoadContent(content);
            AddSounds();
        }

        public void LoadContent(ContentManager content)
        {
            //windBackgr = content.Load<Song>("assets/audio/backgroundSounds/WindBackgrSound");
            clickSound = content.Load<Song>("assets/audio/Click22-Sebastian-759472264");
        }

        public void AddSounds()
        {
            sounds.Add("click", clickSound);
        }

        public void PlaySound(string soundName)
        {
            if(GameVars.IsSoundOn)
                MediaPlayer.Play(sounds[soundName]);
        }
    }
}

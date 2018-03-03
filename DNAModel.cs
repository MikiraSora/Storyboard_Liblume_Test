using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class DNAModel : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 10000;

        [Configurable]
        public int AnimationTimeInv = 33;

        [Configurable]
        public int Count = 50;

        private OsbSprite GenerateSprite(string path)
        {
            var layer = GetLayer("LineEffect");
            var sprite = layer.CreateSprite(@"SB\spetrum\1.png", OsbOrigin.BottomCentre);
            sprite.Fade(OsbEasing.None, StartTime, EndTime, 1, 1);

            return sprite;
        }
        public override void Generate()
        {
            for (int i = 0; i < Count; i++)
            {
                
            }
        }
    }
}

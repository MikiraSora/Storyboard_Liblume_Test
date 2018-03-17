using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.CommandValues;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class BlackScreenTransform : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime;

        [Configurable]
        public int EndTime;
        
        [Configurable]
        public int FadeOutTime;

        [Configurable]
        public OsbEasing FadeOutEasing;

        [Configurable]
        public OsbEasing FadeInEasing;

        [Configurable]
        public int FadeInTime;

        public override void Generate()
        {
            var sprite = GetLayer(this.Identifier).CreateSprite(@"SB\effect\pixel.png");
            sprite.ScaleVec(StartTime, new CommandScale(854,480));
            sprite.Color(StartTime,0,0,0);

            sprite.Fade(FadeOutEasing, StartTime, StartTime + FadeOutTime, 0, 1);
            sprite.Fade(FadeInEasing, EndTime-FadeInTime, EndTime, 1,0);
        }
    }
}

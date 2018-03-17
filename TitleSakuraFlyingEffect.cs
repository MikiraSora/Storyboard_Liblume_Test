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
    public class TitleSakuraFlyingEffect : StoryboardObjectGenerator
    {
        [Configurable]
        public float RaiseTime;

        [Configurable]
        public float RaiseRandomTimeRate;

        [Configurable]
        public float RaiseRandomHeightRate;

        [Configurable]
        public int Count;

        [Configurable]
        public float BaseY;

        [Configurable]
        public float RaiseHeight;

        [Configurable]
        public int StartTime;

        [Configurable]
        public int EndTime;

        [Configurable]
        public float MinAlpha;

        [Configurable]
        public float MaxAlpha;

        [Configurable]
        public int FadeInOutTime;

        [Configurable]
        public float ScaleMin;

        [Configurable]
        public float ScaleMax;

        public override void Generate()
        {
		    
        }
    }
}

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
    public class BeginningBG : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime;

        [Configurable]
        public int EndTime;
        
        [Configurable]
        public int FadeInTime;
        
        [Configurable]
        public int FadeOutTime;
        
        [Configurable]
        public OsbEasing FadeInEasing;

        [Configurable]
        public OsbEasing FadeOutEasing;
        
        [Configurable]
        public string FilePath;

        [Configurable]
        public float Scale;
        
        [Configurable]
        public float TransformBeginScale;
        
        [Configurable]
        public OsbEasing ScaleEasing;
        
        [Configurable]
        public int ScaleTime;

        public override void Generate()
        {
            OsbSprite sprite = GetLayer("Beginning_BG").CreateSprite(FilePath);

            sprite.Scale(ScaleEasing, StartTime, StartTime + ScaleTime, TransformBeginScale, Scale);
            sprite.Fade(FadeOutEasing, StartTime, StartTime + FadeOutTime, 0, 1);
            sprite.Fade(FadeInEasing,EndTime - FadeInTime, EndTime, 1, 0);
        }
    }
}

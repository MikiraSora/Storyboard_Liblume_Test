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
    public class RingAppearanceEffect : StoryboardObjectGenerator
    {
        [Configurable]
        public int AppearanceTime;

        [Configurable]
        public int KeepTime;

        [Configurable]
        public int FadeOutTime;

        [Configurable]
        public int FadeInTime;

        [Configurable]
        public OsbEasing ScaleEasing;

        [Configurable]
        public float MinScale;

        [Configurable]
        public float MaxScale;

        [Configurable]
        public int ScaleTime;

        [Configurable]
        public Vector2 Position=new Vector2(320,240);

        public override void Generate()
        {
            var sprite = GetLayer(this.Identifier).CreateSprite(@"SB\effect\blur_ball.png",OsbOrigin.Centre, Position);

            sprite.Fade(OsbEasing.None, AppearanceTime, AppearanceTime + FadeOutTime, 0, 1);
            sprite.Fade(OsbEasing.None, AppearanceTime+KeepTime - FadeInTime, AppearanceTime+KeepTime, 1, 0);

            sprite.Scale(ScaleEasing, AppearanceTime, AppearanceTime + ScaleTime, MinScale, MaxScale);
        }
    }
}

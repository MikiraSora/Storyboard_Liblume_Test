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
    public class TitleLightDotFlyEffect : StoryboardObjectGenerator
    {
        [Configurable]
        public float FlyTime;

        [Configurable]
        public float FlyRandomTimeRate;

        [Configurable]
        public int Count;

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
        public float BaseSize;

        [Configurable]
        public float SizeRange;

        public override void Generate()
        {
            var layer = GetLayer("TitleLightDotFlyEffect");

            for (int i = 0; i < Count; i++)
            {
                var sprite = layer.CreateSprite(@"SB\effect\light_dot.png");

                float total_time = StartTime + (float)Random(0, 1.0f) * FlyTime;

                while (total_time < EndTime)
                {
                    float cast_time = (float)Random(FlyTime * (1 - FlyRandomTimeRate), FlyTime * (1 + FlyRandomTimeRate));

                    var random_start_posion = new CommandPosition(854, Random(-0, 480));
                    var end_position = new CommandPosition(854 - Random(854 * (1 - 0.25), 854 * (1 + 0.25)), Random(-0, 480));
                    var alpha = Random(MinAlpha, MaxAlpha);

                    sprite.Fade(OsbEasing.None, total_time, total_time + FadeInOutTime, 0, alpha);
                    sprite.Fade(OsbEasing.None, total_time + cast_time - FadeInOutTime, total_time + cast_time, alpha, 0);
                   
                    sprite.Additive(total_time, total_time + cast_time);

                    sprite.Move(OsbEasing.None, total_time, total_time + cast_time, random_start_posion, end_position);

                    total_time += cast_time;
                }
            }
        }
    }
}

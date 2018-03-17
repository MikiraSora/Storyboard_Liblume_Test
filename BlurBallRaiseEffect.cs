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
    class BlurBallRaiseEffect : StoryboardObjectGenerator
    {
        [Configurable]
        public string FilePath;

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
            var layer = GetLayer("BlurBallRaiseEffect");

            for (int i = 0; i < Count; i++)
            {
                var sprite = layer.CreateSprite(FilePath);

                float total_time = StartTime + (float)Random(0, 1.0f) * RaiseTime;

                while (total_time<EndTime)
                {
                    float cast_time = (float)Random(RaiseTime * (1 - RaiseRandomTimeRate), RaiseTime * (1 + RaiseRandomTimeRate));

                    var random_start_posion = new CommandPosition(Random(-107, 640), BaseY);
                    var end_position = new CommandPosition(random_start_posion.X, random_start_posion.Y  - Random(RaiseHeight * (1 - RaiseRandomHeightRate), RaiseHeight * (1 + RaiseRandomHeightRate)));
                    var alpha = Random(MinAlpha, MaxAlpha);

                    sprite.Fade(OsbEasing.None, total_time, total_time + FadeInOutTime, 0, alpha);
                    sprite.Fade(OsbEasing.None, total_time+ cast_time - FadeInOutTime, total_time+ cast_time, alpha, 0);

                    sprite.Scale(total_time, Random(ScaleMin, ScaleMax));
                    sprite.Color(total_time, GetRandomColor());

                    sprite.Move(OsbEasing.None, total_time, total_time + cast_time, random_start_posion, end_position);

                    total_time += cast_time;
                }
            }
        }

        private CommandColor GetRandomColor()
        {
            var rand_add = Random(-30, 30);

            if (Random(3) == 0)
            {
                rand_add = 255-Math.Abs(rand_add);
            }
            else
            {
                rand_add = Math.Min(255, Math.Max(0, 185 + rand_add));
            }

            return new CommandColor(rand_add, rand_add, rand_add);
        }
    }
}

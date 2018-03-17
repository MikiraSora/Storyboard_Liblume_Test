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
    public class TitleSakuraFlyingEffect : StoryboardObjectGenerator
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
            var layer = GetLayer("");

            for (int i = 0; i < Count; i++)
            {
                var sprite = layer.CreateSprite($@"SB\effect\sakura\flower{Random(1,5)}.png");

                float total_time = StartTime + (float)Random(0, 1.0f) * FlyTime;

                while (total_time < EndTime)
                {
                    float cast_time = (float)Random(FlyTime * (1 - FlyRandomTimeRate), FlyTime * (1 + FlyRandomTimeRate));

                    var random_start_posion = new CommandPosition(854, Random(-0, 480));
                    var end_position = new CommandPosition(854 - Random(854 * (1 - 0.25), 854 * (1 + 0.25)), Random(-0, 480));
                    var alpha = Random(MinAlpha, MaxAlpha);

                    var random_radius = Random(0, 2 * Math.PI);
                    var random_rotate_radius = Random(-420, 420) * Math.PI / 180;

                    float random_scale_start_x, random_scale_start_y, random_scale_end_x, random_scale_end_y;
                    float keep_scale = (float)Random(BaseSize * (1 - SizeRange), BaseSize * (1 + SizeRange));
                    float change_scale_start = (float)(Random(BaseSize * (1 - SizeRange), BaseSize * (1 + SizeRange)) * Random(0, 1.0f));
                    float change_scale_end = Random(2857) % 5 == 0 ? change_scale_start : (float)(Random(BaseSize * (1 - SizeRange), BaseSize * (1 + SizeRange)) * Random(0, 1.0f));
                    if (Random(2857) % 2 == 0)
                    {
                        random_scale_start_x = random_scale_end_x = keep_scale;
                        random_scale_start_y = change_scale_start;
                        random_scale_end_y = change_scale_end;
                    }
                    else
                    {
                        random_scale_start_y = random_scale_end_y = keep_scale;
                        random_scale_start_x = change_scale_start;
                        random_scale_end_x = change_scale_end;
                    }

                    sprite.Fade(OsbEasing.None, total_time, total_time + FadeInOutTime, 0, alpha);
                    sprite.Fade(OsbEasing.None, total_time + cast_time - FadeInOutTime, total_time + cast_time, alpha, 0);
                    sprite.Rotate(OsbEasing.None, total_time, total_time + cast_time, random_radius, random_rotate_radius);
                    sprite.ScaleVec(OsbEasing.None,total_time,total_time+cast_time,new CommandScale(random_scale_start_x, random_scale_start_y),new CommandScale(random_scale_end_x, random_scale_end_y));

                    sprite.Additive(total_time, total_time + cast_time); 

                    sprite.Move(OsbEasing.None, total_time, total_time + cast_time, random_start_posion, end_position);

                    total_time += cast_time;
                }
            }
        }
    }
}

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
    public class LineEffect : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 10000;

        [Configurable]
        public float BaseWidthScale = 1;

        [Configurable]
        public int AnimationTimeInv=33;

        [Configurable]
        public float BaseHeightOffset = 0.1f;

        [Configurable]
        public int PositionDistance = 50;

        public List<Vector2> point_list = new List<Vector2>();
        public List<OsbSprite> sprite_list = new List<OsbSprite>();

        public override void Generate()
        {
            var layer = GetLayer("LineEffect");
            for (int i = -107; i < 640; i += PositionDistance)
            {
                var sprite = layer.CreateSprite(@"SB\spetrum\1.png", OsbOrigin.BottomCentre);
                sprite.Fade(OsbEasing.None, StartTime, EndTime, 1, 1);
                sprite_list.Add(sprite);
            }

            for (int t = StartTime; t < EndTime; t+=AnimationTimeInv)
            {
                point_list.Clear();
                for (int x = -107; x < 640; x += PositionDistance)
                {
                    float y = 200 * (float)Math.Sin((x + 107.0f + t) / 854 * 360 / 180 * Math.PI);
                    var position = new Vector2(x, 240 + y);
                    point_list.Add(position);
                }

                ApplyCurrentList(t,t+AnimationTimeInv);
            }

        }

        public void ApplyCurrentList(int start_time, int end_time)
        {
            for (int i = 0; i < point_list.Count - 1; i++)
            {
                Vector2 cur_position = point_list[i], next_position = point_list[i + 1];
                OsbSprite cur_sprite = sprite_list[i], next_sprite = sprite_list[i + 1];

                float angle = (float)(Math.Atan((cur_position.Y - next_position.Y) / (cur_position.X - next_position.X)) + 0.5 * Math.PI);

                float scale = BaseHeightOffset + (float)(Math.Sqrt(Math.Pow(cur_position.X - next_position.X, 2) + Math.Pow(cur_position.Y - next_position.Y, 2))) / 126.0f;

                cur_sprite.Rotate(OsbEasing.None, start_time, end_time, angle, angle);
                cur_sprite.Move(OsbEasing.None,start_time,end_time,cur_position,cur_position);
                cur_sprite.ScaleVec(OsbEasing.None, start_time, end_time, BaseWidthScale, scale, BaseWidthScale, scale);
            }
        }
    }
}

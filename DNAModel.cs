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

        [Configurable]
        public int StartX=-107;

        [Configurable]
        public int Width=854;

        [Configurable]
        public int BaseY=240;

        [Configurable]
        public int Height=200;

        [Configurable]
        public int MoveXInv=100;

        [Configurable]
        public int MoveYInv=1000;

        [Configurable]
        public int MoveXUnitInv=200;

        private OsbSprite GenerateSprite(string path)
        {
            var layer = GetLayer("LineEffect");
            var sprite = layer.CreateSprite(path, OsbOrigin.Centre);

            return sprite;
        }
        public override void Generate()
        {
            float center_base_scale=Height/126.0f;

            for (int i = 0; i < Count; i++)
            {
                float start_x_offset=(i/(float)Count)*Width;
                //height=126

                float start_y_offset=(((int)start_x_offset)%MoveXUnitInv/MoveXUnitInv-0.5f)*Height;

                #region //todo sprite center
                var sprite_center=GenerateSprite(@"SB\spetrum\1.png");
                #endregion
            }
        }
    }
}

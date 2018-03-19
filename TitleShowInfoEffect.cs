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
    public class TitleShowInfoEffect : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime;

        [Configurable]
        public int EndTime;

        [Configurable]
        public int FadeOutTime;

        [Configurable]
        public int FadeInTime;

        [Configurable]
        public int BaseY;

        [Configurable]
        public int StartX;

        [Configurable]
        public float Scale;

        [Configurable]
        public float Offset;

        [Configurable]
        public OsbEasing MoveEasing;

        [Configurable]
        public int MoveTime;

        [Configurable]
        public int MoveDistance;

        [Configurable]
        public int FadeOutTimeInv;
        
        public override void Generate()
        {
            ShowContent("Liblume",MoveDistance, StartTime,EndTime,StartX,BaseY, FadeOutTime,FadeInTime,Scale,Offset,MoveTime,MoveEasing);
            ShowContent("霜月はるか", MoveDistance, StartTime, EndTime, StartX, BaseY + 128 * Scale, FadeOutTime, FadeInTime, Scale, Offset, MoveTime, MoveEasing);
        }

        private void ShowContent(string content,int move_distance, int start_time,int end_time, int start_x, float base_y,int fadeout_time,int fadein_time, float scale,float offset, int move_time,OsbEasing move_easing)
        {
            float size = 128 * Scale;

            for (int i = 0; i < content.Length; i++)
            {
                char c = content[i];
                var position = new CommandPosition(start_x + (offset + size) * i, base_y);

                var sprite = GetLayer("TitleShowInfoEffect").CreateSprite($@"SB\font\title\{(int)c}.png", OsbOrigin.Centre, new CommandPosition(position.X, base_y));

                sprite.Fade(OsbEasing.None, start_time+ FadeOutTimeInv*i, start_time + FadeOutTime + FadeOutTimeInv * i, 0, 1);
                sprite.Fade(OsbEasing.None, end_time - FadeInTime, end_time, 1, 0);

                sprite.Scale(start_time, scale);
                sprite.Color(start_time, 0, 0, 0);

                sprite.MoveX(move_easing, start_time, start_time + move_time, position.X,position.X+ move_distance);
            }
        }
    }
}
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
    public class TitleShowTitleEffect : StoryboardObjectGenerator
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
        public Vector2 SakuraOffset;

        [Configurable]
        public float SakuraAngle;

        [Configurable]
        public float SakuraScale;

        public float[] CharPositionOffset =new []{-25,15.0f,0,-10,-10,25};

        [Configurable]
        public int SubTitleStartX;

        [Configurable]
        public int SubTitleBaseY;

        [Configurable]
        public float SubTitleScale;
        
        [Configurable]
        public float SubTitleUnitOffset;

        [Configurable]
        public float FadeOutTimeInv;

        [Configurable]
        public int INOCHIStartY;

        [Configurable]
        public int INOCHIBaseX;

        [Configurable]
        public float INOCHIScale;

        [Configurable]
        public float INOCHIUnitOffset;

        [Configurable]
        public float INOCHIFadeOutTimeInv;

        public override void Generate()
        {
            ShowMainTitle();
            ShowTitleSakuraPart();
            ShowSubTitle();
            ShowINOCHIPart();
        }

        private void ShowINOCHIPart()
        {
            string title = @"いのち";
            float size = 32 * INOCHIScale;

            for (int i = 0; i < title.Length; i++)
            {
                char c = title[i];
                var position = new CommandPosition(INOCHIBaseX, INOCHIStartY + (INOCHIUnitOffset + size) * i);

                var sprite = GetLayer("TitleShowTitleEffect").CreateSprite($@"SB\font\title\l{(int)c}.png", OsbOrigin.Centre, new CommandPosition(position.X, position.Y));

                sprite.Fade(OsbEasing.None, StartTime + INOCHIFadeOutTimeInv * i, StartTime + INOCHIFadeOutTimeInv * i + FadeOutTime, 0, 1);
                sprite.Fade(OsbEasing.None, EndTime - FadeInTime, EndTime, 1, 0);

                sprite.Scale(StartTime, INOCHIScale);
                sprite.Color(StartTime, 0, 0, 0);
            }
        }

        private void ShowSubTitle()
        {
            string title = @"I was born for you";
            float size = 32 * SubTitleScale;

            for (int i = 0; i < title.Length; i++)
            {
                char c = title[i];
                var position = new CommandPosition(SubTitleStartX + (SubTitleUnitOffset + size) * i, SubTitleBaseY);

                var sprite = GetLayer("TitleShowTitleEffect").CreateSprite($@"SB\font\title\l{(int)c}.png", OsbOrigin.Centre, new CommandPosition(position.X, SubTitleBaseY));

                sprite.Fade(OsbEasing.None, StartTime+FadeOutTimeInv*i, StartTime + FadeOutTimeInv * i+FadeOutTime, 0, 1);
                sprite.Fade(OsbEasing.None, EndTime - FadeInTime, EndTime, 1, 0);

                sprite.Scale(StartTime, SubTitleScale);
                sprite.Color(StartTime, 0, 0, 0);
            }
        }

        private void ShowTitleSakuraPart()
        {
            //flower4.png

            float size = 128 * Scale;

            var position = new CommandPosition(StartX + (Offset + size) * 3+ SakuraOffset.X, BaseY + CharPositionOffset[3] * Scale+ SakuraOffset.Y);
            var sprite = GetLayer("TitleShowTitleEffect").CreateSprite(@"SB\effect\title_flower.png", OsbOrigin.Centre, new CommandPosition(position.X, BaseY));

            sprite.Fade(OsbEasing.None, StartTime, StartTime + FadeOutTime, 0, 1);
            sprite.Fade(OsbEasing.None, EndTime - FadeInTime, EndTime, 1, 0);

            sprite.Rotate(StartTime, SakuraAngle * Math.PI / 180);
            sprite.Scale(StartTime, SakuraScale);

            var color_change_time = StartTime + (EndTime - StartTime) / 2;
            sprite.Color(StartTime, 0, 0, 0);
            sprite.Color(OsbEasing.None, color_change_time, color_change_time+500, new CommandColor(0,0,0),new CommandColor(1,1,1));

            sprite.MoveY(MoveEasing, StartTime, StartTime + MoveTime, BaseY, position.Y);
        }

        private void ShowMainTitle()
        {
            string title = @"伏凋のスペア";
            float size = 128 * Scale;

            for (int i = 0; i < title.Length; i++)
            {
                char c = title[i];
                var position = new CommandPosition(StartX + (Offset + size) * i, BaseY + CharPositionOffset[i] * Scale);

                var sprite = GetLayer("TitleShowTitleEffect").CreateSprite($@"SB\font\title\{(int)c}.png", OsbOrigin.Centre, new CommandPosition(position.X, BaseY));

                sprite.Fade(OsbEasing.None, StartTime, StartTime + FadeOutTime, 0, 1);
                sprite.Fade(OsbEasing.None, EndTime - FadeInTime, EndTime, 1, 0);

                sprite.Scale(StartTime, Scale);
                sprite.Color(StartTime, 0, 0, 0);

                sprite.MoveY(MoveEasing, StartTime, StartTime + MoveTime, BaseY, position.Y);
            }
        }
    }
}

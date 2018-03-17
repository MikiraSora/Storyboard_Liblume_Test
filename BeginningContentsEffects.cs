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
    public class BeginningContentsEffects : StoryboardObjectGenerator
    {
        [Configurable]
        public Vector2 Position = new Vector2(320, 240);

        [Configurable]
        public float FontScale = 0.25f;

        [Configurable]
        public float UnitOffset = 0;

        [Configurable]
        public int StartTime;

        [Configurable]
        public int EndTime;

        [Configurable]
        public string LyricsContent;

        [Configurable]
        public string CharPath = @"SB\font\lyrics";

        [Configurable]
        public int FadeInTimeInv = 50;

        public override void Generate()
        {
            List<OsbSprite> LyricsSprites = new List<OsbSprite>();

            foreach (char c in LyricsContent)
            {
                string char_path = System.IO.Path.Combine(CharPath, $"{(int)c}.png");
                OsbSprite sprite = GetLayer("BeginningContentsEffects").CreateSprite(char_path, OsbOrigin.CentreLeft);
                LyricsSprites.Add(sprite);
            }

            float font_size_len = 128 * FontScale;

            //显示字幕
            for (int i = 0; i < LyricsSprites.Count; i++)
            {
                var sprite = LyricsSprites[i];
                sprite.Fade(OsbEasing.None, StartTime + i * FadeInTimeInv, StartTime + 500 + i * FadeInTimeInv, 0, 1);
                sprite.Fade(OsbEasing.None, EndTime - 500, EndTime, 1, 0);

                var postion = new CommandPosition(Position.X + (font_size_len + UnitOffset) * i, Position.Y);
                sprite.Move(StartTime, postion.X, postion.Y);
                sprite.Scale(StartTime, FontScale);
            }
        }
    }
}

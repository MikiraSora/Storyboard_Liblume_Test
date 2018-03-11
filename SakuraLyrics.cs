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
    public class SakuraLyrics : StoryboardObjectGenerator
    {
        [Configurable]
        public Vector2 Position=new Vector2(320,240);

        [Configurable]
        public float FontScale=0.25f;

        [Configurable]
        public float UnitOffset=0;
        
        [Configurable]
        public float SakuraAlpha=0.15f;
        
        [Configurable]
        public float SakuraUnitOffset-0.0025;

        [Configurable]
        public int StartTime;

        [Configurable]
        public int EndTime;

        [Configurable]
        public string LyricsContent;

        [Configurable]
        public string CharPath= @"SB\font\lyrics";

        public List<OsbSprite> sakura_bg_list=null;

        public List<OsbSprite> LyricsSprites = new List<OsbSprite>();

        public void InitSakuraBG()
        {
            var layer = GetLayer("LyricsLayer_BG");

            sakura_bg_list = new List<OsbSprite>();

            for (int i = 0; i < 15; i++)
            {
                string path = System.IO.Path.Combine(@"SB\effect\lyrics_bg", $"{i+1}.png");
                OsbSprite sprite = layer.CreateSprite(path, OsbOrigin.CentreRight);
                sakura_bg_list.Add(sprite);
            }
        }

        public override void Generate()
        {
            if (sakura_bg_list==null)
            {
                InitSakuraBG();
            }

            var layer = GetLayer("LyricsLayer");
            foreach (char c in LyricsContent)
            {
                string char_path = System.IO.Path.Combine(CharPath, $"{(int)c}.png");
                OsbSprite sprite = layer.CreateSprite(char_path, OsbOrigin.CentreLeft);
                LyricsSprites.Add(sprite);
            }

            float font_size_len=128*FontScale;

            //ÏÔÊ¾×ÖÄ»
            for(int i=0;i<LyricsSprites.Count;i++)
            {
                var sprite=LyricsSprites[i];
                sprite.Fade(OsbEasing.None,StartTime,StartTime+500,0,1);
                sprite.Fade(OsbEasing.None,EndTime-500,EndTime,1,0);

                var postion = new CommandPosition(Position.X+(font_size_len+UnitOffset)*i,Position.Y);
                sprite.Move(StartTime,postion.X,postion.Y);
                sprite.Scale( StartTime, FontScale);
            }

            //ÏÔÊ¾Ó£»¨±³¾° 92x480
            float total_width = LyricsSprites.Count * (font_size_len + UnitOffset);
            float sakura_size_width = 92.0f * FontScale;
            float sakura_size_height = 480.0f * FontScale;
            
            for (int current_index = 0;current_index<(total_width/(sakura_size_width+ SakuraUnitOffset));current_index++)
            {
                if (current_index >= sakura_bg_list.Count)
                {
                    int index = (current_index) % 15;

                    string path = System.IO.Path.Combine(@"SB\effect\lyrics_bg", $"{index + 1}.png");
                    OsbSprite sprite = GetLayer("LyricsLayer_BG").CreateSprite(path, OsbOrigin.CentreRight);
                    sakura_bg_list.Add(sprite);
                }

                var bg_sprite = sakura_bg_list[current_index];

                bg_sprite.Fade(OsbEasing.None, StartTime, StartTime + 500, 0, SakuraAlpha);
                bg_sprite.Fade(OsbEasing.None, EndTime - 500, EndTime, SakuraAlpha, 0);

                var postion = new CommandPosition(Position.X + (sakura_size_width + SakuraUnitOffset) * current_index, Position.Y);
                bg_sprite.Move(StartTime, postion.X, postion.Y);
                bg_sprite.Scale(StartTime, FontScale);
            }
        }
    }
}

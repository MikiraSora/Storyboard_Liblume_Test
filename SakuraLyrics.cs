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
        public float SakuraUnitOffset=0.0025f;

        [Configurable]
        public int StartTime;

        [Configurable]
        public int EndTime;

        [Configurable]
        public string LyricsContent;

        [Configurable]
        public string CharPath= @"SB\font\lyrics";

        [Configurable]
        public int FadeInTimeInv = 50;

        public List<OsbSprite> sakura_bg_list = new List<OsbSprite>();

        public List<OsbSprite> LyricsSprites = new List<OsbSprite>();

        public override void Generate()
        {
            var layer = GetLayer("LyricsLayer");
            foreach (char c in LyricsContent)
            {
                string char_path = System.IO.Path.Combine(CharPath, $"{(int)c}.png");
                OsbSprite sprite = layer.CreateSprite(char_path, OsbOrigin.CentreLeft);
                LyricsSprites.Add(sprite);
            }

            float font_size_len=128*FontScale;

            //显示字幕
            for(int i=0;i<LyricsSprites.Count;i++)
            {
                var sprite=LyricsSprites[i];
                sprite.Fade(OsbEasing.None,StartTime + i * FadeInTimeInv, StartTime+500+i*FadeInTimeInv, 0,1);
                sprite.Fade(OsbEasing.None,EndTime-500,EndTime,1,0);

                var postion = new CommandPosition(Position.X+(font_size_len+UnitOffset)*i,Position.Y);
                sprite.Move(StartTime,postion.X,postion.Y);
                sprite.Scale( StartTime, FontScale);
            }

            //显示樱花背景 92x480
            float total_lyrics_content_width = LyricsSprites.Count * (font_size_len + UnitOffset);
            float sakura_size_width = 92.0f * FontScale;
            float sakura_size_height = 480.0f * FontScale;
        
            int total_sakura_count = (int)(total_lyrics_content_width / (sakura_size_width + SakuraUnitOffset))+1;

            /*
            if (total_sakura_count>15)
            {
                GenerateFullSakura(total_sakura_count,sakura_size_width);
            }
            else
            {
                GenerateMirrorSakura(total_sakura_count, sakura_size_width);
            }
            */
            GenerateMirrorSakura(total_sakura_count, sakura_size_width);
        }

        //认怂,todo//显示一半index,后半部分是前半部分的镜像
        public void GenerateMirrorSakura(int total_sakura_count, float sakura_size_width)
        {
            for (int current_index = 0; current_index < total_sakura_count; current_index++)
            {
                var bg_sprite = RequestAvaliableSakuraSprite(current_index);

                bg_sprite.Fade(OsbEasing.None, StartTime + current_index * FadeInTimeInv, StartTime + 500 + current_index * FadeInTimeInv, 0, SakuraAlpha);
                bg_sprite.Fade(OsbEasing.None, EndTime - 500, EndTime, SakuraAlpha, 0);

                var postion = new CommandPosition(Position.X + (sakura_size_width + SakuraUnitOffset) * current_index, Position.Y);
                bg_sprite.Move(StartTime, postion.X, postion.Y);
                bg_sprite.Scale(StartTime, FontScale);
            }
        }

        //显示index 1~15,中间插几张镜像
        public void GenerateFullSakura(int total_sakura_count, float sakura_size_width)
        {
            for (int current_index = 0; current_index < total_sakura_count; current_index++)
            {
                int sprite_index = current_index;
                bool is_loop = false;

                if (sprite_index >8 && sprite_index < (total_sakura_count - 7))
                {
                    sprite_index = 8 + sprite_index % 2;
                    is_loop = sprite_index % 2 != 0;
                }

                if (sprite_index > (total_sakura_count - 7))
                {
                    sprite_index =8+total_sakura_count - sprite_index;
                }

                var bg_sprite = RequestAvaliableSakuraSprite(sprite_index);

                bg_sprite.Fade(OsbEasing.None, StartTime + current_index * FadeInTimeInv, StartTime + 500 + current_index * FadeInTimeInv, 0, SakuraAlpha);
                bg_sprite.Fade(OsbEasing.None, EndTime - 500, EndTime, SakuraAlpha, 0);

                if (is_loop)
                {
                    if (Random(2)!=0)
                    {
                        bg_sprite.FlipH(StartTime, EndTime);
                    }

                    if (Random(2) != 0)
                    {
                        //bg_sprite.FlipV(StartTime, EndTime);
                    }
                }

                var postion = new CommandPosition(Position.X + (sakura_size_width + SakuraUnitOffset) * current_index, Position.Y);
                bg_sprite.Move(StartTime, postion.X, postion.Y);
                bg_sprite.Scale(StartTime, FontScale);
            }
        }

        public OsbSprite RequestAvaliableSakuraSprite(int current_index)
        {
            int index = (current_index) % 15;

            string path = System.IO.Path.Combine(@"SB\effect\lyrics_bg", $"{index + 1}.png");
            OsbSprite sprite = GetLayer("LyricsLayer_BG").CreateSprite(path, OsbOrigin.CentreRight);
            return sprite;
        }
    }
}

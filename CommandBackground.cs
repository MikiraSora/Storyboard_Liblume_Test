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
    public class CommandBackground : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartMoveTime;

        [Configurable]
        public int KeepMoveTime;
        
        [Configurable]
        public Vector2 StartMovePosition;

        [Configurable]
        public Vector2 EndMovePosition;

        [Configurable]
        public int StartTime;

        [Configurable]
        public int EndTime;

        [Configurable]
        public int FadeInTime;

        [Configurable]
        public int FadeOutTime;

        [Configurable]
        public OsbEasing FadeInEasing;

        [Configurable]
        public OsbEasing FadeOutEasing;

        [Configurable]
        public string FilePath;

        [Configurable]
        public float TransformEndScale;

        [Configurable]
        public float TransformBeginScale;

        [Configurable]
        public OsbEasing ScaleEasing;

        [Configurable]
        public int ScaleTime;

        [Configurable]
        public string LayerName;

        public override void Generate()
        {
            OsbSprite sprite = GetLayer(LayerName).CreateSprite(FilePath);

            sprite.Scale(ScaleEasing, StartTime, StartTime + ScaleTime, TransformBeginScale, TransformEndScale);
            sprite.Fade(FadeOutEasing, StartTime, StartTime + FadeOutTime, 0, 1);
            sprite.Fade(FadeInEasing, EndTime - FadeInTime, EndTime, 1, 0);
            sprite.Move(OsbEasing.None, StartMoveTime, KeepMoveTime + StartMoveTime, StartMovePosition, EndMovePosition);
        }
    }
}

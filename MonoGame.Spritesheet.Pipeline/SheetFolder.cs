using System.Collections.Generic;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Newtonsoft.Json;

namespace MonoGame.Spritesheet.Pipeline
{
    public class SheetFolder
    {
        public string FolderPath { get; set; }
        public string Filter { get; set; }

        public List<SheetSubfolder> Folders { get; set; }

        [JsonIgnore]
        public IReadOnlyList<TextureContent> Textures { get; set; }
    }

    public struct SheetSubfolder
    {
        public string FolderPath { get; set; }
        public string Filter { get; set; }
        public bool IncludeSubfolders { get; set; }
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Spritesheet
{
    public sealed class Sheet
    {
        public Texture2D Texture { get; private set; }
        public IReadOnlyDictionary<string, int> Names { get; private set; }
        public IReadOnlyList<Rectangle> Sources { get; private set; }
        public IReadOnlyList<Vector2> Origins { get; private set; }

        public IReadOnlyList<Vector2> OriginalSizes { get; private set; }

        public Rectangle this[string name] => Sources[Names[name]];
        public Rectangle this[int index] => Sources[index];

        public Vector2 GetOrigin(string name) => Origins[Names[name]];
        public Vector2 GetOrigin(int index) => Origins[index];
        public Vector2 GetOriginalSize(string name) => OriginalSizes[Names[name]];
        public Vector2 GetOriginalSize(int index) => OriginalSizes[index];

        public Sheet()
        {
        }

        public Sheet(Texture2D texture, IReadOnlyDictionary<string, int> names, IReadOnlyList<Rectangle> sources, IReadOnlyList<Vector2> origins, IReadOnlyList<Vector2> originalSizes)
        {
            Texture = texture;
            Names = names;
            Sources = sources;
            Origins = origins;
            OriginalSizes = originalSizes;
        }
    }
}

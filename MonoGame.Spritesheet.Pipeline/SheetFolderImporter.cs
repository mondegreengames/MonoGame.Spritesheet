using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Newtonsoft.Json;

namespace MonoGame.Spritesheet.Pipeline
{
    [ContentImporter(".json", DefaultProcessor = nameof(SheetFolderProcessor), DisplayName = "SheetFolder Importer - Spritesheet")]
    public class SheetFolderImporter : ContentImporter<SheetFolder>
    {
        public override SheetFolder Import(string filename, ContentImporterContext context)
        {
            var data = JsonConvert.DeserializeObject<SheetFolder>(File.ReadAllText(filename));

            var texImporter = new TextureImporter();
            var textures = new List<TextureContent>();

            if (data.FolderPath != null)
                AddFolderToSpriteList(data.FolderPath, data.Filter, false, null, texImporter, context, textures);

            if (data.Folders != null)
            {
                foreach (var folder in data.Folders)
                {
                    if (folder.FolderPath != null)
                        AddFolderToSpriteList(folder.FolderPath, folder.Filter, folder.IncludeSubfolders, null, texImporter, context, textures);
                }
            }

            data.Textures = textures.ToArray();

            return data;
        }
        
        private static void AddFolderToSpriteList(string folderPath, string filter, bool includeSubfolders, string pathPrefix, TextureImporter texImporter, ContentImporterContext context, List<TextureContent> textures)
        {
            if (!Directory.Exists(folderPath))
            {
                context.Logger.LogImportantMessage($"Directory {folderPath} does not exist");
                throw new DirectoryNotFoundException(folderPath);
            }
            var files = Directory.GetFiles(folderPath, filter, includeSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            AddFilesToSpriteList(files, folderPath, texImporter, context, textures);
        }

        private static void AddFilesToSpriteList(string[] files, string pathPrefix, TextureImporter texImporter, ContentImporterContext context, List<TextureContent> textures)
        {
            context.Logger.LogMessage($"pathPrefix {pathPrefix}");

            foreach (var f in files)
            {
                string name;
                if (pathPrefix == null)
                    name = Path.GetFileNameWithoutExtension(f);
                else
                    name = GetPathWithoutExtension(Path.GetRelativePath(pathPrefix, f));

                context.AddDependency(f);
                var texture = texImporter.Import(f, context);
                texture.Name = name;
                
                textures.Add(texture);

                context.Logger.LogMessage($"Added {texture.Name} (full path: {f}) to texture list");
            }
        }

        private static string GetPathWithoutExtension(string path)
        {
            var lastDot = path.LastIndexOf('.');
            string result;
            if (lastDot >= 0)
            {
                result = path.Substring(0, lastDot);
            }
            else
                result = path;
            
            return result;
        }
    }
}

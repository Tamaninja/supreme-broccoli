using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

using osu.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osuTK;

namespace TestTest123.Game.Material
{
    public partial class MaterialStore : Container<MaterialDrawable>
    {
        private Dictionary<string, List<MaterialDrawable>> materialList = [];

        public override Vector2 Size => Vector2.One;

        public MaterialStore()
        {


        }



        public List<MaterialDrawable> GetMaterials(Model model)
        {
            if (!materialList.TryGetValue(model.Filepath, out var materials))
            {
                Logger.LogPrint("no match found");
                materials = [];
                for (var i = 0; i < model.Materials.Count; i++)
                {
                    MaterialDrawable material = new MaterialDrawable(model.Materials[i]);
                    materials.Add(material);
                    AddInternal(material);

                }
                materialList[model.Filepath] = materials;
            }

            return materials;
        }
    }
}

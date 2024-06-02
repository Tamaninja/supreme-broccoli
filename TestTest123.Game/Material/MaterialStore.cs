using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Assimp;
using osu.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osuTK;

namespace TestTest123.Game.Material
{
    public partial class MaterialStore : Container<Container<MaterialDrawable>>
    {
        private Dictionary<Model, Container<MaterialDrawable>> materialList = [];

        public override Vector2 Size => Vector2.One;

        public MaterialStore()
        {


        }



        public Container<MaterialDrawable> GetMaterials(Model model)
        {
            if (!materialList.TryGetValue(model, out var materials))
            {
                materials = new Container<MaterialDrawable>();
                for (var i = 0; i < model.Materials.Count; i++)
                {
                    materials.Add(new MaterialDrawable(model.Materials[i]));

                }
                materialList[model] = materials;
                AddInternal(materials);
            }

            return materials;
        }
    }
}

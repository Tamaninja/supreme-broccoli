using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Assimp;
using osu.Framework.Bindables;
using osu.Framework.Development;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Logging;
using osuTK;
using Vortice;

namespace TestTest123.Game
{
    public class ThreeDimensionalDrawNode : Node, IDisposable
    {
        public Material Material { get; set; }

        public List<NodeInstance> Instances { get; private set; } = [];
        public ThreeDimensionalDrawNode(Node parent) : base(parent)
        {
            
        }

        public virtual void Draw(IRenderer renderer)
        {
            foreach (ThreeDimensionalDrawNode node in Children)
            {
                node.Draw(renderer);
            }
        }


    }
}

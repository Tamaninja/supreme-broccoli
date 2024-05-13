using osuTK;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using Assimp;
using System.Collections.Generic;
using osu.Framework.Logging;

namespace TestTest123.Game
{
    public partial class Box3D : Model
    {
        public Box3D() : base("D:\\Tamaninja\\Documents\\TestTest123\\TestTest123.Resources\\Models\\Lighting Mcqueen\\LightingMcqueen.obj") 
        {

            Scale = new Vector3(100, 100, 100);
        }
    }
}

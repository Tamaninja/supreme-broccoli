using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics.Rendering.Vertices;

namespace TestTest123.Game.Vertices
{
    public interface IMeshVertex<T> : IVertex, IEquatable<T> where T : IMeshVertex<T>
    {
        public abstract static T FromMesh(MeshDrawable mesh, int index);
    }
}

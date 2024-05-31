﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using osu.Framework.Graphics.Rendering.Vertices;
using osuTK.Graphics.ES30;

namespace TestTest123.Game.Vertices
{
    public interface IMeshVertex<T> : IVertex, IEquatable<T> where T : IVertex
    {
        public abstract static T FromMesh(Mesh mesh, int index);

    }
}

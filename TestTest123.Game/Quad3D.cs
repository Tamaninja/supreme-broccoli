
using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Primitives;
using osuTK;

namespace TestTest123.Game
{
    internal class Quad3D
    {
        private Vector3 pos;
        private Quad quad;

        public Quad3D(Vector3 pos, Vector2 size)
        {
            this.pos = pos;
            quad = new Quad(0,0, size.X, size.Y);
            Vector3.TransformPerspective(pos, Matrix4.CreatePerspectiveFieldOfView(80, 16 / 9, 20, 2000));
        }





        public Quad GetQuad() {
            return quad;
        }


    }
}

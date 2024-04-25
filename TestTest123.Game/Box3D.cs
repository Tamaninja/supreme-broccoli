using osuTK;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;

namespace TestTest123.Game
{
    public partial class Box3D : Model
    {
        public Box3D(Vector3 pos)
            : base(pos)
        {

            Colour = Color4.AliceBlue.Opacity(0.5f);
        }

        protected override void Init()
        {
            Vector3 v1 = new Vector3(-0.5f, -0.5f, -0.5f);  // Bottom-left-front corner
            Vector3 v2 = new Vector3(0.5f, -0.5f, -0.5f);  // Bottom-right-front corner
            Vector3 v3 = new Vector3(0.5f, -0.5f, 0.5f);  // Bottom-right-back corner
            Vector3 v4 = new Vector3(-0.5f, -0.5f, 0.5f);  // Bottom-left-back corner
            Vector3 v5 = new Vector3(-0.5f, 0.5f, -0.5f);  // Top-left-front corner
            Vector3 v6 = new Vector3(0.5f, 0.5f, -0.5f);  // Top-right-front corner
            Vector3 v7 = new Vector3(0.5f, 0.5f, 0.5f);  // Top-right-back corner
            Vector3 v8 = new Vector3(-0.5f, 0.5f, 0.5f);  // Top-left-back corner

            int[][] faceIndices = [
                [0, 1, 2, 3],
                [4, 5, 6, 7],
                [0, 3, 7, 4],
                [1, 5, 6, 2],
                [0, 1, 5, 4],
                [2, 3, 7, 6]];




            SetVertices([v1, v2, v3, v4, v5, v6, v7, v8]);
            SetIndices(faceIndices);
        }
    }
}


using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace TestTest123.Game
{
    public partial class Stage : Container<Model>
    {
        private List<Camera> cameras;
        private List<Vector3[]> vertices = new List<Vector3[]>();
        public Stage() {
            cameras = new List<Camera>();

            RelativeSizeAxes = Axes.Both;

        }

        public override void Add(Model model)
        {
            base.Add(model);

            vertices.Add(model.GetVertices());
        }
        public void Add(Camera camera)
        {
            base.Add(camera);

            cameras.Add(camera);
        }

        public List<Vector3[]> GetVertices(Camera camera)
        {
            foreach (Model v in Children) {
                vertices.Add(v.GetVertices());
            }
            return vertices;
        }

    }
}


using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace TestTest123.Game
{
    public partial class Stage : Container<Model>
    {
        private List<Camera> cameras = new List<Camera>();
        public Stage() {

            RelativeSizeAxes = Axes.Both;

        }
        public void Add(Camera camera)
        {
            base.Add(camera);
            cameras.Add(camera);
        }
    }
}

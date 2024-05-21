
using osu.Framework.Logging;
using osuTK;

namespace TestTest123.Game
{
    public partial class Box3D : ModelDrawable
    {
        public Box3D(Camera camera) : base ("D:\\Tamaninja\\Documents\\TestTest123\\TestTest123.Resources\\Models\\Trashcan_Small1.fbx", camera)
        {
            Logger.LogPrint("tesxt");

            Scale3D = (new Vector3(100));

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace TestTest123.Game
{
    public class NodeInstance

    {
        

        public NodeInstance(ModelDrawNode source)
        {
            foreach (var mesh in source.Meshes)
            {
                mesh.Instances.Add(this);
            }

            initializeBindables();
        }

        public Material Material { get; set; } = new Material();
        public Bindable<Matrix4> LocalMatrix { get; set; } = new Bindable<Matrix4>(Matrix4.Identity);
        public Bindable<Vector3> Forward { get; set; } = new Bindable<Vector3>(Vector3.UnitZ);
        public Bindable<Vector3> Rotation { get; set; } = new Bindable<Vector3>(Vector3.Zero);
        public Bindable<Vector3> Position { get; set; } = new Bindable<Vector3>(Vector3.Zero);
        public Bindable<Vector3> Scale { get; set; } = new Bindable<Vector3>(Vector3.One);

        public Colour4 Colour { get; set; } = Colour4.White;

        private void initializeBindables()
        {
            Forward.BindValueChanged(t => UpdateMatrix());
            Position.BindValueChanged(t => UpdateMatrix());
            Rotation.BindValueChanged(t => UpdateMatrix());
            Scale.BindValueChanged(t => UpdateMatrix());
        }


        public virtual void UpdateMatrix()
        {
            LocalMatrix.Value = (
                Matrix4.CreateScale(Scale.Value)
                * createRotationMatrix()
                * Matrix4.CreateTranslation(Position.Value));
        }
        private Matrix4 createRotationMatrix()
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(Rotation.Value.X);
            Matrix4 rotationY = Matrix4.CreateRotationY(Rotation.Value.Y);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(Rotation.Value.Z);

            return (rotationX * rotationY * rotationZ);
        }

    }
}

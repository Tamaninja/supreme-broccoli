using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    public class ThreeDimensionalDrawNode : IThreeDimensional, IDisposable
    {
        protected IRenderer Renderer;
        private bool disposedValue;

        public String Name { get; set; }
        public IShader TextureShader { get; set; }
        public Bindable<Matrix4> LocalMatrix { get; set; } = new Bindable<Matrix4>(Matrix4.Identity);
        public Bindable<Vector3> Forward { get; set; } = new Bindable<Vector3>(Vector3.UnitZ);
        public Bindable<Vector3> Rotation { get; set; } = new Bindable<Vector3>(Vector3.Zero);
        public Bindable<Vector3> Position { get; set; } = new Bindable<Vector3>(Vector3.Zero);
        public Bindable<Vector3> Scale { get; set; } = new Bindable<Vector3>(Vector3.One);

        public Scene Scene { get;}

        public List<ThreeDimensionalDrawNode> Children { get; } = [];

        public LargeTextureStore TextureStore { get; }


        public ThreeDimensionalDrawNode(Scene scene, IRenderer renderer, LargeTextureStore textureStore)
        {
            TextureStore = textureStore;
            Renderer = renderer;
            Scene = scene;

            Forward.BindValueChanged(t => UpdateMatrix());
            Position.BindValueChanged(t => UpdateMatrix());
            Rotation.BindValueChanged(t => UpdateMatrix());
            Scale.BindValueChanged(t => UpdateMatrix());


        }

        public ThreeDimensionalDrawNode(ThreeDimensionalDrawNode parent)
        {
            
            Parent = parent;
            Scene = parent.Scene;
            Renderer = parent.Renderer;
            TextureStore = parent.TextureStore;


            Forward.BindValueChanged(t => UpdateMatrix());
            Position.BindValueChanged(t => UpdateMatrix());
            Rotation.BindValueChanged(t => UpdateMatrix());
            Scale.BindValueChanged(t => UpdateMatrix());
        }

        public virtual void Draw(IRenderer renderer)
        {
            foreach (ThreeDimensionalDrawNode node in Children)
            {
                node.Draw(renderer);
            }
        }

        public ThreeDimensionalDrawNode Parent { get; set; }
        public void AddSubNode(ThreeDimensionalDrawNode node)
        {
            
            node.Parent = this;
            Children.Add(node);
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
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)

                    foreach (var child in Children)
                    {
                        child.Dispose(disposing);
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ThreeDimensionalDrawNode()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

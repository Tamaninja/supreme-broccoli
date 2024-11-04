using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Rendering;

namespace TestTest123.Game
{
    public class Node
    {
        public SceneNode Scene { get; set; }
        public Container Visualization { get; } = [];
        public List<Node> Children { get; } = [];
        private bool disposedValue;

        public Bindable<string> Name = new();
        public void AddSubNode(Node node)
        {

            Children.Add(node);
            Visualization.Add(node.Visualization);
        }

        public Node(Scene scene) //root node
        {
            Name.BindValueChanged(t => Visualization.Name = t.NewValue);
        }
        public Node(SceneNode scene)
        {
            Scene = scene;
            scene.AddSubNode(this);

            Name.BindValueChanged(t => Visualization.Name = t.NewValue);

        }
        public Node(Node parent)
        {
            Scene = parent.Scene;

            Name.BindValueChanged(t => Visualization.Name = t.NewValue);

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

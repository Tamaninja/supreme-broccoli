
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osu.Framework.Graphics.Sprites;
using System.Collections.Generic;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Rendering.Vertices;
using System;
using osuTK.Graphics;
using osuTK.Input;
using Assimp;
using Assimp.Unmanaged;
using osu.Framework.Logging;



namespace TestTest123.Game
{
    public partial class Camera : Model
    {
        private Stage stage;

        private float yaw;
        private float pitch;

        private SpriteText spriteText;

        private float yFov = 50;
        public float FarPlane { get; }
        private Assimp.Camera camera;

        public Camera(Stage stage, SpriteText debug)
        {
            camera = new Assimp.Camera();


            stage.Add(this);
            this.stage = stage;

            FarPlane = 5000f;

            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            pitch = 0;
            yaw = 0;

        }

        public void HandleMouseEvent(MouseMoveEvent e)
        {
            Vector2 delta = e.Delta * 0.25f;

            yaw += delta.X;
            pitch += -delta.Y;

            pitch = MathHelper.Clamp(pitch, -89, 89);

            UpdateViewDirection();
        }

        public void MoveBy(Vector3 offset)
        {


            Position += offset;
        }


        public IReadOnlyList<Model> GetModels()
        {
            return (stage.Children);
        }

        public void UpdateViewDirection()
        {


            Vector3D temp = camera.Direction; 

            temp.X  = MathF.Cos(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));
            temp.Y = MathF.Sin(MathHelper.DegreesToRadians(pitch));
            temp.Z = MathF.Sin(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));

            temp.Normalize();
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
           HandleMouseEvent(e);
           

           return true;
        }


        public Matrix4 GetViewMatrix()
        {
            
            Vector3 vec = Position;
            Vector3 temp = Vector3.Normalize(Position - a(camera.Direction));

            return Matrix4.LookAt(vec, vec + temp, Vector3.UnitY);
        }
        private  Vector3 a(Vector3D value)
        {
            return (new Vector3(value.X, value.Y, value.Z));
        }
        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(yFov), 16 / 9, 1, 50);
        }





        protected override bool OnKeyDown(KeyDownEvent e)
        {
            switch (e.Key)
            {

                case Key.Space:
                    MoveBy(new Vector3(0, 100, 0));
                    return true;

                case Key.LShift:
                    MoveBy(new Vector3(0, -100, 0));
                    return true;

                case Key.A:
                    MoveBy(new Vector3(100f, 0, 0));
                    return true;

                case Key.D:
                    MoveBy(new Vector3(-100f, 0, 0));
                    return true;

                case Key.S:
                    MoveBy(new Vector3(0, 0, -100f));
                    return true;

                case Key.W:
                    MoveBy(new Vector3(0, 0, 100f));
                    return true;
            }
            

            return base.OnKeyDown(e);
        }


        public override DrawColourInfo DrawColourInfo => new DrawColourInfo(Color4.White, base.DrawColourInfo.Blending);



        protected override DrawNode CreateDrawNode() => new CameraDrawNode(this);


        protected class CameraDrawNode : TexturedShaderDrawNode
        {
            private Camera camera;
            private IRenderer renderer;
            public CameraDrawNode(Camera source) : base(source)
            {
                camera = source;
                
            }


            private void drawMeshes(List<Mesh> meshes, IRenderer renderer)
            {
                foreach (Mesh mesh in meshes)
                {
                    List<Vector3D> vertices = mesh.Vertices;
                    IVertexBatch<TexturedVertex3D> batch = renderer.CreateLinearBatch<TexturedVertex3D>(mesh.FaceCount, 1, PrimitiveTopology.Triangles);
                    foreach (Face face in mesh.Faces)
                    {
                        
                        foreach (int index in face.Indices)
                        {
                            Vector3D vec = vertices[index];
                            batch.AddAction(new TexturedVertex3D()
                            {
                                Position = new Vector3(vec.X, vec.Y, vec.Z),
                                Colour = DrawColourInfo.Colour,
                                TexturePosition = Vector2.Zero
                            });
                        }

                    }
                }

            }
            protected override void Draw(IRenderer renderer)
            {
                BindTextureShader(renderer);
                renderer.PushProjectionMatrix(camera.GetViewMatrix() * camera.GetProjectionMatrix());


                foreach (Model model in camera.GetModels())
                {
                    List<Mesh> meshes = model.GetMeshes();
                    if (meshes == null) continue;



                    renderer.PushLocalMatrix(model.GetMatrix());

                        drawMeshes(meshes, renderer);

                    renderer.PopLocalMatrix();
                }

                renderer.PopProjectionMatrix();
                UnbindTextureShader(renderer);

            }
        }
    }
}


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

        private SpriteText spriteText;
        private Vector3 viewDirecion;

        private float yFov = 50;
        public float FarPlane { get; }

        public Camera(Stage stage, SpriteText debug)
        {

            stage.Add(this);
            this.stage = stage;
            spriteText = debug;
            FarPlane = 5000f;

            RelativeSizeAxes = Axes.Both;
            RelativePositionAxes = Axes.Both;

            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

        }

        public void HandleMouseEvent(MouseMoveEvent e)
        {
            Vector2 delta = e.Delta * 0.25f;

            Yaw += delta.X;
            Pitch += -delta.Y;

            Pitch = MathHelper.Clamp(Pitch, -89, 89);
            spriteText.Text = (Yaw +"/"+ Pitch);
            UpdateViewDirection();
        }
        public Matrix4 GetRotationMatrix()
        {
            Matrix4 yaw = Matrix4.CreateRotationX(Yaw);
            Matrix4 pitch = Matrix4.CreateRotationY(Pitch);
            Matrix4 roll = Matrix4.CreateRotationZ(Roll);

            return (yaw * pitch * roll);
        }
        public void MoveBy(Vector3 offset)
        {
            
            Vector3 temp = Vector3.TransformPosition(offset.Normalized(), GetRotationMatrix());

            
            Logger.LogPrint(temp.ToString());
            Position += temp;
        }


        public IReadOnlyList<Model> GetModels()
        {
            return (stage.Children);
        }

        public void UpdateViewDirection()
        {

            viewDirecion.X  = MathF.Cos(MathHelper.DegreesToRadians(Yaw)) * MathF.Cos(MathHelper.DegreesToRadians(Pitch));
            viewDirecion.Y = MathF.Sin(MathHelper.DegreesToRadians(Pitch));
            viewDirecion.Z = MathF.Sin(MathHelper.DegreesToRadians(Yaw)) * MathF.Cos(MathHelper.DegreesToRadians(Pitch));

            viewDirecion.Normalize();
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
           HandleMouseEvent(e);
           

           return true;
        }


        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + viewDirecion, Vector3.UnitY);
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

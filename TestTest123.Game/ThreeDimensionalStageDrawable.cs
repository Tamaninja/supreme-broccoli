﻿
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Assimp;
using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Shaders;
using osu.Framework.Graphics.Shaders.Types;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osuTK;
using osuTK.Graphics;
using osuTK.Graphics.OpenGL;
using TestTest123.Game.Vertices;


namespace TestTest123.Game
{
    public partial class ThreeDimensionalStageDrawable : Container
    {
        public Dictionary<Type, Dictionary<string, Material>> Materials = new Dictionary<Type, Dictionary<string, Material>>();
        public Camera Camera;
        private TextureStore textureStore;
        public ThreeDimensionalStageDrawable()
        {
            RelativeSizeAxes = Axes.Both;
            Colour = Color4.AliceBlue.Opacity(0f);
        }


        public Material GetMaterial(Type type, Assimp.Material assimp)
        {
            Material material;
            if (!Materials.TryGetValue(type, out var materialList)) //type not already in dictionary
            {
                material = new Material(this, assimp);
                materialList = new Dictionary<string, Material>
                {
                    { assimp.Name, material}
                };
                Materials.Add(type, materialList);
                AddInternal(material);
                return (material);
            }
            if (!materialList.TryGetValue(assimp.Name, out material)) //new material, exisiting object
            {
                material = new Material(this, assimp);
                materialList.Add(assimp.Name, material);
                AddInternal(material);
            }
            return (material);

        }


        public Texture GetTextureBypassAtlas(string key)
        {
            return (textureStore.Get(key));
        }
        [BackgroundDependencyLoader]
        private void load(IRenderer renderer, GameHost host, osu.Framework.Game game)
        {

            IResourceStore<TextureUpload> textureLoaderStore = null!;
            textureLoaderStore = host.CreateTextureLoaderStore(new NamespacedResourceStore<byte[]>(game.Resources, @"Textures"));
            textureStore = new TextureStore(renderer, textureLoaderStore, false, TextureFilteringMode.Linear, true);

            Camera = new Camera();
            AddInternal(Camera);

            for (int i = 0;  i < 33; i++)
            {
                Box3D box3d = new Box3D(this);
                AddInternal(box3d);

                box3d.Colour = Color4.DarkOrange;
                box3d.Position3D = new Vector3(new Random().Next(1,24), new Random().Next(1, 4), 100);
                box3d.Delay(i * 500).MoveToZ(0, 50000, Easing.None).Then().Expire();
            }


        }

        protected override DrawNode CreateDrawNode()
        {
            return new StageDrawNode(this);
        }


        protected class StageDrawNode(ThreeDimensionalStageDrawable source) : CompositeDrawableDrawNode(source)
        {
            private Matrix4 vpMatrix = Matrix4.Identity;
            protected new ThreeDimensionalStageDrawable Source => (ThreeDimensionalStageDrawable)base.Source;

            public override void ApplyState()
            {
                base.ApplyState();

                vpMatrix = Source.Camera.VPMatrix;

            }

            protected override void Draw(IRenderer renderer)
            {
                renderer.PushDepthInfo(DepthInfo.Default);
                renderer.PushProjectionMatrix(vpMatrix);
                    base.Draw(renderer);
                renderer.PopProjectionMatrix();
                renderer.PopDepthInfo();
            }
        }
    }
}

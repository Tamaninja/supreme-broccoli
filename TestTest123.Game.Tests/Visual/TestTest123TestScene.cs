using osu.Framework.Testing;

namespace TestTest123.Game.Tests.Visual
{
    public abstract partial class TestTest123TestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new TestTest123TestSceneTestRunner();

        private partial class TestTest123TestSceneTestRunner : TestTest123GameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}

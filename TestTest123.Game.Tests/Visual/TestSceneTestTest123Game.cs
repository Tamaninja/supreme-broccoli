using osu.Framework.Allocation;
using osu.Framework.Platform;
using NUnit.Framework;

namespace TestTest123.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneTestTest123Game : TestTest123TestScene
    {
        // Add visual tests to ensure correct behaviour of your game: https://github.com/ppy/osu-framework/wiki/Development-and-Testing
        // You can make changes to classes associated with the tests and they will recompile and update immediately.

        private TestTest123Game game;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            game = new TestTest123Game();
            game.SetHost(host);

            AddGame(game);
        }
    }
}

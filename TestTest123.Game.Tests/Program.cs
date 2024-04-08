using osu.Framework;
using osu.Framework.Platform;

namespace TestTest123.Game.Tests
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost("visual-tests"))
            using (var game = new TestTest123TestBrowser())
                host.Run(game);
        }
    }
}

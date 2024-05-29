using osu.Framework.Platform;
using osu.Framework;
using TestTest123.Game;

namespace TestTest123.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost(@"TestTest123"))
            using (osu.Framework.Game game = new TestTest123Game())
             host.Run(game);
        }
    }
}

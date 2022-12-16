using Xunit.Abstractions;

namespace Basics.Async
{
    public class Worker
    {
        private readonly ITestOutputHelper _console;

        public Worker(ITestOutputHelper console)
        {
            _console = console;
        }

        public async Task<bool> MineOreAsync()
        {
            // takes 10s to mine ore
            _console.WriteLine("start mining ore...");
            await Task.Delay(10 * 1000);
            _console.WriteLine("ore has been mined");
            return true;
        }

        public async Task<bool> CutTreesAsync()
        {
            // takes 1s to cut trees
            _console.WriteLine("start cutting tree...");
            await Task.Delay(1000);
            _console.WriteLine("tree has been cut");
            return true;
        }
    }
}

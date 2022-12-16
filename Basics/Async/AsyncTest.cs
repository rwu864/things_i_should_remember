using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Basics.Async
{
    public class AsyncTest
    {
        private readonly ITestOutputHelper _console;

        public AsyncTest(ITestOutputHelper console)
        {
            _console = console;
        }

        [Fact]
        public async Task Test_NonBlocking()
        {
            var worker = new Worker(_console);

            // mineOreTask is not awaited
            var mineOreTask = worker.MineOreAsync(); 

            var cutTreetasks = new List<Task>();
            for(int i = 0; i < 5; i++)
            {
                cutTreetasks.Add(worker.CutTreesAsync());
            }

            await Task.WhenAll(cutTreetasks);
            var result = await mineOreTask;
            Assert.True(result);

            // console result: 
            //start mining ore...
            //start cutting tree...
            //start cutting tree...
            //start cutting tree...
            //start cutting tree...
            //start cutting tree...
            //tree has been cut
            //tree has been cut
            //tree has been cut
            //tree has been cut
            //tree has been cut
            //ore has been mined

            // this takes a total of 10s because the long task starts
            // but does not block the smaller tasks 
        }

        [Fact]
        public async Task Test_Blocking()
        {
            var worker = new Worker(_console);

            // mine ore task is awaited right away 
            var result = await worker.MineOreAsync();

            for (int i = 0; i < 5; i++)
            {
                await worker.CutTreesAsync();
            }

            Assert.True(result);

            // console output: 

            //start mining ore...
            //ore has been mined
            //start cutting tree...
            //tree has been cut
            //start cutting tree...
            //tree has been cut
            //start cutting tree...
            //tree has been cut
            //start cutting tree...
            //tree has been cut
            //start cutting tree...
            //tree has been cut...

            // this will take about 15s since we are waiting for the result of each one 
        }
    }

   
}

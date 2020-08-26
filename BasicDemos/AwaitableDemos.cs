using AsyncAwait.DemoComponents;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AsyncAwaitDemo.BasicDemos
{
    public class AwaitableDemos
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public AwaitableDemos(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));
        }

        [Fact(DisplayName = "Demonstration of custom awaitable")]
        public async Task CustomAwaitable()
        {
            var completedAwaitable = new CustomAwaitable(
                _testOutputHelper.WriteLine,
                isCompleted: true);
            await completedAwaitable;

            var incompletedAwaitable = new CustomAwaitable(
                _testOutputHelper.WriteLine,
                isCompleted: false);
            await incompletedAwaitable;
        }
    }
}

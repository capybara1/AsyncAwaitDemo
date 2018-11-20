using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait.DemoComponents
{
    public class LegacyServiceWrapper
    {
        // Note: the implementation is intentionally limited to files with a maximum size of 1024
        // for the sake of simplicity

        public Task<string> ReadFileContentAsync(string path, Encoding endcoding)
        {
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            var tcs = new TaskCompletionSource<string>();

            var buffer = new byte[1024];
            stream.BeginRead(
                buffer,
                0,
                1024,
                asyncResult =>
                {
                    try
                    {
                        var numberOfBytesRead = stream.EndRead(asyncResult);
                        Array.Resize(ref buffer, numberOfBytesRead);
                        var taskResult = endcoding.GetString(buffer);

                        tcs.SetResult(taskResult);
                    }
                    catch (Exception exception)
                    {
                        tcs.SetException(exception);
                    }
                },
                null);
            
            var result = tcs.Task;

            return result;
        }
    }
}

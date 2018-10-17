using System.Threading.Tasks;

namespace AsyncAwait.ServiceContracts
{
    public interface ITestService
    {
        Task<string> ProvideTestValueAsync();
    }
}
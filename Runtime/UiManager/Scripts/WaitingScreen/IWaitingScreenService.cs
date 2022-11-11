using Demos.Scripts;

namespace UIManagement.WaitingScreen
{
    public interface IWaitingScreenService : IService
    {
        bool IsOpen { get; }
        void Open(WaitingScreenConfiguration configuration);
        
        void Close();
    }
}
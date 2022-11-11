using Demos.Scripts;

namespace Redpenguin.UiManager.TouchLocker
{
    public interface ITouchLocker : IService
    {
        void Lock();
        void Unlock();
        bool IsLocked { get; }
    }
}
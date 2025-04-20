using System;

namespace DTech.UGraph.Core
{
    public interface IUGraph : IDisposable
    {
        bool IsEnabled { get; }
        
        void Enable();
        void Process();
        void Disable();
        void Reset();
    }
}

using System;

namespace DTech.UGraph.Core
{
	public interface IUNode : IDisposable
	{
        string Id { get; }
		string Name { get; }
		
		void Enter();
		Status Execute();
		void Exit();
	}
}
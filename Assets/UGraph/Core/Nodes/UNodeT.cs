using DTech.UGraph.Core.Extensions;

namespace DTech.UGraph.Core.Runtime
{
	public abstract class UNodeT<TConfig> : IUNode
		where TConfig : UNodeConfig
	{
		public string Id => Config.Id;
		public string Name => Config.name;
		
		protected TConfig Config { get; }

		public UNodeT(TConfig config)
		{
			Config = config.ThrowIfNull();
		}

		public abstract void Enter();
		public abstract Status Execute();
		public abstract void Exit();
		public virtual void Dispose(){}
	}
}
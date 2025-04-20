namespace DTech.UGraph.Core
{
	public interface IUGraphBuilder<in TConfig>
		where TConfig : UGraphConfig
	{
		IUGraphBuilder<TConfig> SetConfig(TConfig value);
		IUGraph Build();
	}
}
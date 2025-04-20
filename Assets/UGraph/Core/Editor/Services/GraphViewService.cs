using System.Reflection;

namespace DTech.UGraph.Core.Editor
{
	internal static class GraphViewService
	{
		private static readonly MethodInfo _createViewMethod;
		
		static GraphViewService()
		{
			_createViewMethod = typeof(GraphViewService).GetMethod(nameof(CreateView),
				BindingFlags.Static | BindingFlags.NonPublic);
		}

		public static UGraphConfigView GetView(UGraphConfig config)
		{
			MethodInfo methodInfo = _createViewMethod.MakeGenericMethod(new[]
			{
				config.GetType(),
			});

			object result = methodInfo.Invoke(null, new[]
			{
				config,
			});

			return result as UGraphConfigView;
		}

		private static UGraphConfigViewT<TConfig> CreateView<TConfig>(TConfig config)
			where TConfig : UGraphConfig =>
			new(config);
	}
}
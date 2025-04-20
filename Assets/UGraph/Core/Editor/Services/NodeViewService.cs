using System;
using System.Linq;

namespace DTech.UGraph.Core.Editor
{
	public static class NodeViewService
	{
		public static UNodeConfigView GetView(UNodeConfig config)
		{
			if (config == null)
				throw new ArgumentNullException(nameof(config));

			Type configType = config.GetType();
			Type genericBaseType = typeof(UNodeConfigViewT<>);
			Type bestMatch = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(a => a.GetTypes())
				.Where(t => !t.IsAbstract && !t.IsInterface)
				.Where(t => typeof(UNodeConfigView).IsAssignableFrom(t))
				.Where(t =>
				{
					if (!t.IsGenericType &&
						t.BaseType != null &&
						t.BaseType.IsGenericType)
					{
						Type baseGeneric = t.BaseType.GetGenericTypeDefinition();
						if (baseGeneric == genericBaseType)
						{
							Type genericArg = t.BaseType.GetGenericArguments()[0];
							return genericArg.IsAssignableFrom(configType);
						}
					}

					return false;
				})
				.OrderByDescending(t =>
						t.BaseType.GetGenericArguments()[0] == configType ? 2 : 1
				)
				.FirstOrDefault();

			if (bestMatch != null)
			{
				return (UNodeConfigView)Activator.CreateInstance(bestMatch, config);
			}

			Type genericType = genericBaseType.MakeGenericType(configType);
			return (UNodeConfigView)Activator.CreateInstance(genericType, config);
		}
	}
}
using System;
using System.Collections.Generic;
using DTech.UGraph.Core.Extensions;
using UnityEditor.Experimental.GraphView;

namespace DTech.UGraph.Core.Editor
{
	public class UNodeConfigViewT<TConfig> : UNodeConfigView
		where TConfig : UNodeConfig
	{
		public override UNodeConfig ConfigBase => Config;

		protected TConfig Config { get; }

		public UNodeConfigViewT(TConfig config)
		{
			Config = config.ThrowIfNull();
		}
		
		public override IReadOnlyList<Edge> GetEdges() => Array.Empty<Edge>();
	}
}
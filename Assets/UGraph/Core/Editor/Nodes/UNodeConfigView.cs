using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

namespace DTech.UGraph.Core.Editor
{
	public abstract class UNodeConfigView : Node, IDisposable
	{
		public abstract UNodeConfig ConfigBase { get; }
		
		public abstract IReadOnlyList<Edge> GetEdges();
		public virtual void Dispose() { }
	}
}
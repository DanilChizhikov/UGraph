using System;
using UnityEditor.Experimental.GraphView;

namespace DTech.UGraph.Core.Editor
{
	public abstract class UGraphConfigView : GraphView, IDisposable
	{
		public virtual void Dispose() { }
	}
}
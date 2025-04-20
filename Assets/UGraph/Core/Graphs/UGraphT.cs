using System;
using System.Collections.Generic;
using DTech.UGraph.Core.Extensions;
using UnityEngine;

namespace DTech.UGraph.Core.Runtime
{
	public abstract class UGraphT<TNode> : IUGraph
		where TNode : IUNode
	{
		private readonly float _updateInterval;
		
		public bool IsEnabled { get; private set; }
		
		protected TNode EnterNode { get; }
		protected IReadOnlyList<TNode> Nodes { get; }
		protected bool IsDisposed { get; private set; }
		
		private float _lastUpdateTime;

		public UGraphT(TNode enterNode, IEnumerable<TNode> nodes, float updateInterval)
		{
			EnterNode = enterNode;
			Nodes = new List<TNode>(nodes.ThrowIfNull());
			_updateInterval = Mathf.Max(updateInterval, 0f);
			IsEnabled = false;
			IsDisposed = false;
			_lastUpdateTime = 0f;
		}
		
		public void Enable()
		{
			ThrowIfDisposed();
			if (IsEnabled)
			{
				return;
			}
			
			EnableProcess();
			IsEnabled = true;
		}

		public void Process()
		{
			ThrowIfDisposed();
			if (IsEnabled && (Time.time - _lastUpdateTime) > _updateInterval)
			{
				UpdateProcess();
				_lastUpdateTime = Time.time;
			}
		}

		public void Disable()
		{
			ThrowIfDisposed();
			if (!IsEnabled)
			{
				return;
			}

			DisableProcess();
			IsEnabled = false;
		}

		public void Reset()
		{
			ThrowIfDisposed();
			bool cachedEnabled = IsEnabled;
			Disable();
			ResetProcess();
			if (cachedEnabled)
			{
				Enable();
			}
		}

		public void Dispose()
		{
			if (IsDisposed)
			{
				return;
			}
			
			Disable();
			InternalDispose();
			EnterNode.Dispose();
			for (int i = 0; i < Nodes.Count; i++)
			{
				Nodes[i].Dispose();
			}
			
			IsDisposed = true;
		}

		protected void ThrowIfDisposed()
		{
			if (IsDisposed)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
		}
		
		protected virtual void EnableProcess() {}
		protected virtual void UpdateProcess() {}
		protected virtual void DisableProcess() {}
		protected virtual void ResetProcess() {}
		protected virtual void InternalDispose() {}
	}
}
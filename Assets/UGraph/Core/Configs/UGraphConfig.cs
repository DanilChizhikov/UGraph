using System.Collections.Generic;
using UnityEngine;

namespace DTech.UGraph.Core
{
	public abstract partial class UGraphConfig : ScriptableObject
	{
		[SerializeField, Min(0f)] private float _updateInterval = 0.5f;
		[SerializeField] private List<UNodeConfig> _nodes = new();
		[SerializeField, HideInInspector] private UNodeConfig _enterNode = default;
		[SerializeField, HideInInspector] private List<ConnectionInfo> _connections = new();

		public UNodeConfig EnterNode
		{
			get
			{
				if (_enterNode == null)
				{
					if (_nodes.Count > 0)
					{
						_enterNode = _nodes[0];
						_nodes.RemoveAt(0);
					}
				}
				
				return _enterNode;
			}

			set
			{
				if (_enterNode != null && !_nodes.Contains(_enterNode))
				{
					_nodes.Add(_enterNode);
				}

				_enterNode = value;
				_nodes.Remove(_enterNode);
			}
		}
		public IReadOnlyList<UNodeConfig> Nodes => _nodes;
		public float UpdateInterval => _updateInterval;
		public IReadOnlyList<ConnectionInfo> Connections => _connections;
	}
}
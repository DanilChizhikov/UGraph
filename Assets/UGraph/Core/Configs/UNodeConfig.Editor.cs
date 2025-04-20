#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace DTech.UGraph.Core
{
	public abstract partial class UNodeConfig
	{
		[field: SerializeField] public string Comment { get; private set; } = string.Empty;
		[field: SerializeField] public Vector2 Position { get; set; } = Vector2.zero;

		public void ConnectTo(string nodeId)
		{
			if (!HasConnectTo(nodeId))
			{
				_connections.Add(new ConnectionInfo(Id, nodeId));
				MarkAsDirty();
			}
		}

		public void DisconnectFrom(string nodeId)
		{
			int connectionIndex = ConnectionIndexOf(nodeId);
			if (connectionIndex >= 0)
			{
				_connections.RemoveAt(connectionIndex);
				MarkAsDirty();
			}
		}

		public void MoveConnectionTo(int index, string nodeId)
		{
			int connectionIndex = ConnectionIndexOf(nodeId);
			if (connectionIndex >= 0)
			{
				ConnectionInfo connection = _connections[connectionIndex];
				_connections.RemoveAt(connectionIndex);
				_connections.Insert(index, connection);
				MarkAsDirty();
			}
		}

		private bool HasConnectTo(string nodeId) => ConnectionIndexOf(nodeId) >= 0;

		private int ConnectionIndexOf(string nodeId)
		{
			for (int i = 0; i < Connections.Count; i++)
			{
				ConnectionInfo connection = Connections[i];
				if (connection.TargetNodeId.Equals(nodeId))
				{
					return i;
				}
			}
			
			return -1;
		}

		private void MarkAsDirty() => EditorUtility.SetDirty(this);
	}
}
#endif
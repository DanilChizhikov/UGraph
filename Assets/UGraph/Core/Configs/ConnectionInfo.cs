using System;
using UnityEngine;

namespace DTech.UGraph.Core
{
	[Serializable]
	public sealed class ConnectionInfo : IEquatable<ConnectionInfo>
	{
		[field: SerializeField, ReadOnly] public string SourceNodeId { get; private set; }
		[field: SerializeField, ReadOnly] public string TargetNodeId { get; private set; }

		public ConnectionInfo(string sourceNodeId, string targetNodeId)
		{
			SourceNodeId = sourceNodeId;
			TargetNodeId = targetNodeId;
		}

		public bool Equals(ConnectionInfo other) => SourceNodeId == other.SourceNodeId && TargetNodeId == other.TargetNodeId;

		public override bool Equals(object obj) => obj is ConnectionInfo other && Equals(other);

		public override int GetHashCode() => HashCode.Combine(SourceNodeId, TargetNodeId);
	}
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DTech.UGraph.Core
{
	public abstract partial class UNodeConfig : ScriptableObject
	{
		[SerializeField, HideInInspector] private List<ConnectionInfo> _connections = new();
		
		[field: SerializeField, ReadOnly] public string Id { get; private set; } = Guid.NewGuid().ToString();

		public IReadOnlyList<ConnectionInfo> Connections => _connections;
	}
}
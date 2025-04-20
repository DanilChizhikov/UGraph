using System;
using UnityEngine;

namespace DTech.UGraph.Core
{
	public abstract partial class UNodeConfig : ScriptableObject
	{
		[field: SerializeField, ReadOnly] public string Id { get; private set; } = Guid.NewGuid().ToString();
	}
}
using UnityEngine;

namespace DTech.UGraph.Core
{
	public abstract partial class UNodeConfig
	{
		[field: SerializeField] public string Comment { get; private set; } = string.Empty;
		[field: SerializeField] public Vector2 Position { get; set; } = Vector2.zero;
	}
}
#if UNITY_EDITOR
using UnityEditor;

namespace DTech.UGraph.Core
{
	public abstract partial class UGraphConfig
	{
		public void CreateNode<T>()
			where T : UNodeConfig
		{
			var nodeConfig = CreateInstance<T>();
			_nodes.Add(nodeConfig);
			AssetDatabase.AddObjectToAsset(nodeConfig, this);
			MarkAsDirty();
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		public bool HasNode(string nodeId)
		{
			for (int i = 0; i < _nodes.Count; i++)
			{
				UNodeConfig node = _nodes[i];
				if (node != null && node.Id == nodeId)
				{
					return true;
				}
			}

			return false;
		}

		public bool TryGetNode(string nodeId, out UNodeConfig node)
		{
			for (int i = 0; i < _nodes.Count; i++)
			{
				node = _nodes[i];
				if (node != null && node.Id == nodeId)
				{
					return true;
				}
			}

			node = null;
			return false;
		}

		public void RemoveNode(string nodeId)
		{
			if (TryGetNode(nodeId, out UNodeConfig node))
			{
				_nodes.Remove(node);
				MarkAsDirty();
			}
		}

		private void MarkAsDirty() => EditorUtility.SetDirty(this);
	}
}
#endif
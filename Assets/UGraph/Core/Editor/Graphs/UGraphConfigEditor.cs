using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace DTech.UGraph.Core.Editor
{
	[CustomEditor(typeof(UGraphConfig), true)]
	public class UGraphConfigEditor : UnityEditor.Editor
	{
		private UGraphConfig _graph;
        
		[OnOpenAsset]
		public static bool OpenGraph(int instanceId, int index)
		{
			Object asset = EditorUtility.InstanceIDToObject(instanceId);
			if (asset is UGraphConfig graph)
			{
				ShowGraph(graph);
				return true;
			}

			return false;
		}

		public override void OnInspectorGUI()
		{
			DrawShowGraphButton();
			DrawDefaultInspector();
		}
        
		protected virtual void DrawShowGraphButton()
		{
			if (GUILayout.Button("Show Graph"))
			{
				ShowGraph(_graph);
			}
		}

		private static void ShowGraph(UGraphConfig graph) => GraphWindowService.Show(graph);

		protected virtual void OnEnable()
		{
			if (target is UGraphConfig graph)
			{
				_graph = graph;
			}
		}

		protected virtual void OnDisable()
		{
			_graph = null;
		}
	}
}
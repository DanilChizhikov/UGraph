using UnityEditor;
using UnityEngine;

namespace DTech.UGraph.Core.Editor
{
	internal static class GraphWindowService
	{
		public static void Show(UGraphConfig graph)
		{
			if (!TryGetWindow(graph, out GraphEditorWindow window))
			{
				window = CreateWindow();
			}
            
			window.Show(graph);
		}

		private static bool TryGetWindow(UGraphConfig graph, out GraphEditorWindow window)
		{
			window = null;
			GraphEditorWindow[] windows = Resources.FindObjectsOfTypeAll<GraphEditorWindow>();
			for (int i = 0; i < windows.Length; i++)
			{
				GraphEditorWindow graphWindow = windows[i];
				if (graphWindow.GraphConfig == graph)
				{
					window = graphWindow;
					break;
				}
			}

			return window != null;
		}

		private static GraphEditorWindow CreateWindow() => EditorWindow.GetWindow<GraphEditorWindow>();
	}
}
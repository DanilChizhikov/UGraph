using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DTech.UGraph.Core.Editor
{
	public class GraphEditorWindow : EditorWindow
	{
		[SerializeField] private StyleSheet _styleSheet = default;
		
		public UGraphConfig GraphConfig { get; private set; }
		
		private bool _isShown;
		private UGraphConfigView _view;
		
		public void Show(UGraphConfig config)
		{
			GraphConfig = config;
			titleContent = new GUIContent(GraphConfig.name);
			if (_isShown)
			{
				Focus();
			}
			else
			{
				Show();
				_isShown = true;
			}
		}
        
		private void CreateGUI()
		{
			rootVisualElement.styleSheets.Add(_styleSheet);
			OnSelectionChange();
		}
        
		private void OnSelectionChange()
		{
			Object activeObject = Selection.activeObject;
			if (activeObject == GraphConfig)
			{
				return;
			}
            
			if (activeObject == null)
			{
				Debug.LogError("No active object");
				return;
			}

			if (GraphConfig == null)
			{
				Debug.LogError("No graph selected");
				return;
			}

			UGraphConfigView view = GraphViewService.GetView(GraphConfig);
			if (view == null)
			{
				Debug.LogError("No graph view found");
				return;
			}

			if (_view != null)
			{
				rootVisualElement.Remove(_view);
				_view.Dispose();
				_view = null;
			}
            
			_view = view;
			_view.style.flexGrow = 1f;
			rootVisualElement.Insert(0, _view);
		}
	}
}
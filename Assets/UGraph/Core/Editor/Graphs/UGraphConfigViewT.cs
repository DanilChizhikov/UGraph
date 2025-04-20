using System;
using System.Collections.Generic;
using System.IO;
using DTech.UGraph.Core.Extensions;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace DTech.UGraph.Core.Editor
{
	public sealed class UGraphConfigViewT<TConfig> : UGraphConfigView
		where TConfig : UGraphConfig
	{
		private readonly TConfig _config;
		private readonly List<UNodeConfigView> _nodeViews;
		private readonly Dictionary<UNodeConfig, UNodeConfigView> _nodeViewMap;
		private readonly List<Object> _undoCollection;

		internal UGraphConfigViewT(TConfig config)
		{
			_config = config.ThrowIfNull();
			_nodeViews = new List<UNodeConfigView>();
			_nodeViewMap = new Dictionary<UNodeConfig, UNodeConfigView>();
			_undoCollection = new List<Object>();
			Insert(0, new GridBackground());
			this.AddManipulator(new ContentZoomer());
			this.AddManipulator(new ContentDragger());
			this.AddManipulator(new SelectionDragger());
			this.AddManipulator(new RectangleSelector());
			string ussPath = Path.Combine(Application.persistentDataPath, "Editor/GraphEditor.uss");
			StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(ussPath);
			styleSheets.Add(styleSheet);
			CreateView(_config.EnterNode);
			for (int i = 0; i < _config.Nodes.Count; i++)
			{
				UNodeConfig nodeConfig = _config.Nodes[i];
				CreateView(nodeConfig);
			}
			
			CreateConnections();
			graphViewChanged += OnGraphViewChanged;
			Undo.undoRedoPerformed += UndoRedoPerformed;
		}

		public override void Dispose()
		{
			Undo.undoRedoPerformed -= UndoRedoPerformed;
			graphViewChanged -= OnGraphViewChanged;
			for (int i = _nodeViews.Count - 1; i >= 0; i--)
			{
				UNodeConfigView view = _nodeViews[i];
				RemoveView(view);
				view.Dispose();
			}

			_nodeViewMap.Clear();
			DeleteElements(graphElements);
			base.Dispose();
		}

		private void CreateView(UNodeConfig nodeConfig)
		{
			if (nodeConfig == null)
			{
				return;
			}
			
			UNodeConfigView view = NodeViewService.GetView(nodeConfig);
			_nodeViews.Add(view);
			_nodeViewMap.Add(nodeConfig, view);
			AddElement(view);
		}
		
		private void CreateConnections()
		{
			for (int i = 0; i < _nodeViews.Count; i++)
			{
				UNodeConfigView view = _nodeViews[i];
				IReadOnlyList<Edge> edges = view.GetEdges();
				for (int j = 0; j < edges.Count; j++)
				{
					Edge edge = edges[j];
					AddElement(edge);
				}
			}
		}
		
		private void CreateNode(Type type)
		{
			if (ScriptableObject.CreateInstance(type) is UNodeConfig createdNode)
			{
				createdNode.name = type.Name;
				_config.AddNode(createdNode);
				CreateView(createdNode);
			}
		}
		
		private void RemoveView(UNodeConfigView view)
		{
			_nodeViewMap.Remove(view.ConfigBase);
			_nodeViews.Remove(view);
		}
		
		private void UndoRedoPerformed() => AssetDatabase.Refresh();
		
		private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            _undoCollection.Clear();
            _undoCollection.Add(_config);
            _undoCollection.AddRange(_config.Nodes);
            Undo.RegisterCompleteObjectUndo(_undoCollection.ToArray(), "GraphChanged");
            if (graphViewChange.elementsToRemove != null)
            {
                foreach (GraphElement elementToRemove in graphViewChange.elementsToRemove)
                {
                    if (elementToRemove is UNodeConfigView nodeView)
                    {
                        OnStateViewRemoved(nodeView);
                    }

                    if (elementToRemove is Edge edge)
                    {
                        OnEdgeRemoved(edge);
                    }
                }
            }

            if (graphViewChange.edgesToCreate != null)
            {
                foreach (Edge edge in graphViewChange.edgesToCreate)
                {
                    OnEdgeCreated(edge);
                }
            }

            AssetDatabase.SaveAssets();
            return graphViewChange;
        }

        private void OnStateViewRemoved(UNodeConfigView view)
        {
            RemoveView(view);
            foreach (Edge edge in edges)
            {
                if (edge.input.node == view ||
                    edge.output.node == view)
                {
                    edge.input?.Disconnect(edge);
                    edge.output?.Disconnect(edge);
                    RemoveElement(edge);
                }
            }

            _config.RemoveNode(view.ConfigBase.Id);
        }

        private void OnEdgeCreated(Edge edge)
        {
            if (edge.output.node is UNodeConfigView outputView)
            {
                var inputView = (UNodeConfigView)edge.input.node;
				edge.output.ConnectTo(edge.input);
				outputView.ConfigBase.ConnectTo(inputView.ConfigBase.Id);
			}
        }

        private void OnEdgeRemoved(Edge edge)
        {
            if (edge.output.node is UNodeConfigView outputView &&
                edge.input.node is UNodeConfigView inputView)
			{
				outputView.ConfigBase.DisconnectFrom(inputView.ConfigBase.Id);
            }
        }
	}
}
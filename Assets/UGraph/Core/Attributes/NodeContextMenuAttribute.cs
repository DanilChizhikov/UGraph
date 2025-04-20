using System;

namespace DTech.UGraph.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeContextMenuAttribute : Attribute
    {
        public readonly string Path;

        public NodeContextMenuAttribute(string path)
        {
            Path = path;
        }
    }
}
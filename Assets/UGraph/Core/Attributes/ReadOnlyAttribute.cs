using System;
using UnityEngine;

namespace DTech.UGraph.Core
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ReadOnlyAttribute : PropertyAttribute
    {
    }
}
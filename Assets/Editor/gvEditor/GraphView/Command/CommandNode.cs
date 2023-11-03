using System.Collections.Generic;
using UnityEngine;

using System.Numerics;
using NUnit.Framework;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Editor.Command
{
    public class CommandNode : Command
    {
        public UnityEngine.Vector2 pos;
        public m_Node node;
        public List<Edge> edges;
        public bool visiable;

        public CommandNode()
        {
            visiable = true;
        }
        
        public override void Undo()
        {
            
        }
    }
}
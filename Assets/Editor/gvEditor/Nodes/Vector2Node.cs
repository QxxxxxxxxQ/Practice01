using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine; 

namespace Editor
{
    public class Vector2Node : m_Node
    {
        public Port Left;
        public Port Right;

        public Port OutputPort;

        public enum BinaryNodeOpType
        {
            Add,
            Sub,
            Divide,
            Mutiply
        }

        private EnumField _enumField;

        public BinaryNodeOpType OpType
        {
            get { return (BinaryNodeOpType)_enumField.value; }
            set { _enumField.value = value; }
        }
        
        

        public Vector2Node() : base()
        {
            title = "Vector2";

            OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                typeof(Port));
            Left = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
            Left.portName = "Left";
            inputContainer.Add(Left);
            Right = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
            Right.portName = "Right";
            inputContainer.Add(Right);

            _enumField = new EnumField(); 
            
            mainContainer.Add(_enumField);
        }
    }
}
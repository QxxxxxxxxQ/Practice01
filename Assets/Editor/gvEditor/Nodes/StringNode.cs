//字符串输出节点

using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Nodes
{
    public class StringNode : m_Node
    {
        private TextField _textField;
        public string Text
        {         
            get { return _textField.value;  }
        }

        public StringNode() : base()
        {
            title = "String";
            var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi,
                typeof(string));
            outputContainer.Add(outputPort);
            
            _textField = new TextField();
            mainContainer.Add(_textField);
        }
    }
}
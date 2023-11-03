//日志输出节点 

using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Editor.Nodes
{
    public abstract class ProcessNode : m_Node
    {
        public Port InputPort;
        public Port OutputPort; 
        public ProcessNode() : base()
        {
            InputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
                typeof(Port));
            InputPort.portName = "In";
            inputContainer.Add(InputPort);
            
            OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                typeof(Port));
            OutputPort.portName = "Out"; 
            outputContainer.Add(OutputPort); 
        }

        public abstract void Execute();  
    }
}
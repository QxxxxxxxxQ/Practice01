//根节点

using UnityEditor.Experimental.GraphView;

namespace Editor.Nodes
{
    public class RootNode : m_Node
    {
        public Port OutputPort;
        public RootNode(int id) : base()
        {
            title = "Root";
            capabilities -= Capabilities.Deletable;

            OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                typeof(Port));
            OutputPort.portName = "Out";
            outputContainer.Add(OutputPort);
        }
    }
}
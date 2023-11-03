using System.Linq;
using Unity.VisualScripting.YamlDotNet.RepresentationModel;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Editor.Nodes
{
    public class LogNode : ProcessNode
    {
        private Port inputString;
        
        public LogNode() : base()
        {
            title = "Log";
            inputString = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
                typeof(string));
            inputContainer.Add(inputString); 
        }  

        public override void Execute()
        {
            var edge = inputString.connections.FirstOrDefault();
            var node = edge?.output.node as StringNode; 
            
            if(node == null) return; 
            Debug.Log(node.Text);
        }
    }
}
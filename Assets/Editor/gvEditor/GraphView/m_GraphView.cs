using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
using Editor.Command;
using Editor.Nodes;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class m_GraphView : GraphView
    {
        //确保根节点存在
        public RootNode root;
        public CommandPool commandPool; //命令池

        public m_GraphView() : base()
        {
            StyleSheet stylesheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/gvEditor/Styles/GraphViewStyle.uss");
            styleSheets.Add(stylesheet);

            SetViews();

            root = new RootNode(0);

            AddElement(root);
            SetFunctions();
        }

        //视图加载
        private void SetViews()
        {
            //放大和缩小 
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            //更改背景颜色 
            GridBackground gridBackground = new GridBackground();
            gridBackground.name = "grid-background";
            Insert(0, gridBackground);

            gridBackground.StretchToParentSize();
            //滚轮缩放
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            //graphview窗口内容的拖动
            this.AddManipulator(new ContentDragger());
            //选中Node移动功能
            this.AddManipulator(new SelectionDragger());
            //多个node框选功能
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new SelectionDragger());
        }

        //基本功能加载 
        private void SetFunctions()
        {
            //右键弹出Node List  
            var searchWindowProvider = new SearchWindowProvider();
            searchWindowProvider.Initialize(this);
            nodeCreationRequest += context =>
            {
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
            };

            //Undo & Redo 
        }

        //添加监听回调
        private void SetNodesListener()
        {
        }


        //确保只有正确的端口被连接
        public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            foreach (var port in ports.ToList())
            {
                if (startAnchor.node == port.node ||
                    startAnchor.direction == port.direction ||
                    startAnchor.portType != port.portType)
                {
                    continue;
                }

                compatiblePorts.Add(port);
            }

            return compatiblePorts;
        }


        public void Execute()
        {
            var rootEdge = root.OutputPort.connections.FirstOrDefault();
            if (rootEdge == null) return;

            var currentNode = rootEdge.input.node as ProcessNode;
            while (true)
            {
                currentNode.Execute();
                var edge = currentNode.OutputPort.connections.FirstOrDefault();
                if (edge == null) break;

                currentNode = edge.input.node as ProcessNode;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using Editor.Command;
using System.Diagnostics;
using System.Timers;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;
using UnityEditor.Experimental.GraphView;

namespace Editor
{
    public class m_Node : Node
    {
        private int index;
        private System.Timers.Timer _timer;
        private Dictionary<int, CommandNode> commands;

        public string Text
        {
            get { return "0"; }
        }

        public m_Node()
        {
            title = "Sample";
            _timer = new System.Timers.Timer(1000); // 2000毫秒（2秒）
            _timer.AutoReset = false;
            _timer.Elapsed += TimerElapsed;

            //删除节点
            RegisterCallback<DetachFromPanelEvent>(evt =>
            {
                CommandNode cmdNode = new CommandNode();
                cmdNode.visiable = false; 
                RocordNodeToPool(cmdNode);
            });

            //添加节点 
            RegisterCallback<AttachToPanelEvent>(evt => { Debug.Log($"Add : {this.title}"); });

            //节点位置变动
            RegisterCallback<GeometryChangedEvent>(OnPositionChanged);
        }

        private bool isPositionChanging = false;
        private Stopwatch stopwatch = new Stopwatch();
        private Vector2 previousPosition = new Vector2();
        private bool isFirst = false;

        private void OnPositionChanged(GeometryChangedEvent evt)
        {
            if (!isFirst)
            {
                isFirst = true;
                previousPosition = GetPosition().position; 
            }
            _timer.Stop();
            _timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            isFirst = false; 
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            CommandNode cmdNode = new CommandNode();
            RocordNodeToPool(cmdNode);
        }

        //记录下当前的操作轨迹 
        private void RocordNodeToPool(CommandNode cmdNode)
        {
            isFirst = false; 
            Debug.Log("Save");
            cmdNode.node = this;
            cmdNode.pos = previousPosition;
            if (commands == null) commands = CommandPool.inst.commandMap;
            commands.Add(CommandPool.inst.stepIndex++, cmdNode);
            CommandPool.inst.didDeque.AddFirst(cmdNode); 
        }
    }
}
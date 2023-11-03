//命令池 : 记录命令操作
using System;
using System.Collections.Generic;
namespace Editor.Command
{
    public class  CommandPool
    {
        public static CommandPool inst;
        #region fields
        public int stepIndex; //当前的操作数目 
        public Dequeue<Command> didDeque;
        public Stack<Command> undidStack;
        public int maxCommandCount;
        public Dictionary<int, CommandNode> commandMap; 

        private CommandPool()
        {
            maxCommandCount = 100; //可撤回信息条数
            stepIndex = 0;
            undidStack = new Stack<Command>();
            didDeque = new Dequeue<Command>(maxCommandCount);
            commandMap = new Dictionary<int, CommandNode>(); 
        }

        public static CommandPool GetInst()
        {
            if (inst == null) inst = new CommandPool(); 
            return inst;
        }

        #endregion

        #region properties
        public int TotalCommandCount
        {
            get
            {
                return didDeque.Count;
            }
        }

        public int UndidCommandCount
        {
            get
            {
                return undidStack.Count;
            }
        }

        public int DidCommandCount
        {
            get
            {
                return didDeque.Count;
            }
        }
        #endregion

        #region constructors
        public CommandPool(int maxCommandCount)
        {
            didDeque = new Dequeue<Command>(maxCommandCount);
            undidStack = new Stack<Command>();
            this.maxCommandCount = maxCommandCount;
        }
        #endregion

        #region methods
        public void Register(Command command)
        {
            undidStack.Clear();

            if (didDeque.Count == maxCommandCount)
            {
                didDeque.RemoveFirst();
            }

            didDeque.AddLast(command);
        }

        public void Undo()
        {
            if (didDeque.Count == 0)
            {
                return;
            }
            
            
            Command command = didDeque.RemoveLast();
            command.Undo();
            undidStack.Push(command);
        }

        public void Redo()
        {
            if (undidStack.Count == 0)
            {
                return;
            }

            Command command = undidStack.Pop();
            didDeque.AddLast(command);
        }
        #endregion
    }
}
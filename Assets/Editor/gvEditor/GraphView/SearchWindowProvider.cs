//更容易地去创建节点

using System;
using System.Collections.Generic;
using Editor.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Editor
{
    public class SearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private m_GraphView _graphView;

        public void Initialize(m_GraphView graphView)
        { 
            _graphView = graphView;
        }

        //显示列表接口 
        List<SearchTreeEntry> ISearchWindowProvider.CreateSearchTree(SearchWindowContext context)
        {
            var entries = new List<SearchTreeEntry>();
            entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsClass && !type.IsAbstract && (type.IsSubclassOf(typeof(m_Node)))
                        && type != typeof(RootNode))
                    {
                        entries.Add(new SearchTreeEntry(new GUIContent(type.Name)) { level = 1, userData = type });
                    }
                }                
            }
            return entries;
        }

        bool ISearchWindowProvider.OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var type = searchTreeEntry.userData as System.Type;
            var node = Activator.CreateInstance(type) as m_Node;
            _graphView.AddElement(node);
            return true;
        }
    }
}
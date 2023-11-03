using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering;

namespace Editor
{
    public class FunctionsMenuPanel :ScriptableObject
    {
        private m_GraphView _graphView;

        public void Initialize(m_GraphView graphView)
        { 
            _graphView = graphView;
        }

        
    }
}
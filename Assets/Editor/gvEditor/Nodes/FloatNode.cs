using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.Nodes
{
    public class FloatNode : m_Node
    {
        public Port OutputPort;
        private FloatField _floatField;

        public FloatNode(): base()
        {
            title = "Float";

            OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                typeof(Port));
            OutputPort.portName = "Out";
            outputContainer.Add(OutputPort);

            _floatField = new FloatField();
            _floatField.RegisterValueChangedCallback(OnFloatValueChanged);
            mainContainer.Add(_floatField);
            RefreshExpandedState();
        }

        private void OnFloatValueChanged(ChangeEvent<float> evt)
        {
            //
        }

        public float Value
        {
            get { return _floatField.value; }
            set { _floatField.value = value; }
        }
    }
}
using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class MainMenu : VisualElement,  IResolvedStyle
    {
        public VisualElement panelContainer;
        private bool visible; //可见性

        public MainMenu()
        {
            // StyleSheet stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/gvEditor/Styles/GraphViewStyle.uss");
            // styleSheets.Add(stylesheet);

            visible = false;
            panelContainer = new VisualElement();

            Label label = new Label("Menu");
            panelContainer.Add(label);
            panelContainer.name = "mainMenuPanel";
            Insert(0, panelContainer);
            
            // 设置 panelContainer 的样式
            panelContainer.style.width = 350; // 设置宽度为200px
            panelContainer.style.height = Length.Percent(100f); // 设置高度为100%
            panelContainer.style.position = Position.Absolute;
            panelContainer.style.top = 0; // 顶部距离为0
            panelContainer.style.right = 0; // 右边距离为0
            panelContainer.style.backgroundColor =  new StyleColor(new Color(44f/255f, 44f/255f, 44f/255f, 1f));
            panelContainer.style.marginRight = new StyleLength(-100); // 负右边距以居中

            Add(panelContainer);
        }

        public void SetPanelData()
        {
            visible = !visible;
            panelContainer.visible = visible;
        }
    }
}
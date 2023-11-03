using System;
using Editor;
using Editor.Command;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using SearchableEditorWindow = UnityEditor.SearchableEditorWindow;


public class m_Window : EditorWindow
{
    private MainMenu _mainMenu;
    private m_GraphView _graphView;
    private CommandPool _commandPool;

    [MenuItem("Window/Graph View")]
    public static void Open()
    {
        GetWindow<m_Window>("Test Graph View");
    }


    private void OnEnable()
    {
        _mainMenu = new MainMenu();
        _graphView = new m_GraphView()
        {
            style = { flexGrow = 1 }
        };
        rootVisualElement.Add(_graphView);

        _commandPool = CommandPool.GetInst();
        Debug.Log($"CommandPool : {CommandPool.inst.stepIndex}");
        
        //窗口样式
        SetStyle();
        //快捷键设置
       SetFastKeyWords();
    }

    private void SetStyle()
    {
        rootVisualElement.name = "root";
        rootVisualElement.Add(_mainMenu.panelContainer);
        rootVisualElement.Add(new Button(_graphView.Execute) { text = "Execute" });
        Button mainMenuBtn = new Button(_mainMenu.SetPanelData){text = "Right Panel"};
        mainMenuBtn.name = "--main menu btn-";
        rootVisualElement.Add(mainMenuBtn); 
    }
    
    //快捷键 
    private void SetFastKeyWords()
    {
      rootVisualElement.RegisterCallback<KeyDownEvent>(evt =>
      {
          if (evt.keyCode == KeyCode.Z && evt.ctrlKey)
          {
              Debug.Log("Ctrl + Z Pressed");
              // 在这里执行你想要的操作
              CommandPool.inst.Undo();
          }
      });
    }
    
}
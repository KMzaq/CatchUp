using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//들어가면 그맵의 프리팹에 들어가있는 리스트를 받아서 스폰 하자

[CustomEditor(typeof(shapeCreator))]
public class shapeEditor : Editor
{
 



    shapeCreator _shapeCreator;

  
    SelectionInfo selectionInfo;

    bool needRepanint;
   
    GUIStyle style = new GUIStyle();


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Clear"))
        {
            _shapeCreator.points.Clear();
            _shapeCreator._Monster.Clear();
        }
    }


    void OnSceneGUI()
    {

        style.normal.textColor = Color.green;
        style.fontSize = 10;
        
        Event guiEvent = Event.current;
        
        if(guiEvent.type == EventType.Repaint)
        {
            Draw();
        }
        else
        if (guiEvent.type == EventType.Layout) { 
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
        else
        {
            Handleinput(guiEvent); 

            if (needRepanint)
            {
                HandleUtility.Repaint();
            }
        }

 

    }

    void Handleinput(Event guiEvent)
    {
        Ray mouseray = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
        float drawPlaneHight = 0;
        float dastodrawplane = (drawPlaneHight - mouseray.origin.y) / mouseray.direction.y;
        Vector3 mousePosition = mouseray.GetPoint(dastodrawplane);
      if (guiEvent.type == EventType.MouseDrag && guiEvent.button == 0 && guiEvent.modifiers == EventModifiers.None)
        {
            HandleLeftMouseDrag(mousePosition);
        }
      
        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0&& guiEvent.modifiers == EventModifiers.None)
        { 
            HandleLeftMouseDown(mousePosition);
        }
  
        if (guiEvent.type == EventType.MouseUp && guiEvent.button == 0 && guiEvent.modifiers == EventModifiers.None)
        {
            HandleLeftMouseUp(mousePosition);
        }
    
        UpdateMouseOverSelection(mousePosition);
    }


    void HandleLeftMouseDown(Vector3 mousepostition) {
        if (!selectionInfo.mouseIsOverPoint)
        {
            Undo.RecordObject(_shapeCreator, "AddPoint");
            _shapeCreator.points.Add(mousepostition);
            _shapeCreator._Monster.Add(null) ;
            selectionInfo.pointIndex = _shapeCreator.points.Count - 1;
        }
        selectionInfo.pointIsSelected = true;
        needRepanint = true;
    }
    void HandleLeftMouseUp(Vector3 mousepostition) {
        if (selectionInfo.pointIsSelected)
        {
            selectionInfo.pointIsSelected = false;
            selectionInfo.pointIndex = -1;
            needRepanint = true;
        }

    }
    void HandleLeftMouseDrag(Vector3 mousepostition) {
        if (selectionInfo.pointIsSelected)
        {
            _shapeCreator.points[selectionInfo.pointIndex] = mousepostition;
            needRepanint = true;
        }
    }



    private void OnEnable()
    {
        _shapeCreator = target as shapeCreator;
        selectionInfo = new SelectionInfo();
    }


    void UpdateMouseOverSelection(Vector3 MousePostition)
    {
        int mouseOverPointIndex = -1;
        for(int i =0; i < _shapeCreator.points.Count; i++)
        {
            if (Vector3.Distance(MousePostition, _shapeCreator.points[i]) < shapeCreator.handleRadius)
            {
                mouseOverPointIndex = i;
                break;
            }
        }
        if (mouseOverPointIndex != selectionInfo.pointIndex)
        {
            selectionInfo.pointIndex = mouseOverPointIndex;
            selectionInfo.mouseIsOverPoint = mouseOverPointIndex != -1;
            needRepanint = true;
        }
    }

    void Draw()
    {
        Handles.color = Color.black;
        for (int i = 0; i < _shapeCreator.points.Count; i++)
        {
            Handles.color = Color.black;
            Vector3 nextPoint = _shapeCreator.points[(i + 1) % _shapeCreator.points.Count];
            Handles.DrawDottedLine(_shapeCreator.points[i], nextPoint, 5);
            if(i == selectionInfo.pointIndex)
            {
                Handles.color = (selectionInfo.pointIsSelected) ? Color.black : Color.red;

            }
            else {
                Handles.color = Color.white;
            
            }
            //
           // Handles.DrawSolidDisc(_shapeCreator.points[i], Vector3.up, shapeCreator.handleRadius);
            Handles.DrawWireCube(_shapeCreator.points[i], Vector3.one*0.1f);
            //Handles.DoPositionHandle(_shapeCreator.points[i], new Quaternion(0, 0, 0, 0));
          
            //
            Handles.Label(new Vector3(_shapeCreator.points[i].x, _shapeCreator.points[i].y + shapeCreator.textHigh, _shapeCreator.points[i].z),
              "Num " + i, style);  
        }
        needRepanint = false;
    }


    public class SelectionInfo
    {
        public int pointIndex = -1;
        public bool mouseIsOverPoint;
        public bool pointIsSelected;
    }
}

   // 복도
    //복도 시작과 끝 의 위치를 정해줘야함


    //             방
    //방에 들어갔을때 벽에 붙어있는 복도들의위치 에다 벽 생성
    //
    //콜리전 충돌시 몹 생성 구현&복도 닫힘
    // 죽었을시 다음 패턴 있는지 확인 (GUI 버튼으로 몬스터LIST 추가)
    // 패턴 존재시 몹생성
    // 모든 몬스터가 죽으면 복도 열림
    //
    // 복도와 붙어있는 부분의 위치를 받아줌

    //제작

    //룸 시스템 
    // 몬스터 스폰 시스템 에디터(UGUI 제작
    // 맵 생성 에디터 제작
    // 
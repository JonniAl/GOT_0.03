using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : AbstractSceneManager
{
    public delegate void SelectionDelegate(List<Selectable> selectables);
    public static event SelectionDelegate SelectionEvent;

    [SerializeField]
    private static List<Selectable> selectables = new List<Selectable>();

    Vector3 firstMousePosition;
    Vector3 lastMousePosition;

    private InfoDB.PropertyAction currentProperty;
    private bool propertySelected = false;

    private bool multiselect;

    private void OnEnable()
    {
        InputController.OnLeftMouseButtonDown += MouseButtonDown;
        InputController.OnLeftMouseButtonUp += LeftMouseButtonUp;
        InputController.OnRightMouseButtonDown += MouseButtonDown;
        InputController.OnRightMouseButtonUp += RightMouseButtonUp;
        InputController.OnButton_LeftCtrl_Down += MultiselectTrue;
        InputController.OnButton_LeftCtrl_Up += MultiselectFalse;

        PropertyCell.PropertyCellEvent += PropertySelected;
    }

    private void Awake()
    {
        ASMAwake();
    }

    private void Start()
    {
        ASMStart();
    }

    private void OnDisable()
    {
        InputController.OnLeftMouseButtonDown -= MouseButtonDown;
        InputController.OnLeftMouseButtonUp -= LeftMouseButtonUp;
        InputController.OnRightMouseButtonDown -= MouseButtonDown;
        InputController.OnRightMouseButtonUp -= RightMouseButtonUp;
        InputController.OnButton_LeftCtrl_Down -= MultiselectTrue;
        InputController.OnButton_LeftCtrl_Up -= MultiselectFalse;

        PropertyCell.PropertyCellEvent -= PropertySelected;
    }

    protected override void LoadCameraFromGM()
    {
        currentCamera = GameManager.GetGameCamera();
    }

    private void PropertySelected(InfoDB.PropertyAction pAction)
    {
        propertySelected = true;

        currentProperty = pAction;

        Debug.Log(currentProperty);
    }

    private void MultiselectTrue()
    {
        multiselect = true;
    }

    private void MultiselectFalse()
    {
        multiselect = false;
    }

    private void MouseButtonDown(Vector3 vector)
    {
        firstMousePosition = vector;
    }

    private void RightMouseButtonUp(Vector3 vector)
    {
        lastMousePosition = vector;

        if (firstMousePosition == lastMousePosition)
        {
            RightMouseButtonClicked(vector);
        }
        else
        {
            RightMouseButtonSelection();
        }
    }

    private void RightMouseButtonSelection()
    {
        Debug.Log("RightMouseButtonSelection");
    }

    private void RightMouseButtonClicked(Vector3 clickPosition)
    {
        if (selectables.Count != 0)
        {
            PropertySelected(InfoDB.PropertyAction.Go);

            Vector3 worldPlaceClicked = currentCamera.ScreenToWorldPoint(clickPosition);

            foreach (Selectable selectable in selectables)
            {
                MoveController mController = selectable.gameObject.GetComponent<MoveController>();

                mController.Move(worldPlaceClicked);

                Debug.Log(selectable.name + " moving to " + worldPlaceClicked);
            }
        }
    }

    private void LeftMouseButtonUp(Vector3 vector)
    {
        lastMousePosition = vector;

        if (firstMousePosition == lastMousePosition)
        {
            LeftMouseButtonClicked();
        }
        else
        {
            LeftMouseButtonSelection();
        }

        if (SelectionEvent != null)
        {
            SelectionEvent(new List<Selectable>(selectables));
        }
    }

    private void LeftMouseButtonSelection()
    {
        if (multiselect)
        {    //rect add
             RectAdd();
        }
        else
        {   //rect selection
            RectSelection();
        }
    }

    private void LeftMouseButtonClicked()
    {
        if (!propertySelected)
        {
            if (multiselect)
            {   //single add
                SingleAdd();
            }
            else
            {   //single selection
                SingleSelection();
            }
        }

        propertySelected = false;
    }

    void Deselection()
    {
        foreach (Selectable sel in selectables)
        {
            sel.OnDeselected();
        }
        selectables.Clear();
    }

    void SingleSelection()
    {
        Deselection();
        Ray ray = currentCamera.ScreenPointToRay(lastMousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);
        try
        {
            Selectable unit = hitInfo.collider.GetComponent<Selectable>();
            unit.OnSelected();
            selectables.Add(unit);
        }
        catch
        {
        }
    }

    void RectSelection()
    {
        Deselection();
        RaycastHit[] hitsInfo;
        Vector3 center;
        Vector3 halfExtends;
        Ray rayLast = currentCamera.ScreenPointToRay(lastMousePosition);
        Ray rayFirst = currentCamera.ScreenPointToRay(firstMousePosition);
        RaycastHit firstHit, lastHit;
        Physics.Raycast(rayFirst, out firstHit);
        Physics.Raycast(rayLast, out lastHit);

        center = (lastHit.point + firstHit.point) / 2;
        halfExtends = (lastHit.point - firstHit.point) / 2;
        halfExtends = new Vector3(Mathf.Abs(halfExtends.x), 50f, Mathf.Abs(halfExtends.z));
        hitsInfo = Physics.BoxCastAll(center, halfExtends, Vector3.up);
        foreach (RaycastHit hit in hitsInfo)
        {
            try
            {
                Selectable unit = hit.collider.GetComponent<Selectable>();
                if (unit.IsUnit || selectables.Count == 0)
                {
                    unit.OnSelected();
                    selectables.Add(unit);
                }
            }
            catch
            {
            }
        }
    }

    void SingleAdd()
    {
        Ray ray = currentCamera.ScreenPointToRay(lastMousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);
        try
        {
            Selectable unit = hitInfo.collider.GetComponent<Selectable>();
            if (!unit.IsUnit || IsBuildingInSelection())
                return;
            if (unit.IsSelected)
            {
                unit.OnDeselected();
                selectables.Remove(unit);
            }
            else
            {
                unit.OnSelected();
                selectables.Add(unit);
            }
        }
        catch
        {
        }
    }

    void RectAdd()
    {
        if (IsBuildingInSelection())
            return;
        RaycastHit[] hitsInfo;
        Vector3 center;
        Vector3 halfExtends;
        Ray rayLast = currentCamera.ScreenPointToRay(lastMousePosition);
        Ray rayFirst = currentCamera.ScreenPointToRay(firstMousePosition);
        RaycastHit firstHit, lastHit;
        Physics.Raycast(rayFirst, out firstHit);
        Physics.Raycast(rayLast, out lastHit);

        center = (lastHit.point + firstHit.point) / 2;
        halfExtends = (lastHit.point - firstHit.point) / 2;
        halfExtends = new Vector3(Mathf.Abs(halfExtends.x), 50f, Mathf.Abs(halfExtends.z));
        hitsInfo = Physics.BoxCastAll(center, halfExtends, Vector3.up);
        foreach (RaycastHit hit in hitsInfo)
        {
            try
            {
                Selectable unit = hit.collider.GetComponent<Selectable>();
                if (!unit.IsUnit)
                    return;
                if (!unit.IsSelected)
                {
                    unit.OnSelected();
                    selectables.Add(unit);
                }
            }
            catch
            {
            }
        }
    }

    bool IsBuildingInSelection()
    {
        foreach (Selectable unit in selectables)
        {
            if (!unit.IsUnit)
                return true;
        }
        return false;
    }
}

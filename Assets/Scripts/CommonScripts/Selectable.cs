using System.Collections;
using System;
using UnityEngine;

[Serializable]
public class Selectable : MonoBehaviour {
	GameObject selectionCircleObj;
	static Sprite selectionCircleSprite;

	GameObject selectionCircle;
	MeshRenderer mesh;

	float circleY;

	bool isSelected = false;
	public bool IsSelected {
		get {
			return this.isSelected;
		}
		private set {
			isSelected = value;
		}
	}

	[SerializeField]
	bool isUnit = true;
	public bool IsUnit {
		get {
			return this.isUnit;
		}
		private set {
			isUnit = value;
		}
	}

	// Use this for initialization
	void Start () {
		if (selectionCircleSprite == null)
			selectionCircleSprite = Resources.Load<Sprite>("SelectionCircle");
		selectionCircleObj = new GameObject();
		selectionCircleObj.transform.parent = transform;
		selectionCircleObj.name = "SelectionCircle";
		selectionCircleObj.transform.Rotate (90, 0, 0);
		selectionCircleObj.transform.localScale = Vector3.one;
        selectionCircleObj.AddComponent<SpriteRenderer>().sprite = selectionCircleSprite;

		circleY = 0.1f - GetComponent<MeshRenderer>().bounds.extents.y / transform.localScale.y;
		selectionCircleObj.transform.localPosition = new Vector3 (0, circleY , 0);

        selectionCircle = selectionCircleObj;
        selectionCircle.SetActive(false);
    }

	public void OnSelected(){
        selectionCircle.SetActive(true);
        isSelected = true;
	}

	public void OnDeselected(){
        selectionCircle.SetActive(false);
        isSelected = false;
	}
}
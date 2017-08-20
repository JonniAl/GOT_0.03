using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProperties : MonoBehaviour
{
    [SerializeField]
    private PropertyHolder pHolder;

    private void OnEnable()
    {
        LevelManager.SelectionEvent += DrawProperties;
    }

    private void Start()
    {
        foreach (Transform cell in transform)
        {
            cell.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        LevelManager.SelectionEvent -= DrawProperties;
    }

    private void DrawProperties(List<Selectable> selectables)
    {
        if (selectables.Count > 0)
        {
            GameObject temp = selectables[0].gameObject;
            pHolder  = temp.GetComponent<PropertyHolder>();

            foreach (Transform cell in transform)
            {         
                cell.gameObject.SetActive(true);
                Property property = pHolder.GetPropertyFromHolder((int)cell.GetComponent<PropertyCell>().cellIdentifier);

                cell.gameObject.GetComponent<Image>().sprite = property.propertySprite;
                cell.gameObject.GetComponent<PropertyCell>().SetProperty(property.pAction);
            }                     
        }
        else
        {
            pHolder = null;

            foreach(Transform cell in transform)
            {
                cell.gameObject.SetActive(false);
            }
        }
    }
}
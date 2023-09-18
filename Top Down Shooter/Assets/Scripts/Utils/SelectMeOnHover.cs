using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectMeOnHover : MonoBehaviour
{
    public void SelectMe()
    {
        if(EventSystem.current.currentSelectedGameObject != this)
        {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }
    }
}

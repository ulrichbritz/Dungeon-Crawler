using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UB
{
    public class HighlightManager : MonoBehaviour
    {
        private Transform highlightedObj;
        private Transform selectedObj;
        public LayerMask selectableLayer;

        private Outline highlightOutline;
        private RaycastHit hit;

        private void Update()
        {
            if(PlayerCameraManager.instance != null)
                HoverHighlight();
        }

        public void HoverHighlight()
        {
            if (highlightedObj != null)
            {
                if(highlightOutline != null)
                    highlightOutline.enabled = false;

                highlightedObj = null;
            }

            Ray ray = PlayerCameraManager.instance.cameraObject.ScreenPointToRay(Input.mousePosition);

            if(!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, selectableLayer))
            {
                highlightedObj = hit.transform;

                if (highlightedObj.CompareTag("Interactable") && highlightedObj != selectedObj)
                {
                    highlightOutline = highlightedObj.GetComponent<Outline>();
                    if(highlightOutline != null)
                        highlightOutline.enabled = true;
                    else
                    {
                        highlightOutline = highlightedObj.GetComponentInChildren<Outline>();
                        if (highlightOutline != null)
                            highlightOutline.enabled = true;
                    }
                }
                else
                {
                    highlightedObj = null;
                }
            }
        }

        public void SelectedHighlight()
        {
            if (highlightedObj.CompareTag("Enemy") || highlightedObj.CompareTag("Interactable"))
            {
                if(selectedObj != null)
                {
                    selectedObj.GetComponent<Outline>().enabled = false;
                }

                selectedObj = hit.transform;
                selectedObj.GetComponent<Outline>().enabled = true;

                highlightOutline.enabled = true;
                highlightedObj = null;
            }
        }

        public void DeselectHighlight()
        {
            if(selectedObj != null)
            {
                if(highlightOutline != null)
                {
                    selectedObj.GetComponent<Outline>().enabled = false;
                }
                
                selectedObj = null;
            }
            
        }
    }

}

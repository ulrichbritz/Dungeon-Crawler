using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UB
{
    public class FadeObjectBlockingObject : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform target;
        [SerializeField] Camera camera;
        [SerializeField] [Range(0, 1f)] private float fadedAlpha = 0.33f;
        [SerializeField] private bool retainShadows = true;
        [SerializeField] Vector3 targetPositionOffset = Vector3.up;
        [SerializeField] private float FadeSpeed = 1f;

        [Header("Read Only")]
        [SerializeField] private List<FadingObject> objectsBlockingView = new List<FadingObject>();
        private Dictionary<FadingObject, Coroutine> runningCoroutines = new Dictionary<FadingObject, Coroutine>();

        private RaycastHit[] hits = new RaycastHit[10];

        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            if(camera == null)
            {
                if (PlayerCameraManager.instance != null)
                {
                    camera = PlayerCameraManager.instance.cameraObject.GetComponent<Camera>();
                    StartCoroutine(CheckForObjects());
                }
                    
            }
        }

        private IEnumerator CheckForObjects()
        {
            while (true)
            {
                int _hits = Physics.RaycastNonAlloc(
                    camera.transform.position, 
                    (target.transform.position + targetPositionOffset - camera.transform.position).normalized, 
                    hits,
                    Vector3.Distance(camera.transform.position, target.transform.position + targetPositionOffset),
                    layerMask
                );

                if (_hits > 0)
                {
                    for(int i = 0; i < _hits; i++)
                    {
                        FadingObject fadingObject = GetFadingObjectFromHit(hits[i]);

                        if(fadingObject != null && !objectsBlockingView.Contains(fadingObject))
                        {
                            if (runningCoroutines.ContainsKey(fadingObject))
                            {
                                if(runningCoroutines[fadingObject] != null)
                                {
                                    StopCoroutine(runningCoroutines[fadingObject]);
                                }

                                runningCoroutines.Remove(fadingObject);
                            }

                            runningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectOut(fadingObject)));
                            objectsBlockingView.Add(fadingObject);
                        }
                    }
                }

                FadeObjectsNoLongerBeingHit();

                ClearHits();

                yield return null;
            }
 
        }

        private void FadeObjectsNoLongerBeingHit()
        {
            List<FadingObject> objectsToRemove = new List<FadingObject>(objectsBlockingView.Count);

            foreach(FadingObject _fadingObject in objectsBlockingView)
            {
                bool objectIsBeingHit = false;
                for (int i = 0; i <hits.Length; i++)
                {
                    FadingObject hitFadingObject = GetFadingObjectFromHit(hits[i]);
                    if(hitFadingObject != null && _fadingObject == hitFadingObject)
                    {
                        objectIsBeingHit = true;
                        break;
                    }
                }

                if (!objectIsBeingHit)
                {
                    if (runningCoroutines.ContainsKey(_fadingObject))
                    {
                        if(runningCoroutines[_fadingObject] != null)
                        {
                            StopCoroutine(runningCoroutines[_fadingObject]);
                        }

                        runningCoroutines.Remove(_fadingObject);
                    }

                    runningCoroutines.Add(_fadingObject, StartCoroutine(FadeObjectIn(_fadingObject)));
                    objectsToRemove.Add(_fadingObject);
                }

               
            }

            foreach (FadingObject removeObject in objectsToRemove)
            {
                objectsBlockingView.Remove(removeObject);
            }
        }

        private IEnumerator FadeObjectOut(FadingObject _fadingObject)
        {
            foreach(Material _material in _fadingObject.materials)
            {
                _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                _material.SetInt("_ZWrite", 0);
                _material.SetInt("_Surface", 1);

                _material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                _material.SetShaderPassEnabled("DepthOnly", false);
                _material.SetShaderPassEnabled("SHADOWCASTER", retainShadows);

                _material.SetOverrideTag("RenderType", "Transparent");

                _material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                _material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            }

            float time = 0;

            while(_fadingObject.materials[0].color.a > fadedAlpha)
            {
                foreach(Material material in _fadingObject.materials)
                {
                    if (material.HasProperty("_Color"))
                    {
                        material.color = new Color(
                            material.color.r,
                            material.color.g,
                            material.color.b,
                            Mathf.Lerp(_fadingObject.initialAlpha, fadedAlpha, time * FadeSpeed)
                        );
                    }
                }

                time += Time.deltaTime;
                yield return null;
            }

            if (runningCoroutines.ContainsKey(_fadingObject))
            {
                StopCoroutine(runningCoroutines[_fadingObject]);
                runningCoroutines.Remove(_fadingObject);
            }
        }

        private IEnumerator FadeObjectIn(FadingObject _fadingObject)
        {
            float time = 0;

            while (_fadingObject.materials[0].color.a < _fadingObject.initialAlpha)
            {
                foreach (Material material in _fadingObject.materials)
                {
                    if (material.HasProperty("_Color"))
                    {
                        material.color = new Color(
                            material.color.r,
                            material.color.g,
                            material.color.b,
                            Mathf.Lerp(fadedAlpha, _fadingObject.initialAlpha, time * FadeSpeed)
                        );
                    }
                }

                time += Time.deltaTime;
                yield return null;
            }

            foreach (Material _material in _fadingObject.materials)
            {
                _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                _material.SetInt("_ZWrite", 1);
                _material.SetInt("_Surface", 0);

                _material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

                _material.SetShaderPassEnabled("DepthOnly", true);
                _material.SetShaderPassEnabled("SHADOWCASTER", true);

                _material.SetOverrideTag("RenderType", "Opaque");

                _material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
                _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            }

            if (runningCoroutines.ContainsKey(_fadingObject))
            {
                StopCoroutine(runningCoroutines[_fadingObject]);
                runningCoroutines.Remove(_fadingObject);
            }
        }

        private void ClearHits()
        {
            System.Array.Clear(hits, 0, hits.Length);
        }

        private FadingObject GetFadingObjectFromHit(RaycastHit hit)
        {
            return hit.collider != null ? hit.collider.GetComponent<FadingObject>() : null;
        }
    } 
}


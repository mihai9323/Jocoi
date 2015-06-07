using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

    internal Quaternion cameraDefaultRotation;
    internal Vector3 cameraDefaultPosition;
    internal float cameraDefaultZoom;

    public float maxZoom = 30;

    public float zoomInTime = 4.0f;
    public float zoomOutTime = 2.0f;
    
    
    protected Quaternion targetCameraRotation;
    protected GameObject targetObject;

    protected void Start()
    {
        cameraDefaultZoom = camera.fieldOfView;
        cameraDefaultPosition = camera.transform.position;
        cameraDefaultRotation = camera.transform.rotation;
    }
    public virtual void StartZoom(GameObject obj)
    {
       
        targetObject = obj;
       
        StopAllCoroutines();
        transform.LookAt(obj.transform);
        targetCameraRotation = transform.rotation;
        
        transform.rotation = cameraDefaultRotation;
      
        StartCoroutine(ZoomIn());
    }
    public virtual void StopZoom()
    {
      
        StopAllCoroutines();
        StartCoroutine(ZoomOut());
        targetObject = null;
    }
    private IEnumerator ZoomIn()
    {
        float percent = 0;
       
        while (percent < 1.0f && Mathf.Abs(camera.fieldOfView - maxZoom)>1.0f)
        {
            percent += (1.0f/zoomInTime) * Time.deltaTime;
            camera.fieldOfView += (-camera.fieldOfView + maxZoom) * percent;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetCameraRotation, percent);

           
           
            yield return new WaitForFixedUpdate();
        }
        
        camera.fieldOfView = maxZoom;
        while (true)
        {
            transform.LookAt(targetObject.transform);
            yield return new WaitForFixedUpdate();
        }
        
    }
    private IEnumerator ZoomOut()
    {
       
        float percent = 0;
        while (percent<1.0f)
        {
            percent += (1.0f/zoomOutTime) * Time.deltaTime;
            camera.fieldOfView += (-camera.fieldOfView + cameraDefaultZoom) * percent;
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraDefaultRotation, percent);
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = cameraDefaultRotation;
        camera.fieldOfView = cameraDefaultZoom;
    }
    
}

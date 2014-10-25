using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

    internal Quaternion cameraDefaultRotation;
    internal Vector3 cameraDefaultPosition;
    internal float cameraDefaultFieldOfView;

    private Quaternion targetCameraRotation;
    public float targetFieldOfView = 30;

    public float zoomInTime = 4.0f;
    public float zoomOutTime = 2.0f;
   
    
    private GameObject targetObject;

    void Start()
    {
        cameraDefaultFieldOfView = camera.fieldOfView;
        cameraDefaultPosition = camera.transform.position;
        cameraDefaultRotation = camera.transform.rotation;
    }
    public void StartZoom(GameObject obj)
    {
       
        targetObject = obj;
       
        StopAllCoroutines();
        transform.LookAt(obj.transform);
        targetCameraRotation = transform.rotation;
        
        transform.rotation = cameraDefaultRotation;
      
        StartCoroutine(ZoomIn());
    }
    public void StopZoom()
    {
      
        StopAllCoroutines();
        StartCoroutine(ZoomOut());
        targetObject = null;
    }
    private IEnumerator ZoomIn()
    {
        float percent = 0;
       
        while (percent < 1.0f && Mathf.Abs(camera.fieldOfView - targetFieldOfView)>1.0f)
        {
            percent += (1.0f/zoomInTime) * Time.deltaTime;
            camera.fieldOfView += (-camera.fieldOfView + targetFieldOfView) * percent;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetCameraRotation, percent);

           
           
            yield return new WaitForFixedUpdate();
        }
        
        camera.fieldOfView = targetFieldOfView;
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
            camera.fieldOfView += (-camera.fieldOfView + cameraDefaultFieldOfView) * percent;
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraDefaultRotation, percent);
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = cameraDefaultRotation;
        camera.fieldOfView = cameraDefaultFieldOfView;
    }
    
}

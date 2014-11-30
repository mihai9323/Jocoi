using UnityEngine;
using System.Collections;

public class CameraZoom2D : CameraZoom {

   

  
    private float ct;

    private void Start()
    {
        ct = 0;
        cameraDefaultZoom = camera.orthographicSize;
        cameraDefaultRotation = transform.rotation;
        cameraDefaultPosition = transform.position;
    }
    public override void StartZoom(GameObject obj)
    {

        targetObject = obj;

        StopAllCoroutines();
        transform.LookAt(obj.transform);
        targetCameraRotation = transform.rotation;

        transform.rotation = cameraDefaultRotation;

        StartCoroutine(ZoomIn());
    }
    public override void StopZoom()
    {

        StopAllCoroutines();
        StartCoroutine(ZoomOut());
        targetObject = null;
    }

    private IEnumerator ZoomIn()
    {
        float percent = 0;

        while (percent < 1.0f && Mathf.Abs(camera.orthographicSize - maxZoom) > 0)
        {
            percent += (1.0f / zoomInTime) * Time.deltaTime;
            camera.orthographicSize += (-camera.orthographicSize + maxZoom) * percent;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetCameraRotation, percent);



            yield return new WaitForFixedUpdate();
        }

        camera.orthographicSize = maxZoom;
        while (true)
        {
            transform.LookAt(targetObject.transform);
            yield return new WaitForFixedUpdate();
        }

    }
    private IEnumerator ZoomOut()
    {

        float percent = 0;
        while (percent < 1.0f)
        {
            percent += (1.0f / zoomOutTime) * Time.deltaTime;
            camera.orthographicSize += (-camera.orthographicSize + cameraDefaultZoom) * percent;
            transform.rotation = Quaternion.Lerp(transform.rotation, cameraDefaultRotation, percent);
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = cameraDefaultRotation;
        camera.orthographicSize = cameraDefaultZoom;
    }

}

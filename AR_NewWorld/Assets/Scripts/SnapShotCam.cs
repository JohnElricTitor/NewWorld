using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnapShotCam : MonoBehaviour
{
    public GameObject[] cameras; //Left, Right, Portrait
    int resWidth = 256;
    int resHeight = 256;

    int currentCam;

    private void Awake()
    {
        cameras[1].SetActive(true);
        resWidth = cameras[1].GetComponent<Camera>().targetTexture.width;
        resHeight = cameras[1].GetComponent<Camera>().targetTexture.height;
    }
    private void Update()
    {
        Debug.Log(Input.deviceOrientation);
       // CamOrientation();
        
    }
    public void Capture()
    {
        CamOrientation();
        StartCoroutine(TakeScreenshotAndSave());
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture.active = cameras[currentCam].GetComponent<Camera>().targetTexture;
        Texture2D ss = new Texture2D(RenderTexture.active.width, RenderTexture.active.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, RenderTexture.active.width, RenderTexture.active.height), 0, 0);
        ss.Apply();
        // Save the screenshot to Gallery/Photos
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));

        Debug.Log("Permission result: " + permission);

        // To avoid memory leaks
        Destroy(ss);
    }

    private void CamOrientation()
    {
        foreach(GameObject cam in cameras)
        {
            cam.SetActive(false);
        }

        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            cameras[0].SetActive(true);
            currentCam = 0;
        }

        else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            cameras[2].SetActive(true);
            currentCam = 2;
        }
        else
        {
            cameras[1].SetActive(true);
            currentCam = 1;
        }

    }

    public void StartRecording()
    {
        //cameras[currentCam].GetComponent<Camera>().r
    }

    public void StopRecording()
    {
        //NativeGallery.Permission permission = NativeGallery.SaveVideoToGallery(ss, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));
    }
}
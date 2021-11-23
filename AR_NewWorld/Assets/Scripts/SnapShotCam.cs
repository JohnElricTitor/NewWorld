using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class SnapShotCam : MonoBehaviour
{
    Camera snapShotCam;
    int resWidth = 256;
    int resHeight = 256;

    private void Awake()
    {
        snapShotCam = GetComponent<Camera>();
        if (snapShotCam.targetTexture == null)
            snapShotCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        else
        {
            resWidth = snapShotCam.targetTexture.width;
            resHeight = snapShotCam.targetTexture.height;
        }
        //snapCam.gameObject.SetActive(false);
    }

    public void Capture()
    {
        StartCoroutine(TakeScreenshotAndSave());
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        RenderTexture.active = snapShotCam.targetTexture;
        ss.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        ss.Apply();
        // Save the screenshot to Gallery/Photos
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));


        Debug.Log("Permission result: " + permission);

        // To avoid memory leaks
        Destroy(ss);
    }

    public void Record()
    {

    }
}
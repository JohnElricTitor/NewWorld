using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SnapShotCam : MonoBehaviour
{
    public Camera[] cameras; //Left, Right, Portrait
    int resWidth = 256;
    int resHeight = 256;
    [SerializeField] RawImage previewImage;

    [HideInInspector] public int currentCam;

    public void Capture()
    {
        CamOrientation();
        StartCoroutine(TakeScreenshotAndSave());
    }

    private IEnumerator TakeScreenshotAndSave()
    {
        yield return new WaitForEndOfFrame();
        resWidth = cameras[currentCam].targetTexture.width;
        resHeight = cameras[currentCam].targetTexture.height;

        RenderTexture tempRT = new RenderTexture(resWidth, resHeight, 24);
        cameras[currentCam].targetTexture = tempRT;
        cameras[currentCam].Render();

        RenderTexture.active = tempRT;
        Texture2D capture = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        capture.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);

        // Save the screenshot to Gallery/Photos
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(capture, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));

        Debug.Log("Permission result: " + permission);

        // To avoid memory leaks
        Destroy(capture);
    }

    private IEnumerator TakeVideoAndSave()
    {
        yield return new WaitForEndOfFrame();
        resWidth = cameras[currentCam].targetTexture.width;
        resHeight = cameras[currentCam].targetTexture.height;

        RenderTexture tempRT = new RenderTexture(resWidth, resHeight, 24);
        cameras[currentCam].targetTexture = tempRT;
        cameras[currentCam].Render();

        RenderTexture.active = tempRT;
        Texture2D capture = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        capture.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);

        // Save the screenshot to Gallery/Photos
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(capture, "GalleryTest", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));

        Debug.Log("Permission result: " + permission);

        // To avoid memory leaks
        Destroy(capture);
    }

    private void CamOrientation()
    {
        foreach (Camera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }

        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)         //LEFT
        {
            cameras[0].gameObject.SetActive(true);
            currentCam = 0;
        }

        else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)   //RIGHT
        {
            cameras[1].gameObject.SetActive(true);
            currentCam = 1;
        }
        else                                                                    //PORTRAIT
        {
            cameras[2].gameObject.SetActive(true);
            currentCam = 2;
        }
    }


    public void PickImage()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                Texture2D texture = NativeGallery.LoadImageAtPath(path);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }


                previewImage.texture = texture;
                previewImage.transform.parent.gameObject.SetActive(true);
               

                // If a procedural texture is not destroyed manually, 
                // it will only be freed after a scene change
                //Destroy(texture, 5f);
            }
        });

        Debug.Log("Permission result: " + permission);
    }

}
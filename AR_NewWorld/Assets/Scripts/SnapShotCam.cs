using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class SnapShotCam : MonoBehaviour
{

    Camera snapCam;
    int resWidth = 256;
    int resHeight = 256;

    

    private void Awake()
    {
        snapCam = GetComponent<Camera>();
        if (snapCam.targetTexture == null)
            snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
        else
        {
            resWidth = snapCam.targetTexture.width;
            resHeight = snapCam.targetTexture.height;
        }
        snapCam.gameObject.SetActive(false);
    }

    public void TakeSnapShot()
    {
        snapCam.gameObject.SetActive(true);

    }

    private void LateUpdate()
    {
        if (snapCam.gameObject.activeInHierarchy)
        {
            Texture2D snapShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            snapCam.Render();
            RenderTexture.active = snapCam.targetTexture;
            snapShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            byte[] bytes = snapShot.EncodeToPNG();
            string filename = SnapshotName();
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log("SnapShot taken!");
            
            snapCam.gameObject.SetActive(false);
        }


        string SnapshotName()
        {
            return string.Format("{0}/Snapshots/snap_{1}x{2}_{3}.png", 
            Application.dataPath, 
            resWidth, 
            resHeight, 
            System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
        }
    }
}

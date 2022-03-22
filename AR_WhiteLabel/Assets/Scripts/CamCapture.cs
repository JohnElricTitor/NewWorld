using UnityEngine;
using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using System.Collections;

public class CamCapture : MonoBehaviour
{
    RenderTexture renderTexture;
    private MP4Recorder recorder;
    private Coroutine recordVideoCoroutine;

    int currentCam;


    public async void StartRecording()
    {
        GetComponent<SnapShotCam>().CamOrientation();
        currentCam = GetComponent<SnapShotCam>().currentCam;
        renderTexture = GetComponent<SnapShotCam>().cameras[currentCam].targetTexture;

        // Create a recorder
        recorder = new MP4Recorder(renderTexture.width, renderTexture.height, 60, 6_000_000,   // bits per second
        keyframeInterval: 3);
        
        recordVideoCoroutine = StartCoroutine(recording()); //Start recording
        Debug.Log("Recording Started");
    }

    private IEnumerator recording()
    {

        // Create a clock for generating recording timestamps
        RealtimeClock clock = new RealtimeClock();
        Texture2D readbackTexture = new Texture2D(renderTexture.width, renderTexture.height);

        for (int i = 0; ; i++)
        {
            GetComponent<SnapShotCam>().cameras[currentCam].Render();
            RenderTexture.active = renderTexture;
            readbackTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            
            // Commit the frame to NatCorder for encoding
            recorder.CommitFrame(readbackTexture.GetPixels32(), clock.timestamp);
            
            // Wait till end of frame
            yield return new WaitForEndOfFrame();
            RenderTexture.active = null;
        }
    }

    public async void stopRecording()
    {
        
        StopCoroutine(recordVideoCoroutine);                        //Stop Coroutine
        string recordingPath = await recorder.FinishWriting();      // Finish writing
        NativeGallery.Permission permission = NativeGallery.SaveVideoToGallery(recordingPath, "GalleryTest", "Video.mp4", (success, path) => Debug.Log("Media save result: " + success + " " + path));

        Debug.Log("Permission result: " + permission);
    }
}

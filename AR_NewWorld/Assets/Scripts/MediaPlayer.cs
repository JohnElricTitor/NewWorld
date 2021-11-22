using UnityEngine;
using UnityEngine.UI;

public class MediaPlayer : DisplayScreen
{
    [SerializeField] RawImage mediaImage;

    protected override void Start()
    {
        base.Start();
        CloseScreen();
    }

    public void OpenScreen(Texture imageText)
    {
        mediaImage.texture = imageText;
        SetScreen(true);
    }

    public void CloseScreen()
    {
        SetScreen(false);
    }


}

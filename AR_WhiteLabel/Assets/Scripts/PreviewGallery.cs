using UnityEngine;
using UnityEngine.UI;

public class PreviewGallery : MonoBehaviour
{

    [SerializeField] RawImage previewImage;
    Texture2D texture;

    public void PickImage()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create Texture from selected image
                texture = NativeGallery.LoadImageAtPath(path);

                //if (texture == null)
                //{
                //    Debug.Log("Couldn't load texture from " + path);
                //    return;
                //}


                previewImage.texture = texture;
                CanvasExtensions.SizeToParent(previewImage);
                previewImage.transform.parent.gameObject.SetActive(true);

            }
        });

        Debug.Log("Permission result: " + permission);
    }

    public void PickImageOrVideo()
    {
	    if( NativeGallery.CanSelectMultipleMediaTypesFromGallery() )
	    {
	    	NativeGallery.Permission permission = NativeGallery.GetMixedMediaFromGallery( ( path ) =>
	    	{
	    		Debug.Log( "Media path: " + path );
	    		if( path != null )
	    		{
                    if (NativeGallery.GetMediaTypeOfFile(path) == NativeGallery.MediaType.Image)
                    {
                        texture = NativeGallery.LoadImageAtPath(path);
                        previewImage.texture = texture;
                        CanvasExtensions.SizeToParent(previewImage);
                        previewImage.transform.parent.gameObject.SetActive(true);
                    }
                    else if (NativeGallery.GetMediaTypeOfFile(path) == NativeGallery.MediaType.Video)
                    {
                        Handheld.PlayFullScreenMovie(path);
                    }
                }
	    	}, NativeGallery.MediaType.Image | NativeGallery.MediaType.Video, "Select an image or video" );

	    	Debug.Log( "Permission result: " + permission );
	    }
    }

    public void Clear()
    {
        Destroy(texture);
    }
}



static class CanvasExtensions
{
    public static Vector2 SizeToParent(this RawImage image, float padding = 0)
    {
        float w = 0, h = 0;
        RectTransform parent = image.GetComponentInParent<RectTransform>();
        RectTransform imageTransform = image.GetComponent<RectTransform>();

        // check if there is something to do
        if (image.texture != null)
        {
            if (!parent) { return imageTransform.sizeDelta; } //if we don't have a parent, just return our current width;
            padding = 1 - padding;
            float ratio = image.texture.width / (float)image.texture.height;
            Rect bounds = new Rect(0, 0, parent.rect.width, parent.rect.height);
            if (Mathf.RoundToInt(imageTransform.eulerAngles.z) % 180 == 90)
            {
                //Invert the bounds if the image is rotated
                bounds.size = new Vector2(bounds.height, bounds.width);
            }
            //Size by height first
            h = bounds.height * padding;
            w = h * ratio;
            if (w > bounds.width * padding)
            { //If it doesn't fit, fallback to width;
                w = bounds.width * padding;
                h = w / ratio;
            }
        }
        imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
        return imageTransform.sizeDelta;
    }
}

using UnityEngine;

public class NFTController : MonoBehaviour
{

    Animator anim;
    [SerializeField] GameObject captureUI;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void MenuState (bool state)
    {
        anim.SetBool("isOpen", state);
    }

    public void TurnOff()
    {
        captureUI.SetActive(false);
    }

    public void TurnOn()
    {
        captureUI.SetActive(true);
    }

}

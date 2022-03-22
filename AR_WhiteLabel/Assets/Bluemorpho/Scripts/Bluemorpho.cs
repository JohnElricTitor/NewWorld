using UnityEngine;
using System.Collections;

public class Bluemorpho : MonoBehaviour {
    Animator bluemorpho;
    private IEnumerator coroutine;
	// Use this for initialization
	void Start () {
        bluemorpho = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (bluemorpho.GetCurrentAnimatorStateInfo(0).IsName("fly"))
        {
            bluemorpho.SetBool("takeoff", false);
            bluemorpho.SetBool("landing", false);
            bluemorpho.SetBool("idle", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            bluemorpho.SetBool("idle",true);
            bluemorpho.SetBool("walk", false);
            bluemorpho.SetBool("eat", false);
            bluemorpho.SetBool("fly", false);
            bluemorpho.SetBool("flyinplace", true);
        }
        if (Input.GetKey(KeyCode.W))
        {
            bluemorpho.SetBool("walk", true);
            bluemorpho.SetBool("idle", false);
            bluemorpho.SetBool("eat", false);
            bluemorpho.SetBool("fly", true);
            bluemorpho.SetBool("flyinplace", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            bluemorpho.SetBool("turnleft", true);
            bluemorpho.SetBool("idle", false);
            bluemorpho.SetBool("walk", false);
            bluemorpho.SetBool("turnright", false);
            StartCoroutine("idle");
            idle();
        }
        if (Input.GetKey(KeyCode.D))
        {
            bluemorpho.SetBool("turnright", true);
            bluemorpho.SetBool("idle", false);
            bluemorpho.SetBool("walk", false);
            bluemorpho.SetBool("turnleft", false);
            StartCoroutine("idle");
            idle();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            bluemorpho.SetBool("takeoff", true);
            bluemorpho.SetBool("idle", false);
            bluemorpho.SetBool("eat", false);
            StartCoroutine("fly");
            fly();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            bluemorpho.SetBool("landing", true);
            bluemorpho.SetBool("fly", false);
            bluemorpho.SetBool("flyinplace", false);
        }
        if (Input.GetKey(KeyCode.E)){
            bluemorpho.SetBool("eat", true);
            bluemorpho.SetBool("walk", false);
            bluemorpho.SetBool("idle", false);
        }
	}
    IEnumerator idle()
    {
        yield return new WaitForSeconds(0.1f);
        bluemorpho.SetBool("idle", true);
        bluemorpho.SetBool("turnleft", false);
        bluemorpho.SetBool("turnright", false);
        bluemorpho.SetBool("walk", false);
        bluemorpho.SetBool("landing", false);
    }
    IEnumerator fly()
    {
        yield return new WaitForSeconds(0.1f);
        bluemorpho.SetBool("fly", true);
        bluemorpho.SetBool("takeoff", false);
    }
}

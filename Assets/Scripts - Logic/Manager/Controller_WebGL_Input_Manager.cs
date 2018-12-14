using UnityEngine;

using System;
using System.Collections.Generic;

public class Controller_WebGL_Input_Manager : MonoBehaviour 
{
    [Tooltip("Should WebGL send all inputs to the HTML page to the WebGL application?")]
    public bool captureAllInputs = true;

    void Awake()
    {
        WebGLInput.captureAllKeyboardInput = captureAllInputs;
    }

	void Start () {	}
	
	void Update () { }
}

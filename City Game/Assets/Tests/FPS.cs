using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public Text output;
    
	void Update ()
    {
        output.text = (1 / Time.deltaTime).ToString();
	}
}

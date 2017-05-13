using UnityEngine;
using System.Collections;

public class ShaderSwitch : MonoBehaviour {

    public Shader s;
	// Use this for initialization
	void Start () {
        Camera.main.SetReplacementShader(s, "RenderType");
    }
	

}

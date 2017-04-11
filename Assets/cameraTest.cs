using UnityEngine;
using System.Collections;

public class cameraTest : MonoBehaviour
{

    GameObject myCamera;
    WebCamTexture webcamTexture;
    int w = 640;
    int h = 480;
    public int resX = 4;
    public int resY = 4;
    int fSize = 12;
    TextMesh t;

    Texture2D[] tex;

    Color32[] pix;
    Renderer renderer;
    string ascii = "#@%$X&VU*+>})/:,.^-_";
    //string ascii = "# ";
    // Use this for initialization
    void Start()
    {
        t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
        myCamera = GameObject.Find("MainCamera");
        webcamTexture = new WebCamTexture(w,h,30);
        renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("cameratest w:" + w + " | h: " + h);
        // transform.rotation = Quaternion.Euler(90+myCamera.transform.eulerAngles.x, 180+myCamera.transform.eulerAngles.y, transform.rotation.z);


    }
}
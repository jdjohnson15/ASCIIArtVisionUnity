using UnityEngine;
using System.Collections;

public class videoToASCII : MonoBehaviour {

    GameObject myCamera;
    WebCamTexture webcamTexture;
    int w = 320;
    int h = 240;
    public int resX = 4;
    public int resY = 4;
    int fSize = 12;
    TextMesh t;

    bool sf = false;

    float srx = (.45f- 0.113f) / 12f;
    float sry = (.43f - 0.11f) / 12f;
    float pry = (-4.2f + 4.8f) / 12f;
    float si = 0;

    int srate = 15;
    int rate = 15;
    Texture2D[] tex;
    
    Color32[] pix;
    Renderer renderer;
    string ascii = "@%#*+=-:. ";
    //string ascii = "# ";
    // Use this for initialization
    void Start () {
        t = (TextMesh)gameObject.GetComponent(typeof(TextMesh));
        webcamTexture = new WebCamTexture(w,h,30);
       // w = webcamTexture.width;
       // h = webcamTexture.height;
       // renderer = GetComponent<Renderer>();
       // renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();

    }
	
	// Update is called once per frame
	void Update () {
       // transform.rotation = Quaternion.Euler(90+myCamera.transform.eulerAngles.x, 180+myCamera.transform.eulerAngles.y, transform.rotation.z);
        string textText = getText();
        t.text = textText;
        transform.localScale = new Vector3(0.113f + srx *si, 0.11f + sry*si, 0);
        transform.localPosition = new Vector3(5f,0f,-4.8f+ pry*si);
        //Debug.Log(si);
        
        if (--rate == 0)
        {
            if (sf)
            {
                --resX;
                --resY;
                --si;
                if (resX < 4)
                {
                    si = 0;
                    resX = 4;
                    resY = 4;
                    sf = false;
                }
            }
            else
            {

                ++resX;
                ++resY;
                ++si;
                if (resX > 16)
                {
                    si = 12;
                    resX = 16;
                    resY = 16;
                    sf = true;
                }
            }
            rate = srate;
        }
       

        //{0.113, 0.11},{.89, .89}
        //renderer.material.mainTexture.t;
       //Debug.Log(textText);

    }

    string get(float l)
    {
        // Log.i("l", Float.toString(l));
        int i = (int)((l / 255) * ascii.Length);
        string s = "";
      //  if (i > 10 && i < 19)
       // if (i < 7)
           // s = " ";
        if (i == ascii.Length)
            i -= ascii.Length;
        return ascii[i] + s;
    }

    string getText()
    {
        float pixel_chunk;
        Color32[] color = webcamTexture.GetPixels32();
        color = reverse(color);
        int x = 0, y = 0, k = 0;
        string output = "";
        float[] temp = new float[resX * resY];
        for (int v = 0; v < (h / resY); ++v)
        {
            for (int u = 0; u < (w / resX); ++u)
            {
                for (int j = 0; j < resY; ++j)
                {
                    for (int i = 0; i < resX; ++i)
                    {

                        temp[k] = convert(color[(y + j) * w + (x + i)]);
                        ++k;
                    }
                }
                x += resX;
                k = 0;
                pixel_chunk = average(temp);
                output += get(pixel_chunk); ;
            }
            x = 0;
            y += resY;
            // output = output.substring(0, output.length() - 2) + "|\n";
            output += "|\n";
        }
        x = 0;
        y = 0;
        k = 0;
        //System.out.println(output);
        //output += "END\n";
        return output;
    }

    float average(float[] array)
    {
        float total = 0.0f;
        for (int i = 0; i < array.Length; ++i)
        {
            total += array[i];
        }

        return total / array.Length;
    }

    float convert(Color32 c)
    {
        float[] a = { c.r, c.g, c.b };
        return average(a);
    }
    

    Color32[] reverse(Color32[] a)
    {
        Color32 temp;
        int j = a.Length-1;
        int index;
        bool done = false;
        for (int iy = 0; iy < h; ++iy)
        {
            for (int ix = 0; ix < w; ++ix)
            {
                index = iy * w + (w - ix - 1);
                temp = a[index];
                a[index] = a[j];
                a[j] = temp;
                --j;
                if (index > j)
                {
                    done = true;
                    break;
                }   
            }
            if (done)
                break;
        }
        return a;
    }

}

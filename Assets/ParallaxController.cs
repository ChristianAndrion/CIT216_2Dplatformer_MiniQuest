using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform cam;
    Vector3 cameraStartPos;
    float distance;

    GameObject[] backgrounds;
    Material[] mat;
    float[] bgSpeed;

    float farthestBG;

    [Range(0.001f,0.05f)]
    public float parallaxSpeed;

    //Following code is from a YT tutorial
    void Start()
    {
        
        cam = Camera.main.transform;
        cameraStartPos = cam.position;

        int bgCount = transform.childCount;
        mat = new Material[bgCount];
        bgSpeed = new float[bgCount];
        backgrounds = new GameObject[bgCount];

        for(int i = 0; i < bgCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalc(bgCount);
    }


    void BackSpeedCalc(int bgCount)
    {
        for (int i = 0; i < bgCount; i++)
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBG)
            {
                farthestBG = backgrounds[i].transform.position.z - cam.position.z;
            }
        }
        for(int i =0; i < bgCount; i++)
        {
            bgSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z);
        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - cameraStartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = bgSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * -speed);
        }

    }
}

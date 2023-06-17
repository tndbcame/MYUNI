using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
    public GameObject tamagokakegohan;
    public GameObject siroigohan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Transform tamagokake = tamagokakegohan.transform;
            Transform sirogohan = siroigohan.transform;

            Vector2 postama = tamagokake.position;
            Vector2 possiro = sirogohan.position;
            sirogohan.position = postama;
            tamagokake.position = possiro;
        }
    }
}

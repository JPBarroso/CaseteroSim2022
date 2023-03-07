using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoSession : MonoBehaviour
{
    public GameObject[] muebles;
    public Camera main;
    public Light[] luces;
    // Start is called before the first frame update
    void Start()
    {
        muebles = GameObject.FindGameObjectsWithTag("Furniture");
        Debug.Log("Añadidos " + muebles.Length + " muebles a la cuenta y esperando...");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine(TomarFoto());
        }
    }
    IEnumerator TomarFoto()
    {
        for (int i = 0; i < muebles.Length; i++)
        {
            muebles[i].transform.position = new Vector3(0, muebles[i].transform.position.y, muebles[i].transform.position.z);
            yield return new WaitForEndOfFrame();
            ScreenCapture.CaptureScreenshot(Application.dataPath + "/capturas/" + muebles[i].transform.name + ".png");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Tomada foto de " + muebles[i].transform.name);
            muebles[i].transform.position = new Vector3(30, muebles[i].transform.position.y, muebles[i].transform.position.z);
        }
    }
}

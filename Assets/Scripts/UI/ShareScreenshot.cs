using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ShareScreenshot : MonoBehaviour
{
    public GameObject[] botones;
    public string sujeto;
    public string mensaje;
    bool isTakingScreenshot = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void Capturar()
    {
       if(isTakingScreenshot == false)
        {
            StartCoroutine(CapturaPantalla());
        }
    }
    IEnumerator CapturaPantalla()
    {
        isTakingScreenshot = true;
        NativeShare myNS = new NativeShare();
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].SetActive(false);
        }
        yield return new WaitForEndOfFrame();
        Texture2D tx = new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);
        tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tx.Apply();
        string path = Path.Combine(Application.temporaryCachePath, "sharedimage.png");
        File.WriteAllBytes(path, tx.EncodeToPNG());
        Destroy(tx);
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].SetActive(true);
        }
        new NativeShare().AddFile(path).SetSubject(sujeto).SetText(mensaje).Share();
        isTakingScreenshot = false;
    }
}

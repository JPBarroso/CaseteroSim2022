using UnityEngine;
using TMPro;

public class InputTextChanger : MonoBehaviour
{
    [SerializeField] private TMP_InputField casetaInputNameText;
    private string casetaTxt;


    private void Start()
    {
        casetaInputNameText.text = ES3.FileExists(SaveAndLoadManager.FileName) ? CasetaNameLoad() : "Caseta Feria";
    }

    public void SetCasetaString()
    {
        casetaTxt = casetaInputNameText.text;
        ES3.Save("CasetaName", casetaTxt, SaveAndLoadManager.FileName);
    }

    private string CasetaNameLoad()
    {
        return ES3.Load<string>("CasetaName", SaveAndLoadManager.FileName);
    }
    
}

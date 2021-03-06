using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZXing;

public class ScannerBehaviour : MonoBehaviour
{


    [SerializeField]
    private RawImage RawImageBackground;
    [SerializeField]
    private AspectRatioFitter AspectRatioFilter;
    [SerializeField]
    private TextMeshProUGUI textOut;
    [SerializeField]
    private RectTransform scanZone;

    private bool isCanvasAvailable;
    private WebCamTexture CamTexture;

    // Start is called before the first frame update
    void Start()
    {
        SetUpCamera();
    }

    // Update is called once per frame
    void Update()
    {

        UpdateCameraRenderer();
        Scan();
    }

    private void UpdateCameraRenderer()
    {
        if(!isCanvasAvailable)
        {
            return;
        }
        AspectRatioFilter.aspectRatio = (float)CamTexture.width / (float)CamTexture.height;
        RawImageBackground.rectTransform.localEulerAngles = new Vector3(0,180, CamTexture.videoRotationAngle + 180);
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices; // looks for cameras on devices and puts them into a list
        if(devices.Length == 0)//if there are no cameras end
        {
            isCanvasAvailable = false;
            return;
        }
        for(int i = 0; i < devices.Length; i++) //if there's a front facing camera set it to the current camera
        {
            if (devices[i].isFrontFacing == false)
            {
                CamTexture = new WebCamTexture(devices[i].name, (int)scanZone.rect.width, (int)scanZone.rect.height);
            }

            
            CamTexture.Play();
            
            RawImageBackground.texture = CamTexture;

            isCanvasAvailable = true;
        }
    }

    public void OnClickScan()
    {
        Scan();
    }
    private void Scan()
    {
        string meaning = null;
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(CamTexture.GetPixels32(), CamTexture.width, CamTexture.height);
            if(result != null)
            {
                textOut.text = result.Text + " activated!";
                meaning = result.Text;
            }
            else
            {
                textOut.text = " ";
            }
        }
        catch
        {
            textOut.text = " ";
        }

        switch (meaning) //kaboom, matching, frogger, circuit
        {
            case "meditation":
                PlayerPrefs.SetString("Current", "meditation");
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Kaboom Minigame");
                Invoke("LoadKaboom", 1f);
                //kaboom
                break;
            case "feline":
                PlayerPrefs.SetString("Current", "feline");
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Matching Minigame");
                Invoke("LoadMatching", 1f);
                //matching
                break;
            case "disaster":
                PlayerPrefs.SetString("Current", "disaster");
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Frogger");
                Invoke("LoadFrogger", 1f);
                //frogger
                break;
            case "bellissimo":
                PlayerPrefs.SetString("Current", "bellissimo");
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Circuit Minigame 1");
                Invoke("LoadCircuit1", 1f);
                //circuit
                break;
            case "astrofacts":
                PlayerPrefs.SetString("Current", "astrofacts");
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Kaboom Minigame");
                Invoke("LoadKaboom", 1f);
                //kaboom
                break;
            case "constellation":
                PlayerPrefs.SetString("Current", "constellation");
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Matching Minigame");
                Invoke("LoadMatching", 1f);
                //matching
                break;
            case "mismatched":
                PlayerPrefs.SetString("Current", "mismatched");
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Frogger");
                Invoke("LoadFrogger", 1f);
                //frogger
                break;
            case "cannon":
                PlayerPrefs.SetString("Current", "cannon");
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Circuit Minigame 1");
                Invoke("LoadCircuit1", 1f);
                //circuit
                break;
            case "rhythm":
                PlayerPrefs.SetString("Current", "rhythm");
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Frogger");
                Invoke("LoadFrogger", 1f);
                //frogger
                break;
            case "theia":
                PlayerPrefs.SetString("Current", "theia");
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Circuit Minigame 1");
                Invoke("LoadCircuit1", 1f);
                //circuit
                break;
            default:
                textOut.text = "SEARCHING";
                break;
        
        }

    }

    private void LoadCircuit1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Circuit Minigame 1");
    }
    private void LoadFrogger()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Frogger");
    }
    private void LoadMatching()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Matching Minigame");
    }
    private void LoadKaboom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Kaboom Minigame");
    }
}

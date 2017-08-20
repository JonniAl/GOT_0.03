using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    private float fadeIn = 1;
    private float fadeOut = 0;

    private float fadeSpeed = 0.7f;
    private float transparency;

    private Image fadeImage;

	private void Awake ()
    {
        transparency = fadeIn;
        fadeImage = GetComponent<Image>();

        fadeImage.color = new Color(0, 0, 0, fadeIn);

        GameManager.FaderIn += FadeIn;
        GameManager.FaderOut += FadeOut;
	}

    private void Update()
    {
        fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(fadeImage.color.a, transparency, fadeSpeed * Time.deltaTime));
        //Debug.Log(fadeImage.color);
    }

    private void FadeOut()
    {
        //Debug.Log("FaderOut");

        transparency = fadeOut;
        fadeSpeed = 0.7f;
    }

    private void FadeIn()
    {
        //Debug.Log("FaderIn");

        transparency = fadeIn;
        fadeSpeed = 1.5f;
    }

    public static void InitializeFader()
    {
        GameObject fader = (GameObject)Instantiate(Resources.Load("Prefabs/ScreenFader"));

        fader.name = "ScreenFader";
    }
}
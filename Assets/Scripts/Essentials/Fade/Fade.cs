using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    bool fading;
    bool in_;
    bool out_;

    TMP_Text text;
    Image image;

    [SerializeField] Color imgColor;
    [SerializeField] Color txtColor;

    [SerializeField] float speedInImg = 2.4f;
    [SerializeField] float speedInTxt = 2.4f;
    [SerializeField] float speedOutImg = 2.0f;
    [SerializeField] float speedOutTxt = 2.0f;

    const float epsilon = 0.05f;

    public bool In_
    {
        get { return in_; }
        set { in_ = value; }
    }
    public bool Out_
    {
        get { return out_; }
        set { out_ = value; }
    }

    /// <summary>
    /// Get images component if it is not yet set
    /// </summary>
    void Start()
    {
        if (gameObject.HasComponent<Image>())
            image = GetComponent<Image>();

        if (gameObject.HasComponent<TMP_Text>())
            text = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Trigger fade with in or out mode
    /// </summary>
    /// <param name="fadeType">true to fadeIn and false to fadeOut</param>
    /// <param name="delay">wait time in seconds to start the fade</param>
    public void Trigger(bool fadeType, float delay = 0)
    {
        In_ = fadeType;
        Out_ = !fadeType;
        PlayFade(delay);
    }

    // To try demo call this function on Update
    // Display for a fixed time a message
    void PlayFade(float delay = 0)
    {
        if (In_)
            StartCoroutine(StartFade(delay, true));

        if (Out_)
            StartCoroutine(StartFade(delay, false));
    }

    /// <summary>
    /// Hide a sprite and text using fade effect
    /// </summary>
    void HideMessage()
    {
        Color alpha = new Color(1, 1, 1, 0);
        if (image) image.color = Color.Lerp(image.color, alpha, speedOutImg * Time.deltaTime);
        if (text) text.color = Color.Lerp(text.color, alpha, speedOutTxt * Time.deltaTime);
    }

    /// <summary>
    /// Show a sprite and text using fade effect
    /// </summary>
    void ShowMessage()
    {
        if (image) image.color = Color.Lerp(image.color, imgColor, speedInImg * Time.deltaTime);
        if (text) text.color = Color.Lerp(text.color, txtColor, speedInTxt * Time.deltaTime);
    }

    /// <summary>
    /// Coroutine to apply fadeIn effect a sprite and text
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIn()
    {
        fading = true;
        if (image) image.enabled = true;
        if (text) text.enabled = true;
        do
        {
            if (text)
            {
                if ((image && image.color.a < imgColor.a - epsilon) || (text && text.color.a < txtColor.a - epsilon))
                {
                    ShowMessage();
                    yield return null;
                }

                else
                {
                    fading = false;
                    yield break;
                }
            }

            else {
                if (image.color.a < imgColor.a - epsilon)
                {
                    ShowMessage();
                    yield return null;
                }

                else
                {
                    fading = false;
                    yield break;
                }
            }

        } while (true);
    }

    /// <summary>
    /// Coroutine to apply fadeOut effect a sprite and text
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOut()
    {
        fading = true;
        do
        {
            if (text)
            {
                if (text && text.color.a >= epsilon)
                {
                    HideMessage();
                    yield return null;
                }
                else
                {
                    fading = false;
                    text.enabled = false;
                    yield break;
                }
            }

            if (image)
            {
                if (image.color.a >= epsilon)
                {
                    HideMessage();
                    yield return null;
                }
                else
                {
                    fading = false;
                    image.enabled = false;
                    yield break;
                }
            }

        } while (true);
    }

    /// <summary>
    /// Wait For Seconds and fade ui element
    /// </summary>
    /// <param name="seconds">seconds to wait</param>
    /// <param name="fadeInOrOut">type of fade: true to fade in and false to fade out</param>
    /// <returns></returns>
    IEnumerator StartFade(float seconds, bool fadeInOrOut)
    {
        yield return new WaitForSeconds(seconds);
        if (!fading) StartCoroutine(fadeInOrOut ? FadeIn() : FadeOut());
        yield break;
    }
}

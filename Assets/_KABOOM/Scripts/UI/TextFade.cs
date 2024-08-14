using UnityEngine;
using TMPro;
using System.Collections;
public class TextFade : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _fadeText;

    private float _fadeTimeMultiplier = 1.5f;

    private bool _isFading = false;

    private void Update()
    {
        if(_isFading == false)
        {
            StartCoroutine(FadeInText(_fadeTimeMultiplier, _fadeText));
        }
    }

    private IEnumerator FadeInText(float speed, TextMeshProUGUI text)
    {
        _isFading = true;

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0); // Text alpha starts at 0 (invisible)

        while(text.color.a < 1.0f) // While the alpha is less that 1 (completely opaque)...
        {
            //... continually update the alpha until it's at 1
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * speed));

            yield return null;
        }
    }
}

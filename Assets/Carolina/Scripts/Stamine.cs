using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stamine : MonoBehaviour
{
    public Image chargeImage;
    public GameObject chargeCanvas;
    public float maxChargeTime = 3f;
    public float decaySpeed = 1f; // velocidad de vaciado
    public float delayBeforeHide = 0.5f; // tiempo de espera antes de ocultar el canvas

    private float currentCharge = 0f;
    private bool isCharging = false;
    private bool isDraining = false;

    void Start()
    {
        chargeCanvas.SetActive(false);
    }

    void Update()
{
    bool spacePressed = Input.GetKey(KeyCode.Space);
    bool spaceDown = Input.GetKeyDown(KeyCode.Space);
    bool spaceUp = Input.GetKeyUp(KeyCode.Space);

    if (spaceDown)
    {
        isCharging = true;
        isDraining = false;
        StopAllCoroutines();
        chargeCanvas.SetActive(true);
    }

    if (isCharging && spacePressed)
    {
        currentCharge += Time.deltaTime;
        currentCharge = Mathf.Clamp(currentCharge, 0f, maxChargeTime);
        chargeImage.fillAmount = currentCharge / maxChargeTime;
    }

    if (spaceUp)
    {
        isCharging = false;
        isDraining = true;
        StartCoroutine(DrainBar());
    }
}

    IEnumerator DrainBar()
    {
        while (currentCharge > 0f)
        {
            currentCharge -= Time.deltaTime * decaySpeed;
            currentCharge = Mathf.Max(currentCharge, 0f);
            chargeImage.fillAmount = currentCharge / maxChargeTime;
            yield return null;
        }

        yield return new WaitForSeconds(delayBeforeHide);
        chargeCanvas.SetActive(false);
    }
}
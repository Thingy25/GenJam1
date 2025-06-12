using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject character1;
    public GameObject character2;

    private bool isCharacter1Active = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character1.SetActive(true);
        character2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCharacter();
        }
    }

    void SwitchCharacter()
    {
        isCharacter1Active = !isCharacter1Active;

        character1.SetActive(isCharacter1Active);
        character2.SetActive(!isCharacter1Active);
    }
}

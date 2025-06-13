using UnityEngine;

public class CharacterManagerWilson : MonoBehaviour
{
    // Variables
    public enum CharacterType { one, two };
    public CharacterType selectedCharacter;
    private CharacterControllerWilson currentController;

    [SerializeField] private CharacterControllerWilson characterOneControllerScript;
    [SerializeField] private CharacterControllerWilson characterTwoControllerScript;

    void Awake()
    {
        characterOneControllerScript = GetComponent<CharacterOneControllerWilson>();
        characterTwoControllerScript = GetComponent<CharacterTwoControllerWilson>();

        currentController = characterOneControllerScript;
        currentController.enabled = true;

        characterTwoControllerScript.enabled = false;
    }

    public void ToggleCharacter()
    {
        // Alternar entre tipos
        selectedCharacter = (selectedCharacter == CharacterType.one) ? CharacterType.two : CharacterType.one;
        SwapCharacter(selectedCharacter);
    }

    private void SwapCharacter(CharacterType type)
    {
        // Desactiva el script actual si existe
        if (currentController != null)
        {
            currentController.enabled = false;
        }

        // Activa el script segun el tipo de personaje seleccionado
        switch (type)
        {
            case CharacterType.one:
                currentController = characterOneControllerScript;
                currentController.enabled = true;
                break;
            case CharacterType.two:
                currentController = characterTwoControllerScript;
                currentController.enabled = true;
                break;
        }
    } 
}

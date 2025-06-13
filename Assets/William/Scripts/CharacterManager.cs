using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // Variables
    public enum CharacterType { one, two };
    public CharacterType selectedCharacter;
    private CharacterController currentController;

    [SerializeField] private CharacterController characterOneControllerScript;
    [SerializeField] private CharacterController characterTwoControllerScript;

    void Awake()
    {
        characterOneControllerScript = GetComponent<CharacterOneController>();
        characterTwoControllerScript = GetComponent<CharacterTwoController>();

        currentController = characterTwoControllerScript;
        currentController.enabled = true;
        selectedCharacter = CharacterType.two;

        characterOneControllerScript.enabled = false;
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

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InputField loadFileNameInput;

    // Load 버튼 클릭 시 호출
    public void OnLoadButtonClick()
    {
        string fileName = loadFileNameInput.text;
        GameManager.Instance.LoadGameButton(fileName);
    }

    // Save 버튼 클릭 시 호출
    public void OnSaveButtonClick()
    {
        GameManager.Instance.SaveGameButton();
    }
}
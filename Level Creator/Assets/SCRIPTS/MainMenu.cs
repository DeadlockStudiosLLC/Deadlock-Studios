using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour {
    [SerializeField] MapEditorManager _mapEditorManager;

    [SerializeField] TMPro.TMP_InputField _heightField;
    [SerializeField] TMPro.TMP_InputField _widthField;

    [SerializeField] TMPro.TMP_Dropdown _loadDropdown;

    public void EnterEditorPressed() {
        //if(_loadDropdown.)
        _mapEditorManager.mapHeight = int.Parse(_heightField.text);
        _mapEditorManager.mapWidth = int.Parse(_widthField.text);
        _mapEditorManager.mapSize = _mapEditorManager.mapHeight * _mapEditorManager.mapWidth;
    }

    public void QuitApplication() {
        Application.Quit();
    }
}

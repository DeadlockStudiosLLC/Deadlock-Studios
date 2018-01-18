using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorManager : MonoBehaviour {
    int _mapSize;
    public int mapSize { get { return _mapSize; } set { _mapSize = value; } }
    int _mapHeight;
    public int mapHeight { get { return _mapHeight; } set { _mapHeight = value; } }
    int _mapWidth;
    public int mapWidth { get { return _mapWidth; } set { _mapWidth = value; } }

    string _mapName;
    public string mapName { get { return _mapName; } set { _mapName = value; } }
    string _savePath;
    public string savePath { get { return _savePath; } set { _savePath = value; } }

    private void Awake() {
        DontDestroyOnLoad(this);
    }
}

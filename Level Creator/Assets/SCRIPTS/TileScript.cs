using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour{

    public enum enumTileType { None, WaterTile, SandTile, GrassTile, RockTile};

    public bool isPassable = true;
    public bool canMoveTo = false;
	public bool inList = false;
	public bool inMaxList = false;
    public bool isWater = false;

	[SerializeField]private int tileDifficulty = 1;
    [SerializeField] private int addedDifficulty = 0;
    [SerializeField]Transform tileOffsetPOS;

	[SerializeField]enumTileType _tileType;
	public enumTileType tileType { get { return _tileType; } set { _tileType = value; } }
    [SerializeField] ObjectManager.enumObjType _objType;
    public ObjectManager.enumObjType objType { get { return _objType; } set { _objType = value; } }

    [System.Serializable]public struct MapPositionStruct
    {
        public int tileX;
        public int tileZ;
        public int tileID;
        public float tileHeight;
    }
    [SerializeField]private MapPositionStruct mapPosition;

    [System.Serializable]public struct NeighborsStruct
    {
        public GameObject[] nodes;
        public int remaingMoves;
        public TileScript parent;
    }
    [SerializeField]private NeighborsStruct neighbors;

	public void OfflineInitNeighborsStruct(){
		neighbors.nodes = new GameObject[6];
	}
    
    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        neighbors.nodes = new GameObject[6];

        object[] data = this.gameObject.GetPhotonView().instantiationData;
        if (data != null)
        {
            ResetTileTexture(((float)data.GetValue(0)));
        }
	}

	public Vector3 GetTileOffsetPOSCube(){
		return tileOffsetPOS.position;
	}
	public void SetTileOffsetPOSCube(Vector3 newPOS){
		tileOffsetPOS.position = newPOS;
	}

    public void SetNeighbor(int index, GameObject id)
    {
		neighbors.nodes [index] = id;
    }

    public void SetRemainingMoves(int _moves)
    {
        neighbors.remaingMoves = _moves;
    }

    public void SetParent(TileScript _parent)
    {
        neighbors.parent = _parent;
    }

    public NeighborsStruct GetNeighbors()
    {
        return neighbors;
    }

    public void SetMapPosition(int x, float height, int z, int id)
    {
        mapPosition.tileX = x;
        mapPosition.tileHeight = height;
        mapPosition.tileZ = z;
        mapPosition.tileID = id;
    }

    public MapPositionStruct GetMapPosition()
    {
        return mapPosition;
    }
    
    public int GetTileDifficulty()
    {
        return tileDifficulty;
    }

    public int GetAddedDifficulty() {
        return addedDifficulty;
    }

    public void SetTileID(int _id)
    {
        mapPosition.tileID = _id;
    }

    public void SetPassible(bool _pass)
    {
        isPassable = _pass;
    }

    public void SetDifficulty(int _diff)
    {
        tileDifficulty = _diff;
    }

    public void SetAddedDifficulty(int _diff) {
        addedDifficulty = _diff;
    }

    public void ResetTileTexture(float _height)
    {
        if (_height == 0)
        {
            this.GetComponent<MeshRenderer>().material = GameObject.FindGameObjectWithTag("Map").GetComponent<MapManager>().waterMaterialLow;
        }
        else if (_height == 0.2f)
        {
            this.GetComponent<MeshRenderer>().material = GameObject.FindGameObjectWithTag("Map").GetComponent<MapManager>().sandMaterial;
		}
		else if (_height == 0.4f)
		{
			if(tileDifficulty == 1)
				this.GetComponent<MeshRenderer>().material = GameObject.FindGameObjectWithTag("Map").GetComponent<MapManager>().grassMaterial;
			else
				this.GetComponent<MeshRenderer>().material = GameObject.FindGameObjectWithTag("Map").GetComponent<MapManager>().waterMaterialHigh;
		}
        else
		{
			this.GetComponent<MeshRenderer>().material = GameObject.FindGameObjectWithTag("Map").GetComponent<MapManager>().rockMaterial;
        }
    }

    [PunRPC]
    public void SetNetworkTiles(int _viewID, int _tileID, int _height, int _difficulty, bool _passable, 
        GameObject _neighbor0, GameObject _neighbor1, GameObject _neighbor2, GameObject _neighbor3,
        GameObject _neighbor4, GameObject _neighbor5)
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        GameObject go = null;

        for (int index = 0; index < tiles.Length; ++index)
        {
            if(tiles[index].GetComponent<PhotonView>().viewID == _viewID)
            {
                go = tiles[_tileID];
                go.GetComponent<TileScript>().SetTileID(_tileID);

                go.GetComponent<TileScript>().SetNeighbor(0, _neighbor0);
                go.GetComponent<TileScript>().SetNeighbor(1, _neighbor1);
                go.GetComponent<TileScript>().SetNeighbor(2, _neighbor2);
                go.GetComponent<TileScript>().SetNeighbor(3, _neighbor3);
                go.GetComponent<TileScript>().SetNeighbor(4, _neighbor4);
                go.GetComponent<TileScript>().SetNeighbor(5, _neighbor5);

                go.GetComponent<TileScript>().SetPassible(_passable);
                go.GetComponent<TileScript>().SetDifficulty(_difficulty);
            }
        }
    }
}

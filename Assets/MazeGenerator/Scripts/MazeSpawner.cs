using System.Diagnostics;
using UnityEngine;
using System.Collections;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour {
	public enum MazeGenerationAlgorithm{
		PureRecursive,
		RecursiveTree,
		RandomTree,
		OldestTree,
		RecursiveDivision,
	}

	bool comparevectors(Vector3 v1, Vector3 v2){
		float tmp = (v1.x - v2.x > 0) ? v1.x - v2.x : v2.x - v1.x;
		if(tmp > 0.0001){
			return false;
		}
		tmp = (v1.y - v2.y > 0) ? v1.y - v2.y : v2.y - v1.y;
		if(tmp > 0.0001){
			return false;
		}
		tmp = (v1.z - v2.z > 0) ? v1.z - v2.z : v2.z - v1.z;
		if(tmp > 0.0001){
			return false;
		}

		return false;
	}

	public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
	public bool FullRandom = false;
	public int RandomSeed = 12345;
	public GameObject Floor = null;
	public GameObject Wall = null;
	public GameObject Pillar = null;
	public GameObject Unseen = null;
	public int Rows = 5;
	public int Columns = 5;
	public float CellWidth = 5;
	public float CellHeight = 5;
	public bool AddGaps = true;

	public Renderer rend;

	//public GameObject GoalPrefab = null;

	private BasicMazeGenerator mMazeGenerator = null;

	void Start () {
		float xm = (Columns)*(CellWidth+(AddGaps?.2f:0)) - CellWidth/2;
		float zm = (Rows)*(CellHeight+(AddGaps?.2f:0)) - CellWidth/2;

		Vector3 posL = new Vector3(0f,1f,zm);
		Vector3 posR = new Vector3(xm,1f,0);

		if (!FullRandom) {
			Random.seed = RandomSeed;
		}
		switch (Algorithm) {
		case MazeGenerationAlgorithm.PureRecursive:
			mMazeGenerator = new RecursiveMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveTree:
			mMazeGenerator = new RecursiveTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RandomTree:
			mMazeGenerator = new RandomTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.OldestTree:
			mMazeGenerator = new OldestTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveDivision:
			mMazeGenerator = new DivisionMazeGenerator (Rows, Columns);
			break;
		}
		mMazeGenerator.GenerateMaze ();
		int wallcount = 0;
		for (int row = 0; row < Rows; row++) {
			for(int column = 0; column < Columns; column++){
				float x = column*(CellWidth+(AddGaps?.2f:0));
				float z = row*(CellHeight+(AddGaps?.2f:0));
				MazeCell cell = mMazeGenerator.GetMazeCell(row,column);
				GameObject tmp;
				tmp = Instantiate(Floor,new Vector3(x,0,z), Quaternion.Euler(0,0,0)) as GameObject;
				tmp.transform.parent = transform;
				tmp = Instantiate(Unseen,new Vector3(x,15,z), Quaternion.Euler(0,0,0)) as GameObject;
				tmp.transform.parent = transform;
				wallcount = 0;
        		int counter = 0;
				if(cell.WallRight){
					tmp = Instantiate(Wall,new Vector3(x+CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,90,0)) as GameObject;// right
					tmp.transform.parent = transform;
					wallcount++;
          			counter += 1;
				}
				if(cell.WallFront){
					tmp = Instantiate(Wall,new Vector3(x,0,z+CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,0,0)) as GameObject;// front
					tmp.transform.parent = transform;
					wallcount++;
          			counter += 2;
				}
				if(cell.WallLeft){
					tmp = Instantiate(Wall,new Vector3(x-CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,270,0)) as GameObject;// left
					tmp.transform.parent = transform;
					wallcount++;
          			counter += 1;
				}
				if(cell.WallBack){
					tmp = Instantiate(Wall,new Vector3(x,0,z-CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;// back
					tmp.transform.parent = transform;
					wallcount++;
          			counter += 2;
				}
				/*
				if(cell.IsGoal && GoalPrefab != null){
					tmp = Instantiate(GoalPrefab,new Vector3(x,1,z), Quaternion.Euler(0,0,0)) as GameObject;
					tmp.transform.parent = transform;
				}*/

				if(wallcount != 2){
					tmp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
					// tmp.GetComponent<SphereCollider>().isTrigger = true;
					tmp.transform.parent = transform;
					tmp.transform.position = new Vector3(x,1,z);
					tmp.transform.position = new Vector3(x,1,z);
					tmp.tag = "Path";
					rend = tmp.GetComponent<Renderer>();
					rend.enabled = false;
				}
				else{
				if((counter % 2) != 0){
					tmp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
					// tmp.GetComponent<SphereCollider>().isTrigger = true;
					tmp.transform.parent = transform;
					tmp.transform.position = new Vector3(x,1,z);
					tmp.tag = "turn";
					rend = tmp.GetComponent<Renderer>();
					rend.enabled = false;
					}
				}

			}
		}
		if(Pillar != null){
			for (int row = 0; row < Rows+1; row++) {
				for (int column = 0; column < Columns+1; column++) {
					float x = column*(CellWidth+(AddGaps?.2f:0));
					float z = row*(CellHeight+(AddGaps?.2f:0));
					GameObject tmp = Instantiate(Pillar,new Vector3(x-CellWidth/2,0,z-CellHeight/2),Quaternion.identity) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}


		float dist = Mathf.Infinity;
		GameObject nearest;


		Collider[] colsL = Physics.OverlapSphere(posL, 0.1f);
		Collider[] colsR = Physics.OverlapSphere(posR, 0.1f);


		foreach (Collider col in colsL){
		float d = Vector3.Distance(posL, col.transform.position);
		if (d < dist){ 
			dist = d; 
			nearest = col.gameObject;
			Destroy(nearest);
		}  
		}

		dist = Mathf.Infinity;

		foreach (Collider col in colsR){
		float d = Vector3.Distance(posR, col.transform.position);
		if (d < dist){
			dist = d;
			nearest = col.gameObject; 
			Destroy(nearest);
		}
		}

	}

	void Update(){

		float xm = (Columns)*(CellWidth+(AddGaps?.2f:0)) - CellWidth/2;
		float zm = (Rows)*(CellHeight+(AddGaps?.2f:0)) - CellWidth/2;


		Collider[] hitColliders = Physics.OverlapSphere(new Vector3(0f,0.5f,zm-CellWidth/2), 2f);
        foreach (var hitCollider in hitColliders)
        {
			if(hitCollider.gameObject.tag == "turn"){
				//UnityEngine.Debug.Log("found");
            	hitCollider.gameObject.tag = "Path";
			}
        }


		hitColliders = Physics.OverlapSphere(new Vector3(xm-CellWidth/2,0.5f,0f), 2f);
        foreach (var hitCollider in hitColliders)
        {
			if(hitCollider.gameObject.tag == "turn"){
				//UnityEngine.Debug.Log("found");
            	hitCollider.gameObject.tag = "Path";
			}
        }

		
		
	}
}

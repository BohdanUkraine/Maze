using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AIControl : MonoBehaviour
{
	private static float delta = 0.1f;
	Vector3 destination = new Vector3(100,100,100);
	float iterator = 0f;
	float speed = 0.5f;
	bool onplace = true;

	enum dir{
		front,
		right,
		left,
		back,
		none
	}

	dir previous = dir.none;
	

	void getdirections(dir[] directions, Vector3[] arr){

		Ray ray = new Ray(this.transform.position, new Vector3(0, 0, 1));
		Ray ray1 = new Ray(this.transform.position, new Vector3(0, 0, -1));
		Ray ray2 = new Ray(this.transform.position, new Vector3(-1, 0, 0));
		Ray ray3 = new Ray(this.transform.position, new Vector3(1, 0, 0));
		RaycastHit hit; 
		int i = 0;

    	if (Physics.Raycast(ray, out hit)){
    		if (hit.transform.gameObject.tag == "turn" || hit.transform.gameObject.tag == "Path"){
    		    //Debug.Log("path");
    		    directions[i] = dir.back;
    		    arr[i] = hit.transform.position;
    		    ++i;
    		}
            Debug.DrawLine(ray.origin, hit.point, Color.blue);
			//Debug.Log("back?");
    	}

        if (Physics.Raycast(ray1, out hit)){
        	if (hit.transform.gameObject.tag == "turn" || hit.transform.gameObject.tag == "Path"){
    		    //Debug.Log("path");
    		    directions[i] = dir.front;
    		    arr[i] = hit.transform.position;
    		    ++i;
    		}
            Debug.DrawLine(ray1.origin, hit.point, Color.blue);
			//Debug.Log("front?");
    	}

        if (Physics.Raycast(ray2, out hit)){
        	if (hit.transform.gameObject.tag == "turn" || hit.transform.gameObject.tag == "Path"){
    		    //Debug.Log("path");
    		    directions[i] = dir.right;
    		    arr[i] = hit.transform.position;
    		    ++i;
    		}
            Debug.DrawLine(ray2.origin, hit.point, Color.blue);
			//Debug.Log("right?");
        }

        if (Physics.Raycast(ray3, out hit)){
        	if (hit.transform.gameObject.tag == "turn"  || hit.transform.gameObject.tag == "Path"){
    		    //Debug.Log("path");
    		    directions[i] = dir.left;
    		    arr[i] = hit.transform.position;
    		    ++i;
    		}
            Debug.DrawLine(ray3.origin, hit.point, Color.blue);
			//Debug.Log("left?");
        }

	}

	int find(dir[] a, dir b){
		int i = 0;
		foreach(dir tmp in a){
			//Debug.Log(tmp + "   " + b + "   " + (b == tmp));
			if(b == tmp){
				return i;
			}
			i++;
		}
		return 88;
	}


	void setdirection(dir[] direction, Vector3[] vect, bool press = false){
		//Debug.Log("setdirection is called");
		int i = 0, n = 0;
        System.Random rnd = new System.Random();
        while(direction[n] != dir.none)
            n++;

        int r = rnd.Next(0, n);
		bool k = !timer.playerturn;
		if (press){
			i = find(direction, dir.front);
			if(i != 88 && previous != dir.front){
				//Debug.Log("front direction true");
				destination = vect[i];
				onplace = false;
				previous = dir.back;
				return;
			}
			i = find(direction, dir.back);
			if(i != 88 && previous != dir.back){
				//Debug.Log("back direction true");
				destination = vect[i];
				onplace = false;
				previous = dir.front;
				return;
			}
			i = find(direction, dir.left);
			if(i != 88 && previous != dir.left){
				//Debug.Log("left direction true");
				destination = vect[i];
				onplace = false;
				previous = dir.right;
				return;
			}
			i = find(direction, dir.right);
			if(i != 88 && previous != dir.right){
				//Debug.Log("right direction true");
				destination = vect[i];
				onplace = false;
				previous = dir.left;
				return;
			}
		}else 
		if (k){
			i = find(direction, dir.front);
			//Debug.Log("front " + i.ToString());
			if(i != 88 && i == r){
				//Debug.Log("front direction");
				destination = vect[i];
				onplace = false;
				previous = dir.back;
				return;
			}
			i = find(direction, dir.back);
			//Debug.Log("back " + i.ToString());
			if(i != 88 && i == r){
				//Debug.Log("back direction");
				destination = vect[i];
				onplace = false;
				previous = dir.front;
				return;
			}
			i = find(direction, dir.left);
			//Debug.Log("left " + i.ToString());
			if(i != 88 && i == r){
				//Debug.Log("left direction");
				destination = vect[i];
				onplace = false;
				previous = dir.right;
				return;
			}
			i = find(direction, dir.right);
			//Debug.Log("right " + i.ToString());
			if(i != 88 && i == r){
				//Debug.Log("right direction");
				destination = vect[i];
				onplace = false;
				previous = dir.left;
				return;
			}
		}
		
	}


	GameObject GO = null;
	Vector3 pos;

	void OnCollisionEnter(Collision collision) {
		//collision.gameObject.tag == "Path" 
		//Debug.Log("lol");

		if(Vector3.Distance(this.transform.position, destination) < 0.7)
			this.transform.position = destination;



		if (collision.gameObject.tag == "turn"){
			//If the GameObject has the same tag as specified, output this message in the console
			//Debug.Log("Collides with turn");
			dir[] directions = {dir.none, dir.none, dir.none}; 
			Vector3[] a = {new Vector3(100,100,100), new Vector3(100,100,100), new Vector3(100,100,100)};
			getdirections(directions, a);
			setdirection(directions, a, true);
			iterator = 0;

			pos = collision.gameObject.transform.position;
			GO = collision.gameObject;
			collision.gameObject.SetActive(false);
		}

		if (collision.gameObject.tag == "Path"){
			timer.playerturn = true;
            //Debug.Log("llllll");

			this.transform.position = destination;

				//Debug.Log("Collides with Path");
			destination = new Vector3(100,100,100);
			onplace = true;
			iterator = 0;

			pos = collision.gameObject.transform.position;
			GO = collision.gameObject;
			collision.gameObject.SetActive(false);
		}	
		
		if(this.transform.position.x != pos.x || this.transform.position.z != pos.z && GO != null)
			{
				Debug.Log(GO.gameObject.name);
				GO.SetActive(true);
			}
		else
			Debug.Log("KKKKKK");
	}


    void FixedUpdate()
    {
    	
		dir[] direction = new dir[5];

		for(int l=0; l<5; l++)
			direction[l] = dir.none;


		Vector3[] vect = new Vector3[5];
		int i = 0, n = 0;

		if(onplace)
			getdirections(direction, vect);

		while(direction[n] != dir.none)
			n++;

		if(onplace)
			setdirection(direction, vect);
		
		//moving
        // Debug.Log(onplace);
        // Debug.Log(iterator);
        // Debug.Log(destination);

		if (destination != new Vector3(100,100,100) && !onplace)
		{
			if	(this.transform.position.z < destination.z || this.transform.position.x < destination.x)
				iterator = speed;
			if	(this.transform.position.z > destination.z || this.transform.position.x > destination.x)
				iterator = -1 * speed;



			if (this.transform.position.x > destination.x - delta && this.transform.position.x < destination.x + delta && !onplace){
				this.transform.position = new Vector3 (this.transform.position.x, 
					this.transform.position.y, this.transform.position.z + iterator);
				//Debug.Log("x = x");
			}
			else
			if (this.transform.position.z > destination.z - delta && this.transform.position.z < destination.z + delta && !onplace){
				this.transform.position = new Vector3 (this.transform.position.x + iterator, 
					this.transform.position.y, this.transform.position.z);
				//Debug.Log("z = z " + iterator.ToString());
			}

		}

    }
	
}

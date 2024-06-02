//José Pablo Peañaloza Cobos
//17/SEPT/2021
//Script that has the indivudual tile behaviour.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IPooledObject
{
    private Endless2D   tileManager;

    private Vector3     direction;      //Stores the direction where the tile will move
    private float       speed;          //Speed of the tile
    private bool        shouldMove;     //Flag that tells the tile if it has to move

    int                 nRows;          //Stores the grid size
    int                 nColumns;

    private SpriteRenderer spriteRenderer;

    public void onObjectSpawn()
    {//Adds itself to the Tile manager
        tileManager = Endless2D.Instance;

        tileManager.OrderTilesToMove += moveTileInDirectionOf;
        Endless2D.availableTiles.Add(this);
        nRows = ((Endless2D)Endless2D.Instance).nRows;
        nColumns =   ((Endless2D)Endless2D.Instance).nColumns;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        changeTile();
    }

    public void OnDisable()
    {//Removes itself from the tile manager when disabled
        Endless2D.availableTiles.Remove(this);
    }


    public void moveTileInDirectionOf(Vector3 direction, float speed)
    {
        this.speed = speed;
        this.direction = direction;
        shouldMove = true;
        isOutOfBounds(tileManager.FirstTile, tileManager.LastTile);             //Checks if the tile is still in the grid.
        transform.Translate(direction * Time.deltaTime * speed, Space.World);   //Moves the tile
    }


    public bool isOutOfBounds(Vector3 start, Vector3 end)
    {
        float speedDirection = speed < 0 ? -1 : 1; //float that is either 1 or -1 for changing the direction if the speed is negative
        bool isOutofBounds = false;
        if (transform.position.x < start.x || transform.position.x > end.x)
        {
            isOutofBounds = true;
            transform.position =  transform.position - new Vector3(direction.x * nColumns, direction.y * nRows) * transform.localScale.x *speedDirection;
            changeTile();
        }
        if (transform.position.y > start.y || transform.position.y < end.y)
        {
            isOutofBounds = true;   
            transform.position = transform.position - new Vector3(direction.x * nColumns, direction.y * nRows) * transform.localScale.x * speedDirection;
            changeTile();
        }
        if (transform.position.z < start.z || transform.position.z > end.z)
        {//CORREGIR
            isOutofBounds = true;
            transform.position = transform.position - new Vector3(direction.x * nColumns, 0, direction.z * nRows) * transform.localScale.x * speedDirection;
            changeTile();
        }

        return isOutofBounds;
    }

    public void changeTile()
    {
        int rand = Random.Range(0, ((Endless2D)Endless2D.Instance).availableSprites.Length);
        spriteRenderer.sprite = ((Endless2D)Endless2D.Instance).availableSprites[rand];
    }
}

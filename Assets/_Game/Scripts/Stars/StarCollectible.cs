using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    public GameLogic gameLogic;
    private StarLevelDisplayer starLevelDisplayer;

    void Start() {
        gameLogic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        starLevelDisplayer = GameObject.Find("StarLevelDisplayer").GetComponent<StarLevelDisplayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == Planet.LAYER) {            
            if (true || collision.gameObject.GetComponent<Linkable>().isPlanet) {
                gameLogic.currStars++;
                starLevelDisplayer.UpdateStars(gameLogic.currStars);
                Destroy(gameObject);
            }
        }
    }
}

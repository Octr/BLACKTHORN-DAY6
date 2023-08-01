using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    private StarLevelDisplayer starLevelDisplayer;

    void Start() {
        starLevelDisplayer = GameObject.Find("StarLevelDisplayer").GetComponent<StarLevelDisplayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == Planet.LAYER) {            
            if (true || collision.gameObject.GetComponent<Linkable>().isPlanet) {
                GameLogic.Instance.currStars++;
                starLevelDisplayer.UpdateStars(GameLogic.Instance.currStars);
                Destroy(gameObject);
            }
        }
    }
}

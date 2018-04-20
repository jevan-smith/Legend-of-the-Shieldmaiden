using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class HeartPiece : MonoBehaviour
{
    public HeartUI _HeartUI;

    public int _HeartPieceAmount;

    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        _HeartUI = FindObjectOfType<HeartUI>();
    }

    void OnTriggerEnter2D(Collider2D _Other)
    {
        if (_Other.tag == "PlayerH")
        {
            _HeartUI.AddHeartPiece(_HeartPieceAmount);
            Destroy(gameObject);
        }


    }
}
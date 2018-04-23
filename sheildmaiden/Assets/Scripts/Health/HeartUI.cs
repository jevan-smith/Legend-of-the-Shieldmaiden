﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class HeartUI : MonoBehaviour
{

    public PlayerH _LocalPlayer;

    public List<HeartIcon> _HeartIcons;

    public int _HeartPieces;


    public int _MaxHeartsContainer = 20;

    public GameObject _HeartUIPiece;


    private int _DrawHeartPieces;

    public void Start()
    {
        UpdateHearts();
    }

    /// <summary>
    /// This function will be resposible for redrawing the list  and keeping track of current health and max health.
    /// </summary>
    void UpdateHearts()
    {
        _HeartIcons = GetComponentsInChildren<HeartIcon>().ToList();
        _LocalPlayer = FindObjectOfType<PlayerH>();
        _LocalPlayer._MaxHealth = (_HeartIcons.Count) * 4;
        _LocalPlayer._CurHealth = _LocalPlayer._MaxHealth;
    }
    public void Update()
    {


        DrawHearts();


    }

    /// <summary>
    /// This function is responsible for adding the heart pieces when picked up in game.
    /// </summary>
    /// <param name="_HeartsPiecesToAdd"></param>
    public void AddHeartPiece(int _HeartsPiecesToAdd)
    {
        if (_HeartIcons.Count >= _MaxHeartsContainer)
            return;

        _HeartPieces += _HeartsPiecesToAdd;

        if (_HeartPieces - 4 >= 0)
        {
            GameObject _TempObject = Instantiate(_HeartUIPiece, Vector3.zero, Quaternion.identity) as GameObject;

            _TempObject.transform.SetParent(this.transform);

            _HeartPieces -= 4;

            UpdateHearts();
        }

    }
    /// <summary>
    /// This is the function resposible for drawing the hearts and sending information to their animators.
    /// </summary>
    void DrawHearts()
    {
        //We will get the amount of hearts in the list and count to them in a loop
        for (int i = 1; i < _HeartIcons.Count + 1; i++)
        {

            //We store the value of the pieces of heart in the last partial filled heart
            _DrawHeartPieces = _LocalPlayer._CurHealth % 4;


            //If the current heart container is full 
            if (_LocalPlayer._CurHealth >= i * 4)
            {
                //Set the heart to full
                _HeartIcons[i - 1].SetHeartAnim(4);

            }

            else
            {
                //If the heartis empty set the image to empty
                if ((_LocalPlayer._CurHealth - ((i - 1) * 4)) <= 0)
                    _HeartIcons[i - 1].SetHeartAnim(0);
                else
                {

                    //Debug.Log("The value being sent to the heart " + i + "  is " + _HeartPieces);

                    //If none of the above the heart must be partially full so fill it
                    _HeartIcons[i - 1].SetHeartAnim(_DrawHeartPieces);
                }

            }


        }
    }
}
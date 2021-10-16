using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinTEXT : MonoBehaviour
{
   public static int coin;
   private Text _text;

   private void Start()
   {
      _text = GetComponent<Text>();
   }

   private void Update()
   {
      _text.text = coin.ToString();
   }
}

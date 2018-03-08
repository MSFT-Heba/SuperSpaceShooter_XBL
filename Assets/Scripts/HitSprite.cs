using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class HitSprite : MonoBehaviour
    {

        private void OnEnable()
        {
            Invoke("DisableSprite", .3f);
        }

        private void DisableSprite()
        {
            gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance;

        public GameObject SmallEnemyPrefab;
        public GameObject LargeEnemyPrefab;
        public GameObject EnemyLaserPrefab;
        public GameObject PlayerLaserPrefab;
        public GameObject EnemyHitIconPrefab;
        public GameObject PlayerHitIconPrefab;

        public enum ObjectType
        {
            EnemySmall,
            EnemyLarge,
            EnemyLaser,
            PlayerLaser,
            EnemyHitIcon,
            PlayerHitIcon
        }

        private List<GameObject> EnemySmall;
        private List<GameObject> EnemyLarge;
        private List<GameObject> EnemyLasers;
        private List<GameObject> PlayerLasers;
        private List<GameObject> EnemyLaserHitIcons;
        private List<GameObject> PlayerLaserHitIcons;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        private void Start()
        {
            EnemySmall = new List<GameObject>();
            EnemyLarge = new List<GameObject>();
            EnemyLasers = new List<GameObject>();
            PlayerLasers = new List<GameObject>();
            EnemyLaserHitIcons = new List<GameObject>();
            PlayerLaserHitIcons = new List<GameObject>();

            //Get all available game objects
            foreach (Transform child in transform.Find("Enemies"))
            {
                if (child.name.Contains("Large"))
                {
                    EnemyLarge.Add(child.gameObject);

                }
                else
                {
                    EnemySmall.Add(child.gameObject);
                }

                child.gameObject.SetActive(false);
            }

            foreach (Transform child in transform.Find("EnemyLasers"))
            {
                EnemyLasers.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }

            foreach (Transform child in transform.Find("PlayerLasers"))
            {
                PlayerLasers.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }

            foreach (Transform child in transform.Find("EnemyLaser_HitIcon"))
            {
                EnemyLaserHitIcons.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }

            foreach (Transform child in transform.Find("PlayerLaser_HitIcon"))
            {
                PlayerLaserHitIcons.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeNeeded"></param>
        /// <returns></returns>
        public GameObject RequestObject(ObjectType typeNeeded)
        {
            GameObject objectRequested = null;

            if (typeNeeded == ObjectType.EnemySmall)
            {
                objectRequested = EnemySmall.FirstOrDefault(x => x.activeSelf == false);

                // no object is available, so we'll create a new one
                if (objectRequested == null)
                {
                    objectRequested = Instantiate(SmallEnemyPrefab, EnemySmall[0].transform.parent);
                    objectRequested.transform.localScale = Vector3.one;
                    EnemySmall.Add(objectRequested);
                }
            }
            else if (typeNeeded == ObjectType.EnemyLarge)
            {
                objectRequested = EnemyLarge.FirstOrDefault(x => x.activeSelf == false);

                // no object is available, so we'll create a new one
                if (objectRequested == null)
                {
                    objectRequested = Instantiate(LargeEnemyPrefab, EnemyLarge[0].transform.parent);
                    objectRequested.transform.localScale = Vector3.one;
                    EnemyLarge.Add(objectRequested);
                }
            }
            else if (typeNeeded == ObjectType.EnemyLaser)
            {
                objectRequested = EnemyLasers.FirstOrDefault(x => x.activeSelf == false);

                // no object is available, so we'll create a new one
                if (objectRequested == null)
                {
                    objectRequested = Instantiate(EnemyLaserPrefab, EnemyLasers[0].transform.parent);
                    objectRequested.transform.localScale = new Vector3(.6f, .6f, .6f);
                    EnemyLasers.Add(objectRequested);
                }
            }
            else if (typeNeeded == ObjectType.PlayerLaser)
            {
                objectRequested = PlayerLasers.FirstOrDefault(x => x.activeSelf == false);

                // no object is available, so we'll create a new one
                if (objectRequested == null)
                {
                    objectRequested = Instantiate(PlayerLaserPrefab, PlayerLasers[0].transform.parent);
                    objectRequested.transform.localScale = new Vector3(.6f, .6f, .6f);
                    PlayerLasers.Add(objectRequested);
                }
            }
            else if (typeNeeded == ObjectType.PlayerHitIcon)
            {
                objectRequested = PlayerLaserHitIcons.FirstOrDefault(x => x.activeSelf == false);

                // no object is available, so we'll create a new one
                if (objectRequested == null)
                {
                    objectRequested = Instantiate(PlayerHitIconPrefab, PlayerLaserHitIcons[0].transform.parent);
                    objectRequested.transform.localScale = Vector3.one;
                    PlayerLaserHitIcons.Add(objectRequested);
                }
            }
            else if (typeNeeded == ObjectType.EnemyHitIcon)
            {
                objectRequested = EnemyLaserHitIcons.FirstOrDefault(x => x.activeSelf == false);

                // no object is available, so we'll create a new one
                if (objectRequested == null)
                {
                    objectRequested = Instantiate(EnemyHitIconPrefab, EnemyLaserHitIcons[0].transform.parent);
                    objectRequested.transform.localScale = Vector3.one;
                    EnemyLaserHitIcons.Add(objectRequested);
                }
            }

            return objectRequested;
        }

    }
}

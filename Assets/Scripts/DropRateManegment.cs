using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManegment : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name; // nome do drop
        public GameObject itemPrefab; // prejab do drop

        public float dropRate; // Rating de drop
    }

    public List<Drops> drops;

    void OnDestroy()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();

        foreach (Drops rate in drops)
        {
            if (randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }

        //Confere se tem drops possiveis de dropar e escolhe um deles randomicamente (se houver mais de um drop)
        if (possibleDrops.Count > 0)
        {
            Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}

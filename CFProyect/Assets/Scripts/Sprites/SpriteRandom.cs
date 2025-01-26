using UnityEngine;

public class SpriteRandom : MonoBehaviour
{
    private int rand;
    [SerializeField] private Sprite[] spriteMugre;
    private void OnEnable()
    {
        rand = Random.Range(0, spriteMugre.Length);
        GetComponent<SpriteRenderer>().sprite = spriteMugre[rand];

    }
}

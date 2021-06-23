using UnityEngine;

public class Collectable : MonoBehaviour
{
    private ScoreManager Scm;
    public string CollectableName;
    public GameObject CollectedPrefab;

    private void Awake()
    {
        Scm = FindObjectOfType<ScoreManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Scm.AddFruitAmount(CollectableName, 1);
            GameObject Cllt = Instantiate(CollectedPrefab, transform.position, CollectedPrefab.transform.rotation);
            Destroy(Cllt, 0.3f);
            Destroy(gameObject);
        }
    }
}

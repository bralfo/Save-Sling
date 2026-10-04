using UnityEngine;

public class SalvageItem : MonoBehaviour
{

    [SerializeField] private string itemName;


    [SerializeField] private int value = 10;


    [SerializeField] private float weight = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float destroyY = -16f;
    [SerializeField] private float destroyX = 50f;

    public string ItemName => itemName;
    public int Value => value;
    public float Weight => weight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.mass = weight;
        }
    }

    private void Update()
    {
        Destroy();
    }


    void Destroy()
    { 
        if (transform.position.y < destroyY || transform.position.x > destroyX)
        {
            Destroy(gameObject);
        }
    }
}

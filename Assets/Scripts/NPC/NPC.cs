using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private float limiteX;
    [SerializeField] private float limiteY;

    // [SerializeField] private NPCSpawner spawner;
    private int npcEscolhido;
    private string nomeAnimacao;
    private NPCSpawner spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcEscolhido = Random.Range(0, 16);
        nomeAnimacao = "NPC " + (char)('A' + npcEscolhido);
        animator.Play(nomeAnimacao);

    }

    // Update is called once per frame
    void Update()
    {
        Destroy();
    }

    private void Destroy()
    {
        if (transform.position.x < limiteX || transform.position.y < limiteY)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpawner(NPCSpawner spawner)
    {
        this.spawner = spawner;
    }
    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.NPCSaiu();
        }
    }
}

using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("GTA")]
    [SerializeField] private Transform itemPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] itens;
    [SerializeField] private float velocidade = 2f;

    [Header("SELFDestroy Limits")]
    [SerializeField] private float limiteX;
    [SerializeField] private float limiteY;
    private int itemEscolhido;
    private int npcEscolhido;
    private string nomeAnimacao;
    private NPCSpawner spawner;
    private Transform pontoFila;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemSort();
        NPCSelect();

    }

    private void ItemSort()
    {
        itemEscolhido = Random.Range(0, itens.Length);

        GameObject item = Instantiate(itens[itemEscolhido], itemPoint);

        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.transform.localScale = Vector3.one;

        ItemCarregado itemCarregado = item.GetComponent<ItemCarregado>();

        if (itemCarregado != null)
        {
            itemCarregado.Seguir(itemPoint);
        }
    }

    public void SetPontoFila(Transform ponto)
    {
        pontoFila = ponto;
    }

    private void NPCSelect()
    {
        npcEscolhido = Random.Range(0, 16);
        nomeAnimacao = "NPC " + (char)('A' + npcEscolhido);
        animator.Play(nomeAnimacao);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy();

        PontoFila();
    }

    private void PontoFila()
    {
        if (pontoFila != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                pontoFila.position,
                velocidade * Time.deltaTime
            );
        }
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

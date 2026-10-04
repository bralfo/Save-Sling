using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("GTA")]
    [SerializeField] private Transform itemPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] itens;

    [SerializeField] private float velocidade = 2f;
    [SerializeField] private float alturaPulo = 0.1f;
    [SerializeField] private float velocidadePulo = 8f;

    private Vector3 posicaoBase;

    [Header("SELFDestroy Limits")]
    [SerializeField] private float limiteX;
    [SerializeField] private float limiteY;

    private int itemEscolhido;
    private int npcEscolhido;
    private string nomeAnimacao;

    private NPCSpawner spawner;
    private Transform pontoFila;


    void Start()
    {
        ItemSort();
        NPCSelect();
    }


    void Update()
    {
        Destroy();
        PontoFila();
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


    private void NPCSelect()
    {
        npcEscolhido = Random.Range(0, 16);

        nomeAnimacao = "NPC " + (char)('A' + npcEscolhido);

        animator.Play(nomeAnimacao);
    }


    private void PontoFila()
    {
        if (pontoFila == null)
        {
            return;
        }

        MoverParaFila();
        SaltarEnquantoAnda();
    }


    private void MoverParaFila()
    {
        posicaoBase = Vector3.MoveTowards(
            posicaoBase,
            pontoFila.position,
            velocidade * Time.deltaTime
        );
    }


    private void SaltarEnquantoAnda()
    {
        float distancia = Vector3.Distance(posicaoBase, pontoFila.position);

        if (distancia > 0.05f)
        {
            float salto = Mathf.Abs(
                Mathf.Sin(Time.time * velocidadePulo)
            ) * alturaPulo;

            transform.position = posicaoBase + Vector3.up * salto;
        }
        else
        {
            posicaoBase = pontoFila.position;
            transform.position = posicaoBase;
        }
    }


    public void SetPontoFila(Transform ponto)
    {
        pontoFila = ponto;

        if (pontoFila != null)
        {
            posicaoBase = transform.position;
        }
    }


    private void Destroy()
    {
        if (transform.position.x < limiteX ||
            transform.position.y < limiteY)
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
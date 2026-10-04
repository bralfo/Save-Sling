using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("GTA")]
    [SerializeField] private Transform itemPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] itens;
    [SerializeField] private float velocidade = 2f;
    [SerializeField] private float velocidadeSaida = 8f;
    [SerializeField] private float alturaPulo = 0.05f;
    [SerializeField] private float velocidadePulo = 8f;
    [SerializeField] private Transform pontoSaida;


    [Header("Self Destroy Limits")]
    [SerializeField] private float limiteX;
    [SerializeField] private float limiteY;

    private Vector3 posicaoBase;

    private int itemEscolhido;
    private int npcEscolhido;
    private int indiceFila;

    private string nomeAnimacao;

    private NPCSpawner spawner;
    private Transform pontoFila;
    private Rigidbody2D rb;

    private bool chegouNoPonto;
    private bool esperandoEstilingue;
    private bool saindoDoPredio;

    private GameObject itemAtual;

    private Estilingue estilingue;


    // =========================
    // UNITY
    // =========================

    private void Start()
    {
        ItemSort();
        NPCSelect();

        estilingue = FindAnyObjectByType<Estilingue>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Destruir();

        if (saindoDoPredio)
        {
            SairDoPredio();
            return;
        }

        PontoFila();

        VerificarChegada();

        VerificarEstilingue();
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.NPCSaiu(this);
        }
    }


    // =========================
    // ITEM
    // =========================

    private void ItemSort()
    {
        itemEscolhido = Random.Range(0, itens.Length);

        itemAtual = Instantiate(
            itens[itemEscolhido],
            itemPoint
        );

        itemAtual.transform.localPosition = Vector3.zero;
        itemAtual.transform.localRotation = Quaternion.identity;
        itemAtual.transform.localScale = Vector3.one;

        ItemCarregado itemCarregado =
            itemAtual.GetComponent<ItemCarregado>();

        if (itemCarregado != null)
        {
            itemCarregado.Seguir(itemPoint);
        }
    }

    public SalvageItem PegarItem()
    {
        if (itemAtual == null)
        {
            return null;
        }

        ItemCarregado itemCarregado =
            itemAtual.GetComponent<ItemCarregado>();

        if (itemCarregado != null)
        {
            itemCarregado.PararDeSeguir();
        }

        itemAtual.transform.SetParent(null);

        SalvageItem item =
            itemAtual.GetComponent<SalvageItem>();

        itemAtual = null;

        return item;
    }


    // =========================
    // NPC
    // =========================

    private void NPCSelect()
    {
        npcEscolhido = Random.Range(0, 16);

        nomeAnimacao =
            "NPC " + (char)('A' + npcEscolhido);

        animator.Play(nomeAnimacao);
    }


    // =========================
    // FILA
    // =========================

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
        float distancia =
            Vector3.Distance(
                posicaoBase,
                pontoFila.position
            );

        if (distancia > 0.05f)
        {
            float salto =
                Mathf.Abs(
                    Mathf.Sin(Time.time * velocidadePulo)
                ) * alturaPulo;

            transform.position =
                posicaoBase + Vector3.up * salto;
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

    public void AvancarNaFila(Transform novoPonto)
    {
        SetPontoFila(novoPonto);
    }

    public void SetIndiceFila(int indice)
    {
        indiceFila = indice;
    }

    private void VerificarChegada()
    {
        if (pontoFila == null)
        {
            return;
        }

        float distancia =
            Vector3.Distance(
                posicaoBase,
                pontoFila.position
            );

        if (distancia <= 0.05f)
        {
            chegouNoPonto = true;
        }
        else
        {
            chegouNoPonto = false;
        }
    }

    public bool EstaNoPonto1()
    {
        return chegouNoPonto && indiceFila == 0;
    }


    // =========================
    // ESTILINGUE
    // =========================

    private void VerificarEstilingue()
    {
        if (EstaNoPonto1() && !esperandoEstilingue)
        {
            esperandoEstilingue = true;

            EntregarItem();
        }
    }

    private void EntregarItem()
    {
        SalvageItem item = PegarItem();

        if (item == null)
        {
            return;
        }

        estilingue.SetItem(item, this);

    }

    // =========================
    // SAÍDA DO PRÉDIO
    // =========================

    private void SairDoPredio()
    {
        if (pontoSaida == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            pontoSaida.position,
            velocidadeSaida * Time.deltaTime
        );

        float distancia = Vector3.Distance(
            transform.position,
            pontoSaida.position
        );

        if (distancia <= 0.05f)
        {
            Destroy(gameObject);
        }
    }


    // =========================
    // SPAWNER
    // =========================

    public void SetSpawner(NPCSpawner spawner)
    {
        this.spawner = spawner;
    }

    public void SetPontoSaida(Transform ponto)
    {
        pontoSaida = ponto;

        Debug.Log("Ponto de saída recebido: " + pontoSaida);
    }

    public void ItemFoiLancado()
    {
        saindoDoPredio = true;
    }

    // =========================
    // DESTRUIÇÃO
    // =========================

    private void Destruir()
    {
        if (
            transform.position.x < limiteX ||
            transform.position.y < limiteY
        )
        {
            Destroy(gameObject);
        }
    }
}
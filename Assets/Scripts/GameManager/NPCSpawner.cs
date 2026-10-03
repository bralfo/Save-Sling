using UnityEngine;
using System.Collections;



public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float intervaloSpawn = 1f;
    [SerializeField] private Transform[] pontosFila;
    private bool spawnInicializando;


    private int npcsAtivos;

   
    
    void Start()
    {
        spawnInicializando = true;
        StartCoroutine(NPCSort());
    }

    private IEnumerator NPCSort()
    {
        for (int i = 0; i < 6; i++)
        {
            SpawnNPC();

            yield return new WaitForSeconds(intervaloSpawn);
        }

        spawnInicializando = false;
    }

    void Update()
    {
        if (!spawnInicializando && npcsAtivos < 6 && !gameManager.partidaAcabou)
        {
            SpawnNPC();
        }
    }

    public void SpawnNPC()
    {
        GameObject novoNPC = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);

        NPC npc = novoNPC.GetComponent<NPC>();

        npc.SetSpawner(this);
        npc.SetPontoFila(pontosFila[npcsAtivos]);

        npcsAtivos++;
    }
    public void NPCSaiu()
    {
        npcsAtivos--;
    }
}
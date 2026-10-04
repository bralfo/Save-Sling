using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float intervaloSpawn = 1f;
    [SerializeField] private Transform[] pontosFila;
    [SerializeField] private Transform pontoSaida;
    private bool spawnInicializando;


    private List<NPC> npcsAtivos = new List<NPC>();



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
        if (!spawnInicializando && npcsAtivos.Count < 6 && !gameManager.partidaAcabou)
        {
            SpawnNPC();
        }
    }

    public void SpawnNPC()
    {
        GameObject novoNPC = Instantiate(
        npcPrefab,
        spawnPoint.position,
        spawnPoint.rotation
    );

        NPC npc = novoNPC.GetComponent<NPC>();

        npc.SetSpawner(this);
        npc.SetPontoSaida(pontoSaida);

        npcsAtivos.Add(npc);

        int indiceFila = npcsAtivos.Count - 1;

        npc.SetIndiceFila(indiceFila);
        npc.SetPontoFila(pontosFila[indiceFila]);

    }
    public void NPCSaiu(NPC npc)
    {
        npcsAtivos.Remove(npc);

        AtualizarFila();
    }

    private void AtualizarFila()
    {
        for (int i = 0; i < npcsAtivos.Count; i++)
        {
            npcsAtivos[i].SetIndiceFila(i);
            npcsAtivos[i].AvancarNaFila(pontosFila[i]);
        }
    }
}
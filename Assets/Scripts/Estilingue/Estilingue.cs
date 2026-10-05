using UnityEngine;
using UnityEngine.InputSystem;

public class Estilingue : MonoBehaviour
{
    [Header("Configurações do Estilingue")]
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float maxPullDistance = 5f;

    [Header("Referências")]
    [SerializeField] private Transform launchPoint;

    [Header("Áudio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip somLancamento;

    private SalvageItem currentItem;
    private Rigidbody2D currentRigidbody;
    private NPC currentNPC;

    private Camera mainCamera;
    private bool isAiming;

    private void Awake()
    {
        mainCamera = Camera.main;

        // Tenta obter o AudioSource no mesmo GameObject caso não esteja atribuído no Inspector
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (currentItem == null) return;

        // Inicia a mira ao clicar com o botão esquerdo do mouse
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartAiming();
        }

        if (isAiming)
        {
            UpdateAim();

            // Solta o item e dispara quando o botão do mouse é liberado
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Launch();
            }
        }
    }

    /// <summary>
    /// Recebe o item e a referência do NPC vindos da fila.
    /// </summary>
    public void SetItem(SalvageItem item, NPC npc)
    {
        if (currentItem != null) return;

        currentItem = item;
        currentNPC = npc;

        currentRigidbody = item.GetComponent<Rigidbody2D>();

        if (currentRigidbody != null)
        {
            currentRigidbody.bodyType = RigidbodyType2D.Kinematic;
            currentRigidbody.linearVelocity = Vector2.zero;
            currentRigidbody.angularVelocity = 0f;
        }

        item.transform.position = launchPoint.position;
    }

    private void StartAiming()
    {
        isAiming = true;
    }

    private void UpdateAim()
    {
        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
        mouseScreenPosition.z = -mainCamera.transform.position.z;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f;

        Vector2 direction = (Vector2)(mouseWorldPosition - launchPoint.position);
        direction = Vector2.ClampMagnitude(direction, maxPullDistance);

        currentItem.transform.position = launchPoint.position + (Vector3)direction;
    }

    private void Launch()
    {
        isAiming = false;

        if (currentItem == null || currentRigidbody == null) return;

        // 1. Calcula a direção oposta ao puxão
        Vector2 launchDirection = (Vector2)(launchPoint.position - currentItem.transform.position);

        // 2. Transforma em Dynamic para ativar a gravidade e aplica a força de impulso
        currentRigidbody.bodyType = RigidbodyType2D.Dynamic;
        currentRigidbody.AddForce(
            launchDirection.normalized * launchForce,
            ForceMode2D.Impulse
        );

        // 3. Toca o efeito sonoro de lançamento
        if (audioSource != null && somLancamento != null)
        {
            audioSource.PlayOneShot(somLancamento);
        }

        // 4. Manda a Câmera passar a seguir o item em voo
        CameraController cam = FindAnyObjectByType<CameraController>();
        if (cam != null)
        {
            cam.FollowItem(currentItem.transform);
        }
        else
        {
            Debug.LogWarning("CameraController não encontrado na cena!");
        }

        // 5. Avisa o NPC que entregou o item para sair do prédio
        if (currentNPC != null)
        {
            currentNPC.ItemFoiLancado();
            currentNPC = null;
        }

        // 6. Reseta as variáveis do item no estilingue
        currentItem = null;
        currentRigidbody = null;
    }
}
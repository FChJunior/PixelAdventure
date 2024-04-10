using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Box : MonoBehaviour
{
    [Header("Box")]
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D col2D;
    [SerializeField] private int[] lifes;
    [SerializeField] private int life;
    [SerializeField] private int boxType;
    [SerializeField] private int[] quantBox;
    [SerializeField] private int quant;
    [SerializeField] private GameObject[] breaks;
    [SerializeField] private List<GameObject> itens;
    [SerializeField] private List<float> probabilidades;
    private List<GameObject> itensSorteados = new List<GameObject>();

    void Awake()
    {
        anim.SetInteger("Box", boxType);
        life = lifes[boxType];
        quant = quantBox[boxType];
    }
    void Start()
    {
        for (int i = 0; i < quant; i++)
        {
            GameObject itemSorteado = SortearItem();
            itensSorteados.Add(itemSorteado);
        }
    }
    void Update()
    {
        if (life == 0)
        {
            breaks[boxType].SetActive(true);
            foreach (GameObject item in itensSorteados)
            {
                GameObject go = Instantiate(item, transform.position, Quaternion.identity);
                go.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                go.GetComponent<Collider2D>().isTrigger = false;
            }
            life--;
            spriteRenderer.enabled = false;
            StartCoroutine(Disable());
            col2D.enabled = false;
        }
    }
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(2f);
        breaks[boxType].SetActive(false);

        float time = 0.1f;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(time);
            breaks[boxType].SetActive(true);
            yield return new WaitForSeconds(time);
            breaks[boxType].SetActive(false);
            time -= 0.01f;
        }
    }
    GameObject SortearItem()
    {
        float total = 0;

        // Calcula o total das probabilidades
        foreach (float prob in probabilidades)
        {
            total += prob;
        }

        // Gera um número aleatório entre 0 e o total das probabilidades
        float randomPoint = Random.Range(0, total);

        // Percorre os itens para encontrar qual item corresponde ao ponto aleatório gerado
        for (int i = 0; i < itens.Count; i++)
        {
            if (randomPoint < probabilidades[i])
            {
                return itens[i];
            }
            else
            {
                randomPoint -= probabilidades[i];
            }
        }

        // Retorna -1 se algo der errado
        return null;
    }

    public void Hit(PlayerController player, float dir)
    {
        anim.SetTrigger("Hit");
        player.Jumping(150f * dir, Vector2.up);
        life--;
    }
    
}

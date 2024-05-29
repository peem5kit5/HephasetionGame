using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.prefabItemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }
    public static ItemWorld DropItem( Item item)
    {
        Transform player = GameObject.Find("Player").transform;
        Vector3 randomDir = Random.insideUnitCircle * 3.5f;
        Vector3 throwPos = player.position - randomDir;
        ItemWorld itemWorld = SpawnItemWorld(throwPos , item);
        itemWorld.GetComponent<Rigidbody2D>().velocity = (throwPos - player.position).normalized * 5;
        Destroy(itemWorld.gameObject, 20);
        return itemWorld;
    }
    private Item item;
    private TextMeshPro textMeshPro;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("amountText").GetComponent<TextMeshPro>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if (item.amount > 1)
        {

            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
    }
    public Item GetItem()
    {
        return item;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

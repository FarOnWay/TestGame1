using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNpcController : NpcController
{
    public GameObject inventoryUI;
    bool hasInterected = false;
    /*
    default scritp for the npcs that sell items
    the logic is the same as the NpcController, so thats why we inherit from it
    this code will have the logic for the shopkeeper, that is a inventory (UI element) and a list of items
    the player can buy or change items with the shopkeeper, also sell items as well
    so, the shopkeeper will have a inventory (UI) and a list of items that he can sell
    we can make a logic to diferent days or in events the npcs sell different items, like terraria
    */

    #region ItemsToSell
    /*
    each npc will have a list of items that they can sell
    the items will be a key-value pair, item and price
    */


    [System.Serializable]
    public class ItemAmount
    {
        public ItemInstance item;
        public int price;
    }

    public List<ItemAmount> itemsToSell = new List<ItemAmount>();

    #endregion


    void Update()
    {
        PlayerInteract();
    }

    public override void PlayerInteract()
    {
        /*
        when the player interacts with a shopkeeper npc, both inventorys will be shown, the players and the shopkeepers
        so, the logic is to open the players inventory below the shopkeepers inventory, just like terraria
        to make it easier for the player to buy items
        */


        if (Input.GetMouseButtonDown(1) && !hasInterected)
        {
            if (player == null || spriteRenderer == null || inventoryUI == null)
            {
                Debug.LogError("Player, SpriteRenderer, or InventoryUI is not assigned in the inspector");
                return;
            }

            if (Vector2.Distance(player.transform.position, transform.position) < 4)
            {

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    spriteRenderer.flipX = player.transform.position.x < transform.position.x;
                    inventoryUI.SetActive(true);
                    player.ShowInventory();

                    hasInterected = true;
                    Idle();
                    return;
                }
            }
        }

        else if (Input.GetMouseButtonDown(1) && hasInterected)
        {
            if (inventoryUI == null)
            {
                Debug.LogError("InventoryUI is not assigned in the inspector");
                return;
            }

            inventoryUI.SetActive(false);
            hasInterected = false;
        }
    }
}

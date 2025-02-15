using UnityEngine;

public interface ILootableItem
{
    void AddToInventory();
    Sprite GetSprite();
    string GetName();
    void Use();
}

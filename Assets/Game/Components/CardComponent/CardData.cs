using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards")]
public class CardData : ScriptableObject
{
    public string cardName;
    public string cardDescription;

    public Sprite cardArtwork;
    public Sprite CardArtworkBorders;
}

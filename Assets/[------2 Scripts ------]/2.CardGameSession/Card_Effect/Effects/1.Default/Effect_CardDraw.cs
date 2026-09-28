using UnityEngine;

[CreateAssetMenu (fileName = "Effect_CardDraw", menuName = "Effect/Default/Card_Draw")]
public class Effect_CardDraw : Effect
{
    [Header("Card Draw")]
    [SerializeField]int drawAmout = 1;

    public override void Execute()
    {
        GameManager.Instance.DeckManager.DrawCard(drawAmout);
    }
}

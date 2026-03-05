using System.Collections.Generic;
using UnityEngine;

public class UpgradeInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject UpgradeInfoCardPrefab;

    private List<UpgradeInfoCard> cards = new List<UpgradeInfoCard>();

    public void UpdateUpgradeInfo(UpgradeData data)
    {
        UpgradeInfoCard card = FindUpgradeInfoCard(data.upgradeType);
        if(card != null)
        {
            card.AddUpgradeCount();
        }
        else
        {
            GameObject go = Instantiate(UpgradeInfoCardPrefab, transform);
            UpgradeInfoCard newCard = go.GetComponent<UpgradeInfoCard>();
            if(newCard != null)
            {
                newCard.SetData(data);
                cards.Add(newCard);
            }
        }
    }

    UpgradeInfoCard FindUpgradeInfoCard(UpgradeType type)
    {
        for(int i=0; i<cards.Count; ++i)
        {
            if (cards[i].GetUpgradeType() == type)
            {
                return cards[i];
            }
        }

        return null;
    }
}

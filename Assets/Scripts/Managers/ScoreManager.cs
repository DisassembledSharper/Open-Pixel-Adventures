using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Apples;

    public void AddFruitAmount(string Fruit, int Amount)
    {
        switch(Fruit)
        {
            case "Apple":
                Apples += Amount;
                break;
        }
    }
}

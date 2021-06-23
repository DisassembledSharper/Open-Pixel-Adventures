using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Apples;
    public int Bananas;
    public int Cherries;
    public int Kiwis;
    public int Melons;
    public int Oranges;
    public int Pineapples;
    public int Strawberries;
    public void AddFruitAmount(string Fruit, int Amount)
    {
        switch(Fruit)
        {
            case "Apple":
                Apples += Amount;
                break;
            case "Banana":
                Bananas += Amount;
                break;
            case "Cherry":
                Cherries += Amount;
                break;
            case "Kiwi":
                Kiwis += Amount;
                break;
            case "Melon":
                Melons += Amount;
                break;
            case "Orange":
                Oranges += Amount;
                break;
            case "Pineapple":
                Pineapples += Amount;
                break;
            case "Strawberry":
                Strawberries += Amount;
                break;
        }
    }
}

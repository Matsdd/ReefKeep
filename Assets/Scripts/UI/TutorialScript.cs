using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Canvas tutCanvas;
    public TMP_Text txt;
    public Image tutSpriteRenderer;
    public Image posSpriteRenderer;
    private float tutotimer = 0;
    private int lang = 0;

    // State of the tutorial
    private int state = 0;

    private void Start()
    {
        if (PlayerPrefs.GetFloat("Tutorial") == 0)
        {
            tutCanvas.enabled = true;
        }
        else
        {
            tutCanvas.enabled = false;
        }
        tutSpriteRenderer.color = new Color(0, 0, 0, 0);

        lang = PlayerPrefs.GetInt("LocaleID", 0);

        if (lang == 1)
        {
            txt.text = "Poseidon: Hallo vreemdeling, ik ben Poseidon en ga je begeleiden met je avontuur in Reefkeep.";
        }
        else
        {
            txt.text = "Poseidon: Hello stranger, I'm Poseidon and I'll help you start your adventure in Reefkeep";
        }
    }

    public void onClickTut()
    {
        if (tutotimer <= 0)
        {
            if (state == 0)
            {
                if (lang == 1)
                {
                    txt.text = "Poseidon: Laten we gelijk beginnen, ik zal je eerst uitleg geven over de verschillende gebouwen.";
                }
                else
                {
                    txt.text = "Poseidon: Let's begin, I'll start by explaining the different buildings.";
                }
                changePoseidonAppearance("Poseidon2");
            }
            else if (state == 1)
            {
                if (lang == 1)
                {
                    txt.text = "Poseidon: Het eerste gebouw dat we gaan bespreken is de visitor center, dankzij dit gebouw kunnen er bezoekers komen om je ecosysteem te bekijken.";
                }
                else
                {
                    txt.text = "Poseidon: The first building I'll explain is the visitor center, thanks to this building visitors will come to look at your ecosystem.";
                }
                changeTutorialAppearance("VISITORCENTER_1");
                tutSpriteRenderer.color = new Color(255, 255, 255, 255);
                changePoseidonAppearance("Poseidon1");
            }
            else if (state == 2)
            {
                if (lang == 1)
                {
                    txt.text = "Wanneer je op de visitor center klikt zal je je inkomen zien dat je kan verzamelen.";
                }
                else
                {
                    txt.text = "When you click on the visitor center you'll see your income that you can collect.";
                }
            }
            else if (state == 3)
            {
                if (lang == 1)
                {
                    txt.text = "Poseidon: Dit is de recycling station, dankzij dit gebouw kunnen we afval recyclen en nog wat geld ervan verdienen.";
                }
                else
                {
                    txt.text = "Poseidon: This is the recycling station, thanks to this building we can recycle pieces of trash and collect some money of them.";
                }
                changeTutorialAppearance("RECYCLER_1");
            }
            else if (state == 4)
            {
                if (lang == 1)
                {
                    txt.text = "Wanneer je op dit gebouw klikt zal je ook zien hoeveel je per afval verdient";
                }
                else
                {
                    txt.text = "When you click on this building you can see how much you earn per piece of trash.";
                }
                changePoseidonAppearance("Poseidon2");

            }
            else if (state == 5)
            {
                if (lang == 1)
                {
                    txt.text = "Poseidon: Het volgende gebouw is de library, wanneer je hierop klikt kom je terecht bij de Findex, hierin zie je welke vissen je allemaal kan ontdekken, en hoe je die kan aantrekken.";
                }
                else
                {
                    txt.text = "Poseidon: The next building is the library, when you click on the library the Findex will open, here you can see which fish you can appear in your ecosystem and how to attract them.";
                }

                changeTutorialAppearance("LIBRARY");
            }
            else if (state == 6)
            {
                if (lang == 1)
                {
                    txt.text = "Poseidon: Tot slot hebben we Marcel en zijn tuktuk, wanneer je op hem klikt kom je bij de shop, hier kan je stenen, planten en vissen kopen om je ecosysteem up te graden en nieuwe vissen aan te trekken.";
                }
                else
                {
                    txt.text = "Poseidon: At last we have Juan in his tuktuk, when you click on him you'll open his shop, in the shop you can buy plants, rocks and fish to upgrade your ecosystem and attract new fish.";
                }
                changeTutorialAppearance("TUKTUK");
                changePoseidonAppearance("Poseidon1");
            }
            else if (state == 7)
            {
                if (lang == 1)
                {
                    txt.text = "Poseidon: Bekijk de Findex en koop een steen om te beginnen. Veel succes en veel plezier!";
                }
                else
                {
                    txt.text = "Poseidon: You should take a look at the Findex and buy a rock to start off. Good luck and have fun!";
                }
                tutSpriteRenderer.color = new Color(0, 0, 0, 0);
                changePoseidonAppearance("Poseidon2");
            }
            else
            {
                tutCanvas.enabled = false;
                PlayerPrefs.SetFloat("Tutorial", 1);
            }
            state++;
            tutotimer = 60;
        }
    }

    private void FixedUpdate()
    {
        tutotimer--;
    }

    public void changeTutorialAppearance(string spriteName)
    {
        Sprite levelSprite = Resources.Load<Sprite>("Sprites/Buildings/" + spriteName);
        if (levelSprite != null)
        {
            tutSpriteRenderer.sprite = levelSprite;
        }
        else
        {
            Debug.LogError("Sprite not found with name: " + spriteName);
        }
    }

    public void changePoseidonAppearance(string spriteName) 
    {
        Sprite levelSprite = Resources.Load<Sprite>("Sprites/UI/" + spriteName);
        if (levelSprite != null)
        {
            posSpriteRenderer.sprite = levelSprite;
        }
        else
        {
            Debug.LogError("Sprite not found with name: " + spriteName);
        }
    }
}

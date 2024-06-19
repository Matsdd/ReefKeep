using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Canvas tutCanvas;
    public Canvas blockCanvas;
    public TMP_Text txt;
    public Image tutSpriteRenderer;
    public Image posSpriteRenderer;
    private float tutotimer = 0;
    //0, boven, 1, onder, 2, shop
    public float place;
    public SpriteRenderer spr;

    // State of the tutorial
    private int state = 0;

    private void Start()
    {
        if (place == 0)
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

            if (PlayerPrefs.GetInt("LocaleID") == 1)
            {
                txt.text = "Poseidon: Hallo vreemdeling, ik ben Poseidon en ga je begeleiden met je avontuur in Reefkeep.";
            }
            else
            {
                txt.text = "Poseidon: Hello stranger, I'm Poseidon and I'll accompany you through your adventure in Reefkeep.";
            }
        }else if (place == 1)
        {
            if (PlayerPrefs.GetFloat("TutorialU") == 0)
            {
                tutCanvas.enabled = true;
            }
            else
            {
                tutCanvas.enabled = false;
            }
            tutSpriteRenderer.color = new Color(0, 0, 0, 0);

            if (PlayerPrefs.GetInt("LocaleID") == 1)
            {
                txt.text = "Poseidon: Welkom in jouw eigen ecosysteem, ik zal nog wat extra uitleg geven voor je om je ecosysteem op te bouwen.";
            }
            else
            {
                txt.text = "Poseidon: Welcome to your own ecosystem! I shall explain some things to get you started.";
            }
        }
        else if(place == 2)
        {
            if (PlayerPrefs.GetFloat("TutorialS") == 0)
            {
                tutCanvas.enabled = true;
            }
            else
            {
                tutCanvas.enabled = false;
            }
            tutSpriteRenderer.color = new Color(0, 0, 0, 0);


            if (PlayerPrefs.GetInt("LocaleID") == 1)
            {
                txt.text = "Poseidon: Tot slot zal ik nog een beetje uitleggen over de shop, Marcel hier helpt je om je ecosysteem zo mooi mogelijk in the richten.";
            }
            else
            {
                txt.text = "Poseidon: Last but not least we have the shop, Juan will help you to organise your ecosystem as best as posible.";
            }
            changePoseidonAppearance("Poseidon2");
        }
        if ((PlayerPrefs.GetInt("Tutorial") == 1 && place == 1) || (PlayerPrefs.GetInt("TutorialU") == 1 && place == 2) || (PlayerPrefs.GetInt("TutorialS") == 1 && place == 3))
        {
            playSound();
        }
    }

    public void onClickTut()
    {
        if (tutotimer <= 0)
        {
            if (place == 0)
            {
                if (state == 0)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Laten we gelijk beginnen, hier staan verschillende gebouwen die je helpen.";
                    }
                    else
                    {
                        txt.text = "Poseidon: Let's begin, here are some buildings which you can use.";
                    }
                    changePoseidonAppearance("Poseidon2");
                }
                else if (state == 1)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Dit is de Visitor Center, dankzij dit gebouw komen bezoekers je ecosysteem bekijken.";
                    }
                    else
                    {
                        txt.text = "Poseidon: This is the Visitor Center, thanks to this building people will visit to look at your ecosystem.";
                    }
                    changeTutorialAppearance("Buildings/VISITORCENTER_1");
                    tutSpriteRenderer.color = new Color(255, 255, 255, 255);
                    changePoseidonAppearance("Poseidon1");
                }
                else if (state == 2)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
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
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Het volgende gebouw is de recycling station, dankzij dit gebouw kunnen we afval recyclen en nog wat geld ervan verdienen.";
                    }
                    else
                    {
                        txt.text = "Poseidon: The next building is the recycling station, thanks to this building we can recycle pieces of trash and collect some money of them.";
                    }
                    changeTutorialAppearance("Buildings/RECYCLER_1");
                }
                else if (state == 4)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Wanneer je op dit gebouw klikt staat er hoeveel je per afval verdient.";
                    }
                    else
                    {
                        txt.text = "When you click on this building you can see how much cash you earn per trash.";
                    }
                    changePoseidonAppearance("Poseidon2");

                }
                else if (state == 5)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Het volgende gebouw is de library, wanneer je hierop klikt kom je terecht bij de Findex, hierin zie je welke vissen je allemaal kan ontdekken";
                    }
                    else
                    {
                        txt.text = "Poseidon: The next building is the library, when you click on the library the findex will open, in the findex you can see which fish you can come by in your ecosystem.";
                    }

                    changeTutorialAppearance("Buildings/LIBRARY");
                }
                else if (state == 6)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Tot slot hebben we Marcel in zijn tuktuk, wanneer je op hem klikt kom je bij de shop, hier kan je stenen, planten en vissen kopen om nieuwe vissen aan te trekken.";
                    }
                    else
                    {
                        txt.text = "Poseidon: At last we have Juan in his tuktuk, when you click on him you'll go to his shop, in the shop you can buy plants, rocks and fish to attract new fish.";
                    }
                    changeTutorialAppearance("Buildings/TUKTUK");
                    changePoseidonAppearance("Poseidon1");
                }
                else if (state == 7)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Als je een gebouw wilt verbeteren kan je deze knop klikken, verbeterde gebouwen verdienen vaak meer geld of hebben een extra functie.";
                    }
                    else
                    {
                        txt.text = "Poseidon: If you want to upgrade a building you can click this button, upgraded building will gain extra cash or will gain an extra function.";
                    }
                    changeTutorialAppearance("UI/Button5");
                }
                else if (state == 8)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Ik zal zo nog verder uitleggen over je ecosysteem, klik nu op deze knop op onderwater te gaan.";
                    }
                    else
                    {
                        txt.text = "Poseidon: I'll explain further about your ecosystem, click on this button to dive underwater.";
                    }
                    changeTutorialAppearance("UI/Button9");
                    changePoseidonAppearance("Poseidon2");
                }
                else
                {
                    tutCanvas.enabled = false;
                    PlayerPrefs.SetFloat("Tutorial", 1);
                    tutSpriteRenderer.color = new Color(0, 0, 0, 0);
                    spr.sortingOrder = 10;
                    blockCanvas.enabled = true;
                }
            }else if(place == 1)
            {
                if (state == 0)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Zoals je ziet is je ecosysteem op dit moment erg leeg, maar wanneer je je ecosysteem een beetje op fleurt zal er weer leven in komen.";
                    }
                    else
                    {
                        txt.text = "Poseidon: As you can see, your ecosystem is pretty empty at the moment, but if you brighten up your ecosystem a little, life will come back to it.";
                    }
                    changePoseidonAppearance("Poseidon2");
                }
                else if (state == 1)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Dit soort stukken afval kunnen terecht komen in je ecosysteem, deze kan je recycelen door erop te klikken.";
                    }
                    else
                    {
                        txt.text = "Poseidon: There will be floating pieces of trash in your ecosystem, make sure to recycle these by clicking on them.";
                    }
                    changeTutorialAppearance("Props/TRASH1");
                }
                else if (state == 2)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Zoals je waarschijnlijk al hebt gezien zijn er ook grote clusters afval in je ecosysteem, deze kosten geld om te laten verwijderen.";
                    }
                    else
                    {
                        txt.text = "Poseidon: As you probably already saw there are these big clusters of trash in your ecosystem, these cost cash to remove.";
                    }
                }
                else if (state == 3)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: De laatste uitleg bevind zich in de shop.";
                    }
                    else
                    {
                        txt.text = "Poseidon: The last tutorial is at the shop.";
                    }
                    changeTutorialAppearance("Buildings/TUKTUK");
                    changePoseidonAppearance("Poseidon2");
                }
                else
                {
                    tutCanvas.enabled = false;
                    PlayerPrefs.SetFloat("TutorialU", 1);
                }
            }
            else if (place == 2)
            {
                if (state == 0)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Voor de shop heb je drie verschillende secties: Stenen/Planten, Decoraties en Vissen.";
                    }
                    else
                    {
                        txt.text = "Poseidon: In the shop we have three different section: Rocks/Plants, Decorations and Fish.";
                    }
                    changePoseidonAppearance("Poseidon1");
                }
                else if (state == 1)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Stenen en planten helpen je met vissen aantrekken naar je ecosysteem, decoraties zijn er om je ecosysteem een beetje op te fleuren.";
                    }
                    else
                    {
                        txt.text = "Poseidon: Rocks and Plants will help you attract fish to your ecosystem, decorations are there to brighten up your ecosystem a bit.";
                    }
                    changePoseidonAppearance("Poseidon1");
                }
                else if (state == 2)
                {
                    if (PlayerPrefs.GetInt("LocaleID") == 1)
                    {
                        txt.text = "Poseidon: Oh, en als je wilt weten welke stenen welke vissen aantrekken moet je de findex even een bezoekje brengen. Veel plezier alvast!";
                    }
                    else
                    {
                        txt.text = "Poseidon: Oh, and if you want to know which rocks attract which fish you should pay a visit to the findex. Have a fun in advance!";
                    }
                    changePoseidonAppearance("Poseidon1");
                    changeTutorialAppearance("Buildings/LIBRARY");
                }
                else
                {
                    tutCanvas.enabled = false;
                    PlayerPrefs.SetFloat("TutorialS", 1);
                }
            }
            state++;
            tutotimer = 30;
            playSound();
        }
    }

    private void FixedUpdate()
    {
        tutotimer--;
    }

    public void changeTutorialAppearance(string spriteName)
    {
        Sprite levelSprite = Resources.Load<Sprite>("Sprites/" + spriteName);
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

    public void playSound()
    {
        int random = Random.Range(0, 2);
        if (random == 1)
        {
            SfxManager.instance.playSfx("sPoseidon");
        }
        else
        {
            SfxManager.instance.playSfx("sPoseidon2");
        }
    }
}

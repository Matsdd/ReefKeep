using UnityEngine;
using TMPro;

public class FactsScript : MonoBehaviour
{
    public TMP_Text txt;
    private float newScale = 1;
    private bool turning = false;

    private readonly string[] factsEN = {
    "The Wadden Sea is a UNESCO World Heritage Site with unique tidal flats and rich biodiversity.",
    "The Wadden Sea has the largest intertidal sand and mudflats in the world.",
    "The Wadden Sea's tides expose vast seabeds at low tide.",
    "Millions of migratory birds visit the Wadden Sea annually.",
    "The Wadden Sea is home to many harbor seals and grey seals.",
    "Over 10,000 species live in the Wadden Sea, including endangered ones.",
    "Mudflat hiking is a popular activity in the Wadden Sea.",
    "The Wadden Sea's landscape changes constantly due to tides.",
    "The Wadden Sea is a vital nursery for many marine species.",
    "Male seahorses carry and give birth to their young.",
    "Seahorses can change color to blend in with their surroundings.",
    "Sardines swim in large schools for protection.",
    "Sardines feed on plankton using their gill rakers.",
    "Sturgeons are called living fossils, existing for over 200 million years.",
    "Sturgeons' roe is processed into caviar.",
    "Some sturgeons live over 100 years.",
    "The starry ray has white spots that look like stars.",
    "The starry ray can produce a mild electric discharge for defense.",
    "Seabass eat fish, crustaceans, and cephalopods.",
    "Seabass live in various habitats from rocky shores to open ocean.",
    "Stingrays have a venomous tail barb for defense.",
    "Stingrays detect prey with special sensory organs.",
    "Atlantic salmon migrate from the ocean to rivers to spawn.",
    "Salmon can leap up waterfalls during migration.",
    "Bullhead catfish are nocturnal and feed on insects, crustaceans, and small fish.",
    "Male bullhead catfish guard the eggs and young.",
    "Opah are the only fully warm-blooded fish.",
    "Opah live in deep ocean waters, 50-500 meters down.",
    "Opah can grow over 2 meters long and weigh over 100 kg.",
    "Thresher sharks use their long tails to stun prey.",
    "Thresher sharks sometimes leap out of the water.",
    "Thresher sharks are vulnerable due to overfishing.",
    "Basking sharks are the second largest fish, after whale sharks.",
    "Basking sharks filter-feed on plankton.",
    "Basking sharks migrate long distances seasonally."
};
    private readonly string[] factsNL =
    {
    "De Waddenzee is een UNESCO-werelderfgoed met unieke getijdengebieden en biodiversiteit.",
    "De Waddenzee heeft het grootste systeem van intergetijdenzand en modderplaten ter wereld.",
    "De getijden van de Waddenzee leggen grote zeebodems bloot bij laag water.",
    "Miljoenen trekvogels bezoeken jaarlijks de Waddenzee.",
    "De Waddenzee herbergt veel gewone en grijze zeehonden.",
    "Meer dan 10.000 soorten leven in de Waddenzee, waaronder bedreigde.",
    "Wadlopen is een populaire activiteit in de Waddenzee.",
    "Het landschap van de Waddenzee verandert voortdurend door de getijden.",
    "De Waddenzee is een belangrijke kraamkamer voor veel mariene soorten.",
    "Mannelijke zeepaardjes dragen en baren hun jongen.",
    "Zeepaardjes kunnen van kleur veranderen om zich aan te passen.",
    "Sardines zwemmen in grote scholen voor bescherming.",
    "Sardines voeden zich met plankton via hun kieuwzeef.",
    "Steuren worden levende fossielen genoemd en bestaan al 200 miljoen jaar.",
    "De kuit van steuren wordt verwerkt tot kaviaar.",
    "Sommige steuren worden meer dan 100 jaar oud.",
    "De stekelrog heeft witte vlekken die op sterren lijken.",
    "De stekelrog kan een milde elektrische lading produceren voor verdediging.",
    "Zeebaars eet vissen, kreeftachtigen en koppotigen.",
    "Zeebaars leeft in verschillende habitats van rotskusten tot open oceaan.",
    "Pijlstaartroggen hebben een giftige stekel aan hun staart voor verdediging.",
    "Pijlstaartroggen detecteren prooien met speciale zintuigen.",
    "Atlantische zalmen migreren van de oceaan naar rivieren om te paaien.",
    "Zalm kan watervallen opspringen tijdens migratie.",
    "Meervallen zijn nachtactief en eten insecten, kreeftachtigen en kleine vissen.",
    "Mannelijke meervallen bewaken de eieren en jongen.",
    "Opah zijn de enige volledig warmbloedige vissen.",
    "Opah leeft in diepe oceaanwateren, 50-500 meter diep.",
    "Opah kan meer dan 2 meter lang worden en meer dan 100 kg wegen.",
    "Voshaaien gebruiken hun lange staart om prooien te verdoven.",
    "Voshaaien springen soms uit het water.",
    "Voshaaien zijn kwetsbaar door overbevissing.",
    "Reuzenhaaien zijn de op één na grootste vis, na de walvishaai.",
    "Reuzenhaaien filteren plankton uit het water.",
    "Reuzenhaaien migreren seizoensgebonden over lange afstanden."
};



    // Set the language
    private void Awake()
    {
        int newFact = Random.Range(0, (factsEN.Length - 1));
        if (PlayerPrefs.GetInt("LocaleID") == 0)
        {
            txt.text = "Fact: \n" + factsEN[newFact];
        }
        else
        {
            txt.text = "Wist je dat: \n" + factsNL[newFact];
        }
    }

    // Animate the text with scale
    private void FixedUpdate()
    {
        newScale += turning ? -0.002f : 0.002f;
        if (newScale > 1.22f)
        {
            turning = true;
        }
        else if (newScale < 1f)
        {
            turning = false;
        }
        this.gameObject.transform.localScale = new Vector3(newScale, newScale, 1);
    }
}

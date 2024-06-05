using UnityEngine;
using TMPro;

public class FactsScript : MonoBehaviour
{
    public TMP_Text txt;
    private float newScale = 1;
    private bool turning = false;
    private readonly string[] factsEN = {
        "The Wadden Sea is a UNESCO World Heritage Site, recognized for its unique tidal flats and biodiversity.",
        "The Wadden Sea is the largest unbroken system of intertidal sand and mudflats in the world.",
        "The Wadden Sea experiences a significant tidal range, which creates vast areas of exposed seabed at low tide.",
        "It is a crucial stopover for migratory birds, with millions of birds visiting annually.",
        "The Wadden Sea is home to a significant population of harbor seals and grey seals.",
        "The Wadden Sea hosts over 10,000 species of plants and animals, including many unique and endangered species.",
        "Mudflat hiking (Wadlopen) is a popular activity where people walk across the seabed at low tide",
        "The landscape of the Wadden is constantly changing due to tidal and sedimentary processes.",
        "The Wadden Sea serves as a vital nursery area for many marine species, providing a safe place for a lot of fish.",
        "Many seahorse species are monogamous and mate for life.",
        "Male seahorses carry and give birth to their young.",
        "Seahorse can change color to blend in with their surroundings.",
        "Sardines swim in large schools for protection against predators.",
        "Sardines primarily feed on plankton, filtering it from the water using their gill rakers.",
        "Sturgeons are often referred to as living fossils because they have existed for over 200 million years.",
        "Sturgeons are famous for their roe, which is processed into caviar.",
        "Sturgeons can live for several decades, with some species living over 100 years.",
        "The starry ray has a unique pattern of white spots on its back, resembling stars.",
        "Like other rays, the Starry ray can produce a mild electric discharge for defense.",
        "Seabass have a varied diet, including fish, crustaceans, and cephalopods.",
        "Seabass can be found in a range of habitats from rocky shores to open ocean.",
        "Stingrays have a venomous barb on their tail used for defense.",
        "Stingrays have specialized sensory organs that detect electrical fields produced by prey.",
        "Atlantic salmon are anadromous, migrating from the ocean to freshwater rivers to spawn.",
        "Salmon are known for their ability to leap up waterfalls and obstacles during migration.",
        "Bullhead catfish are primarily nocturnal, feeding at night on a diet of insects, crustaceans, and small fish.",
        "Bullhead catfish build nests and exhibit parental care, with males guarding the eggs and young.",
        "Opah are the only known fully warm-blooded fish, maintaining a higher body temperature than the surrounding water.",
        "Opah inhabit deep ocean waters, often found at depths of 50 to 500 meters.",
        "Opah can grow quite large, with some individuals reaching over 2 meters in length and weighing over 100 kg.",
        "Thresher sharks are known for their extraordinarily long tail fin, which they use to stun prey.",
        "Thresher sharks are known to leap out of the water, a behavior known as breaching.",
        "Thresher sharks are classified as vulnerable due to overfishing and bycatch.",
        "Basking sharks are the second largest fish in the world, after the whale shark.",
        "Basking sharks are filter feeders, consuming plankton by swimming with their mouths wide open.",
        "Basking sharks undertake long migratory journeys, often moving to different regions seasonally."
    };
    private readonly string[] factsNL =
    {
        "De Waddenzee is een UNESCO-werelderfgoed, erkend om zijn unieke getijdengebieden en biodiversiteit.",
        "De Waddenzee is het grootste ononderbroken systeem van intergetijdenzand en modderplaten ter wereld.",
        "De Waddenzee kent een aanzienlijk getijdenverschil, waardoor uitgestrekte gebieden van de zeebodem bij laag water droogvallen.",
        "Het is een cruciale stopplaats voor trekvogels, met miljoenen vogels die jaarlijks langskomen.",
        "De Waddenzee herbergt een aanzienlijke populatie gewone zeehonden en grijze zeehonden.",
        "De Waddenzee herbergt meer dan 10.000 soorten planten en dieren, waaronder veel unieke en bedreigde soorten.",
        "Wadlopen is een populaire activiteit waarbij mensen bij laag water over de zeebodem lopen.",
        "Het landschap van de Wadden verandert voortdurend door getijden- en sedimentaire processen.",
        "De Waddenzee dient als een belangrijke kraamkamer voor veel mariene soorten en biedt een veilige plek voor vele vissen.",
        "Veel soorten zeepaardjes zijn monogaam en paren voor het leven.",
        "Mannelijke zeepaardjes dragen en baren hun jongen.",
        "Zeepaardjes kunnen van kleur veranderen om zich aan hun omgeving aan te passen.",
        "Sardines zwemmen in grote scholen voor bescherming tegen roofdieren.",
        "Sardines voeden zich voornamelijk met plankton en filteren dit uit het water met hun kieuwzeef.",
        "Steuren worden vaak levende fossielen genoemd omdat ze al meer dan 200 miljoen jaar bestaan.",
        "Steuren zijn beroemd om hun kuit, die wordt verwerkt tot kaviaar.",
        "Steuren kunnen meerdere decennia leven, met sommige soorten die meer dan 100 jaar oud worden.",
        "De stekelrog heeft een uniek patroon van witte vlekken op zijn rug, die op sterren lijken.",
        "Net als andere roggen kan de stekelrog een milde elektrische lading produceren voor verdediging.",
        "Zeebaars heeft een gevarieerd dieet, inclusief vissen, kreeftachtigen en koppotigen.",
        "Zeebaars kan worden gevonden in verschillende habitats, van rotsachtige kusten tot open oceaan.",
        "Pijlstaartroggen hebben een giftige stekel aan hun staart die ze gebruiken voor verdediging.",
        "Pijlstaartroggen hebben gespecialiseerde zintuiglijke organen die elektrische velden van prooien detecteren.",
        "Atlantische zalmen zijn anadroom en migreren van de oceaan naar zoetwaterrivieren om te paaien.",
        "Zalm staat bekend om zijn vermogen om watervallen en obstakels op te springen tijdens migratie.",
        "Meervallen zijn voornamelijk nachtactief en voeden zich 's nachts met een dieet van insecten, kreeftachtigen en kleine vissen.",
        "Meervallen bouwen nesten en vertonen ouderlijke zorg, waarbij de mannetjes de eieren en jongen bewaken.",
        "Opah zijn de enige bekende volledig warmbloedige vissen en behouden een hogere lichaamstemperatuur dan het omringende water.",
        "Opah bewonen diepe oceaanwateren, vaak op diepten van 50 tot 500 meter.",
        "Opah kan behoorlijk groot worden, met sommigen die meer dan 2 meter lang worden en meer dan 100 kg wegen.",
        "Voshaaien zijn bekend om hun buitengewoon lange staartvin, die ze gebruiken om prooien te verdoven.",
        "Voshaaien staan bekend om het uit het water springen, een gedrag dat breaching wordt genoemd.",
        "Voshaaien worden geclassificeerd als kwetsbaar door overbevissing en bijvangst.",
        "Reuzenhaaien zijn de op één na grootste vis ter wereld, na de walvishaai.",
        "Reuzenhaaien zijn filtervoeders en consumeren plankton door met hun mond wijd open te zwemmen.",
        "Reuzenhaaien ondernemen lange migratie-reizen en verplaatsen zich vaak seizoensgebonden naar verschillende regio's."
    };

    // Set the language
    private void Awake()
    {
        int newFact = Random.Range(0, (factsEN.Length - 1));
        if (PlayerPrefs.GetInt("LocaleID") == 0)
        {
            txt.text = "Fun Fact: \n" + factsEN[newFact];
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

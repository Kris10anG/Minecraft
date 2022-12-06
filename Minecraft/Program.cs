using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Minecraft
{
    internal class Program
    {
        //Minecraft oppgave!!
        //I Minecraft er det mange ting du kan gjøre! Her er noen av tingene!

        //NETHER
        /*
         *NetherHandler();
         *SearchForNetherCastle(); Kan kanskje møte fiender (pigling
         *BlazeSpawner();
         *SeekBlaze();
         *KillBlaze();
         *DropBlazeRod();
         * BackToWorld();
         */

        //Steve
        /*
         *CheckForEnoughObsidian()
         *CraftNetherPortal()
         * CheckForEnoughFlintAndIron() Flint and steel item
         *
         */
        //Minecraft oppgave!!
        /*I Minecraft er det mange ting du kan gjøre! Her er noen av tingene!
         *Mine etter ting (stone, diamonds, gold, iron osv)
         *
         *Dra til nether (hva trenger du for å dra dit? Kanskje obsidian og flint and steel.
         * Når du har flint and steel skal du lage en tenner til portalen. Den må da sjekke om du har 1 flint og 1 steel i inventoryen for at det skal gå.
         * Du skal smelte iron til steel. Lag en metode som heter MeltOres() som skal smeltet ironet du har minet!
         *Når du miner blokken skal den havne på bakken og karakteren skal plukke den opp og den skal bli lagt til i inventoryen hans
         * Mine(): Mine blokker, DropBlocks(), PickUpBlocks(), GetInInventory()
         * Mine() tilhører Mine klassen, DropBlocks() tilhører Block klassen, PickUpBlocks() tilhører Steve, GetInInventory() tilhører steve
         *
         * Dra til End (hva trenger du for å dra til end? Kanskje Eye of ender)
         * Drepe enderdragon!
         * Bygge hus?
         */

        //Du kan starte med å lage en minecraft karakter. Han skal ha en pickaxe og sticks. han kan ha 1 stack med sticks
        //Du trenger også et inventory! Det kan foreløpig være tomt

        //steve kan ha sin egen meny
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            var game = new Game();
                game.Init(config.GetConnectionString("SqlConnection"));

        }
    }
}

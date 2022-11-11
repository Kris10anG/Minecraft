using System;

namespace Minecraft
{
    internal class Program
    {
        //Minecraft oppgave!!
        //I Minecraft er det mange ting du kan gjøre! Her er noen av tingene!

        //NETHER
        /*
         *NetherHandler();
         *FindNehterCastle();
         *BlazeSpawner();
         *DropBlazeRod();
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
         * Du skal smelte iron til steel. Lag en metode som heter MeltIron() som skal smeltet ironet du har minet!
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
        static void Main(string[] args)
        {
            var steve = new Steve(10, 64, "pickaxe", "sword");
            var mine = new Mine(steve);
            var craftingtable = new Furnace(steve);
            while (true) //til enderdragon er drept!
            {
                Console.WriteLine($"Hva vil du gjøre?\n" +
                                  $"Utforske hula: 1\n" +
                                  $"Sjekke Inventory: 2\n" +
                                  $"Smelte iron ore: 3\n" +
                                  $"Mine flint fra gravel: 4\n" +
                                  $"Dra til nether: 5");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        mine.MineMode();
                        break;
                    case "2":
                        Console.WriteLine($"Hva ønsker du å printe?\n" +
                                          $"1: BlockInventory\n" +
                                          $"2: Monsters loot\n" +
                                          $"3: Sjekke om du kan lage tenner\n" +
                                          $"4: Item list");
                        var option = Console.ReadLine();
                        switch (option)
                        {
                            case "1":
                                steve.PrintBlocks();
                                break;
                            case "2":
                                steve.PrintLoot();
                                break;
                            case "3":
                                steve.CheckForFlintAndSteel();
                                break;
                            case "4":
                                steve.PrintItems();
                                break;
                        }
                        break;
                    case "3":
                        steve.ViewCraftingtable();
                        break;
                    case "4":
                        steve.MineGravelToFlint();
                        break;

                    case "5":
                        if (steve.CheckForEnoughObsidian() && steve.CheckForFlintAndSteel())
                        {
                            Console.WriteLine($"Du er nå i nether :O");
                        }
                        else
                        {
                            Console.WriteLine($"Du kan ikke lage portal til nether");
                        }
                        break;
                    //case "2":
                    //    while (true) //sjekker om jeg har lagd en nether portal
                    //    {
                            
                    //    }
                    //    break;
                }
            }
          
        }
    }
}

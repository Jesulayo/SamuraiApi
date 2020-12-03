using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    internal class Program
    {
        private static SamuraiContext context = new SamuraiContext();
        static void Main(string[] args)
        {
            //context.Database.EnsureCreated();
            //GetSamurais("Before Add:");
            //AddSamurai();
            //InsertMultipleSamurais();
            //GetSamurais("After Add:");

            //QueryFilters();

            //AggregatingInQueries();

            //RetrieveAndUpdateSamurai();

            //RetrieveAndUpdateMultipleSamurai();

            //MultipleDatabaseOperation();

            //RetrieveAndDeleteSamurai();

            //InsertBattle();

            //QueryAndUpdateBAttle_Disconnected();

            //InsertNewSamuraiWithAQuote();

            //AddQuotToSamuraiWhileTracked();

            //AddQuoteToSamuraiWithoutBeingTracked(38);

            //AddQuoteToSamuraiWithoutBeingTrackedUsingAttached(33);

            //AddQuoteToSamuraiWithoutBeingTrackedUsingForeignKey(37);


            //GetAllSamurais();

            //EagerLoadSamuraiWithQuotes();

            //ProjectSomeProperties();

            //ExplicitLoadQuotes();

            //ModifyingRelatedDataWhenNotTracked();

            //JoinBattleAndSamurai();

            //EnlistSamuraiIntoBattle();

            //GetSamuraiWithBattle();

            //GetSamuraiWithBattleProjection();

            //AddNewSamuraiWithHorse();

            //AddNewHorseToSamuraiUsingId();

            //ReplaceHorse();

            //GetSamuraiWithClan();

            //GetClanWithSamurais();

            QuerySamuraiBattleStats();

            //AddSamuraiToClan();
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void InsertMultipleSamurais()
        {
            var samurai1 = new Samurai { Name = "Jackson" };
            var samurai2 = new Samurai { Name = "Jonah" };
            var samurai3 = new Samurai { Name = "Donald" };
            var samurai4 = new Samurai { Name = "Jude" };
            context.Samurais.AddRange(samurai1, samurai2, samurai3, samurai4);
            context.SaveChanges();
        }

        private static void InsertVariousTypes()
        {
            var samurai = new Samurai { Name = "Nunu" };
            var clan = new Clan { ClanName = "Chicharito" };
            context.AddRange(samurai, clan);
            context.SaveChanges();
        }

        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "PhilJipo" };
            context.Samurais.Add(samurai);
            context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void GetAllSamurais()
        {
            var samurais = context.Samurais.ToList();
        }
        private static void QueryFilters()
        {
            var name = "Donald";
            var samurais = context.Samurais.Where(s => s.Name == name).ToList();

            //var samurais = context.Samurais.Where(s => EF.Functions.Like(s.Name, name)).ToList();
            Console.WriteLine(samurais.Count);

            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void AggregatingInQueries()
        {
            var name = "Donald";

            //to return first item
            //var samurai = context.Samurais.Where(s => s.Name == name).FirstOrDefault();
            //or
            //var samurai = context.Samurais.FirstOrDefault(s => s.Id == 12);


            //to find by id
            //var samurai = context.Samurais.FirstOrDefault(s => s.Id == 12);
            //or
            //var samurai = context.Samurais.Where(s => s.Id == 12).FirstOrDefault();
            //or
            //var samurai = context.Samurais.Find(12);

            //last item in a list
            var samurai = context.Samurais.OrderBy(s => s.Id).LastOrDefault(s => s.Name == "Jackson");

            Console.WriteLine(samurai.Name);
            Console.WriteLine(samurai.Id);

        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = context.Samurais.FirstOrDefault();
            samurai.Name += "yes";
            context.SaveChanges();

            var sam = context.Samurais.Where(s => EF.Functions.Like(s.Name, "%yes%"));
            Console.WriteLine(samurai.Name);
            Console.WriteLine(samurai.Id);

        }

        private static void RetrieveAndUpdateMultipleSamurai()
        {
            var samurais = context.Samurais.Skip(8).Take(4).ToList();
            samurais.ForEach(s => s.Name += "San");
            context.SaveChanges();

            var sam = context.Samurais.Where(s => EF.Functions.Like(s.Name, "%San%")).ToList();

            foreach (var samurai in sam)
            {
                Console.WriteLine(samurai.Name);
                Console.WriteLine(samurai.Id);
                Console.WriteLine();
            }

        }

        private static void MultipleDatabaseOperation()
        {
            var samurai = context.Samurais.Where(s => s.Name == "Donald").FirstOrDefault();
            samurai.Name += "New";
            context.Samurais.Add(new Samurai { Name = "Mumu" });
            context.SaveChanges();

        }

        private static void RetrieveAndDeleteSamurai()
        {
            var samurai = context.Samurais.Find(25);
            context.Samurais.Remove(samurai);
            context.SaveChanges();
        }

        private static void InsertBattle()
        {
            context.Battles.Add(new Battle
            {
                Name = "World War 1",
                StartDate = new DateTime(1560, 1, 23),
                EndDate = DateTime.Now
            });
            context.SaveChanges();
        }

        private static void QueryAndUpdateBAttle_Disconnected()
        {
            var battle = context.Battles.AsNoTracking().FirstOrDefault();
            battle.Name = "News war";
            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }

        public static void InsertNewSamuraiWithAQuote()
        {
            var samurai = new Samurai
            {
                Name = "Longi",
                Quotes = new List<Quote>
                {
                    new Quote{Text = "Be wise, Don't be shy"},
                    new Quote{Text = "Oops, no way"}
                }
            };

            context.Samurais.Add(samurai);
            context.SaveChanges();
        }

        public static void AddQuotToSamuraiWhileTracked()
        {
            var samurai = context.Samurais.FirstOrDefault();
            samurai.Quotes.Add(new Quote
            {
                Text = "Saved man"
            });

            context.SaveChanges();
        }

        public static void AddQuoteToSamuraiWithoutBeingTracked(int samuraiId)
        {
            var samurai = context.Samurais.Find(samuraiId);
            samurai.Quotes.Add(new Quote
            {
                Text = "yes yes yes yes"
            });

            using (var newContext = new SamuraiContext())
            {
                newContext.Samurais.Update(samurai);
                newContext.SaveChanges();
            }
        }

        public static void AddQuoteToSamuraiWithoutBeingTrackedUsingAttached(int samuraiId)
        {
            var samurai = context.Samurais.Find(samuraiId);
            samurai.Quotes.Add(new Quote
            {
                Text = "yes yes yes yes"
            });

            using (var newContext = new SamuraiContext())
            {
                newContext.Samurais.Attach(samurai);
                newContext.SaveChanges();
            }
        }

        public static void AddQuoteToSamuraiWithoutBeingTrackedUsingForeignKey(int samuraiId)
        {
            var quote = new Quote
            {
                Text = "NEPA don cut light o",
                SamuraiId = samuraiId
            };
            using (var newContext = new SamuraiContext())
            {
                newContext.Quotes.Add(quote);
                newContext.SaveChanges();
            }
        }

        //GetSamuraiWithQuote
        private static void EagerLoadSamuraiWithQuotes()
        {
            var samuraiWithQuotes = context.Samurais.Where(s => s.Name.Contains("Jonah")).Include(s => s.Quotes).ToList();

        }

        //GetSamuraiWithQuote
        private static void ProjectSomeProperties()
        {
            //var someProperties = context.Samurais.Select(s => new { s.Name, s.Id, s.Quotes, s.Quotes.Count }).ToList();

            //foreach (var samurai in someProperties)
            //{
            //    Console.WriteLine(samurai.Name + " " + samurai.Id);
            //}

            //var somePropertiesWithQuotes = context.Samurais
            //    .Select(s => new { s.Name, s.Id, HappyQuotes = s.Quotes.Where(q => q.Text.Contains("yes")) }).ToList();

            var samuraisWithHappyQuotes = context.Samurais.Where(s => s.Name.Contains("Ayo"))
                .Select(s => new { Samurai = s, HappyQuotes = s.Quotes.Where(q => q.Text.Contains("yes")) }).ToList();

            //var firstSamurai = samuraisWithHappyQuotes[0].Samurai.Name += "Peter";
        }

        private static void ExplicitLoadQuotes()
        {
            var samurai = context.Samurais.FirstOrDefault(s => s.Name.Contains("Longi"));
            context.Entry(samurai).Collection(s => s.Quotes).Load();
            context.Entry(samurai).Reference(s => s.Horse).Load();
        }

        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 39);
            var quote = samurai.Quotes[0];
            quote.Text = "350 school";

            using (var newContext = new SamuraiContext())
            {
                newContext.Entry(quote).State = EntityState.Modified;
                newContext.SaveChanges();
            }
        }

        private static void JoinBattleAndSamurai()
        {
            var sbJoin = new SamuraiBattle { SamuraiId = 39, BattleId = 2 };
            context.Add(sbJoin);
            context.SaveChanges();
        }

        private static void EnlistSamuraiIntoBattle()
        {
            //var battle = context.Battles.Find(2);
            //battle.SamuraiBattles.Add(new SamuraiBattle
            //{
            //    SamuraiId = 39
            //});
            //context.SaveChanges();


            var battle = context.Battles.Find(4);
            battle.SamuraiBattles.Add(new SamuraiBattle
            {
                SamuraiId = 36
            });

            context.SaveChanges();
        }


        private static void GetSamuraiWithBattle()
        {
            var samuraiWithBattle = context.Samurais.Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle)
                .FirstOrDefault(samurai => samurai.Id == 36);
        }

        private static void GetSamuraiWithBattleProjection()
        {
            var samuraiWithBattle = context.Samurais.Where(s => s.Id == 36)
                .Select(s => new
                {
                    Samurai = s,
                    Battle = s.SamuraiBattles.Select(sb => sb.Battle)
                }).
                FirstOrDefault();
        }

        private static void AddNewSamuraiWithHorse()
        {
            var samurai = new Samurai { Name = "Ajet" };
            samurai.Horse = new Horse { Name = "Unicorn" };
            context.Samurais.Add(samurai);
            context.SaveChanges();

        }

        private static void AddNewHorseToSamuraiUsingId()
        {
            var horse = new Horse { Name = "Levels", SamuraiId = 36 };
            context.Add(horse);
            context.SaveChanges();
        }

        private static void ReplaceHorse()
        {
            //var samurai = context.Samurais.Include(s => s.Horse).FirstOrDefault(s => s.Id == 40);
            var samurai = context.Samurais.Find(40);
            context.Entry(samurai).Reference(s => s.Horse).Load();
            samurai.Horse = new Horse { Name = "Astalavi" };


            context.SaveChanges();
        }

        private static void GetSamuraiWithClan()
        {
            var samurai = context.Samurais.Include(s => s.Clan).FirstOrDefault();

        }

        private static void GetClanWithSamurais()
        {
            var clan = new Clan { ClanName = "Sensei" };

            context.Clans.Add(clan);
            context.SaveChanges();
        }

        //private static void AddSamuraiToClan()
        //{
            

        //    context.SaveChanges();
        //}

        private static void QuerySamuraiBattleStats()
        {
            var stats = context.SamuraiBattleStats.ToList();
        }

    }
}

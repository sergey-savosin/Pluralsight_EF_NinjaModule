using NinjaDomain.Classes;
using NinjaDomain.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            //InsertMultipleNinjas();
            //SimpleNinjaQueries();
            //QueryAndUpdateNinja();
            QueryAndUpdateNinjaDisconnected();
            Console.ReadLine();
        }

        private static void InsertNinja()
        {
            var ninja = new Ninja
            {
                Name = "SamsonSan",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(2008, 1, 28),
                ClanId = 1
            };
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }
        private static void InsertMultipleNinjas()
        {
            var ninja1 = new Ninja
            {
                Name = "Leonardo",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 1, 1),
                ClanId = 1
            };
            var ninja2 = new Ninja
            {
                Name = "Raphael",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1985, 1, 1),
                ClanId = 1
            };
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.AddRange(new List<Ninja> { ninja1, ninja2 });
                context.SaveChanges();
            }
        }
        private static void SimpleNinjaQueries()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                DateTime dt = new DateTime(1984, 1, 1);
                string nm = "Raphael";
                var ninjas = context.Ninjas
                    .Where(n => n.DateOfBirth >= dt)
                    .OrderBy(n => n.Name)
                    .Skip(1)
                    .Take(1)
                    .FirstOrDefault()
                    ;
                //var query = context.Ninjas;
                //var someninjas = query.ToList();
                //foreach (var ninja in ninjas)
                //{
                    Console.WriteLine(ninjas.Name);
                //}
            }
        }
        private static void QueryAndUpdateNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = !(ninja.ServedInOniwaban);
                context.SaveChanges();
            }
        }
        private static void QueryAndUpdateNinjaDisconnected()
        {
            Ninja ninja;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
            }

            ninja.ServedInOniwaban = !(ninja.ServedInOniwaban);
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Attach(ninja);
                context.Entry(ninja).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}

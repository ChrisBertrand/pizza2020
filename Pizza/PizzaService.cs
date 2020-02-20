using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Pizza
{
    class Pizza
    {
        public Pizza(int id, int slices)
        {
            Id = id;
            Slices = slices;
        }

        public int Id { get; set; }
        public int Slices { get; set; }
    }

    class Order
    {
        public int SlicesNeeded { get; set; }
        public List<Pizza> PizzasSelected { get; set; }

    }

    class PizzaService
    {
        // one pizza of each type
        public Order order = new Order();

        // size == number of slices

        // required slices, get as many as possible but not more
        public List<Pizza> getPizzas(string path)
        {
            string[] lines = File.ReadAllLines(path);

            order.PizzasSelected = new List<Pizza>();
            order.SlicesNeeded = int.Parse(lines[0].Split(" ")[0]);
            
            var pizzaVariationCount = int.Parse(lines[0].Split(" ")[1]);
            var pizzaVariations = new List<Pizza>(pizzaVariationCount);

            Console.WriteLine($"Order need {order.SlicesNeeded} with {pizzaVariationCount} types, {lines.Length}");
            var slicesPer = lines[1].Split(" ");
            for (var i = 0; i < slicesPer.Length - 1; i++)
            {
                        var pizza = new Pizza(i, int.Parse(slicesPer[i]));
                        pizzaVariations.Add(pizza);
            }

            return pizzaVariations;
        }

        public Order getPizzasToOrder() {

            //List<Pizza> PizzasAvailable = getPizzas("C:/Users/chris.bertrand/source/repos/Pizza/Pizza/a_example.in");
            //List<Pizza> PizzasAvailable = getPizzas("C:/Users/chris.bertrand/source/repos/Pizza/Pizza/b_small.in");
            //List<Pizza> PizzasAvailable = getPizzas("C:/Users/chris.bertrand/source/repos/Pizza/Pizza/c_medium.in");
            //List<Pizza> PizzasAvailable = getPizzas("C:/Users/chris.bertrand/source/repos/Pizza/Pizza/d_quite_big.in");
            List<Pizza> PizzasAvailable = getPizzas("C:/Users/chris.bertrand/source/repos/Pizza/Pizza/e_also_big.in");

            Console.WriteLine($"pizzas: {PizzasAvailable.Count}");

            int currentCount = 0;
            foreach(var pizza in PizzasAvailable.OrderByDescending(p => p.Slices))
            {
                if (currentCount <= order.SlicesNeeded)
                {
                    var wouldBeCount = currentCount + pizza.Slices;

                    if (wouldBeCount > order.SlicesNeeded)
                    {
                        continue;
                    }
                    else
                    {
                        currentCount += pizza.Slices;
                        order.PizzasSelected.Add(pizza);
                    }
                }
            }
            foreach (var p in order.PizzasSelected)
            {
                Console.WriteLine($"pizzas {p.Id} with {p.Slices}");
            }
                return order;
        }

        public void createOrder(Order order)
        {
            Console.WriteLine($" Order is: {order.SlicesNeeded}, slices got: {order.PizzasSelected.Sum(p => p.Slices)}");

            var logPath = "C:/Users/chris.bertrand/source/repos/Pizza/Pizza/alsobig.in";
           
            using (StreamWriter pizzaWriter = new StreamWriter(logPath))
            {
                pizzaWriter.WriteLine(order.PizzasSelected.Count);
                pizzaWriter.WriteLine(String.Join(" ", order.PizzasSelected.Select(p => p.Id)));
            }
        }
    }
}

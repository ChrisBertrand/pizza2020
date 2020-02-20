using System;

namespace Pizza
{
    class Program
    {
        static void Main(string[] args)
        {
            var ps = new PizzaService();
            var order = ps.getPizzasToOrder();
            ps.createOrder(order);
        }
    }
}

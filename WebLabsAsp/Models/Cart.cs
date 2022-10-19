using WebLabsAsp.Entities;

namespace WebLabsAsp.Models;

public class Cart
{
    public Dictionary<Guid,CartItem> Items { get; set; }
    public Cart()
    {            
        Items = new Dictionary<Guid, CartItem>();
    }
    public int Count
    {
        get
        {
            return Items.Sum(item => item.Value.Quantity);
        }
    }
    public int Price
    {
        get
        {
            return Items.Sum(item => item.Value.Quantity * item.Value.Car.Price);
        }
    }
    public virtual void AddToCart(Car car)
    {
        if (Items.ContainsKey(car.Id))
            Items[car.Id].Quantity++;
        else
            Items.Add(car.Id, new CartItem {
                Car = car, Quantity = 1
            });
    }

    public virtual void RemoveOneFromCart(Car car)
    {
        if (!Items.ContainsKey(car.Id)) return;
        if (Items[car.Id].Quantity > 1)
            Items[car.Id].Quantity--;
        else
            Items.Remove(car.Id);
    }

    public virtual void RemoveFromCart(Guid id)
    {
        Items.Remove(id);
    }
    
    public virtual void ClearAll()
    {
        Items.Clear();
    }
}


public class CartItem
{        
    public Car Car { get; set; }
    public int Quantity { get; set; }
}
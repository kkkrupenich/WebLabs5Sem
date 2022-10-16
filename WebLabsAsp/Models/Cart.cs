using WebLabsAsp.Entities;

namespace WebLabsAsp.Models;

public class Cart
{
    public Dictionary<Guid,CartItem> Items { get; set; }
    public Cart()
    {            
        Items = new Dictionary<Guid, CartItem>();
    }
    /// <summary>
    /// Количество объектов в корзине
    /// </summary>
    public int Count
    {
        get
        {
            return Items.Sum(item => item.Value.Quantity);
        }
    }
    /// <summary>
    /// Количество калорий
    /// </summary>
    public int Price
    {
        get
        {
            return Items.Sum(item => item.Value.Quantity * item.Value.Car.Price);
        }
    }
        
    /// <summary>
    /// Добавление в корзину
    /// </summary>
    /// <param name="car">добавляемый объект</param>
    public virtual void AddToCart(Car car)
    {
        // если объект есть в корзине
        // то увеличить количество
        if (Items.ContainsKey(car.Id))
            Items[car.Id].Quantity++;
        // иначе - добавть объект в корзину
        else
            Items.Add(car.Id, new CartItem {
                Car = car, Quantity = 1
            });
    }
        
    /// <summary>
    /// Удалить объект из корзины
    /// </summary>
    /// <param name="id">id удаляемого объекта</param>
    public virtual void RemoveFromCart(Guid id)
    {
        Items.Remove(id);
    }
        
    /// <summary>
    /// Очистить корзину
    /// </summary>
    public virtual void ClearAll()
    {
        Items.Clear();
    }
}

/// <summary>
/// Клас описывает одну позицию в корзине
/// </summary>
public class CartItem
{        
    public Car Car { get; set; }
    public int Quantity { get; set; }
}
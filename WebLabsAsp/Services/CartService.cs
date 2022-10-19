using Newtonsoft.Json;
using WebLabsAsp.Entities;
using WebLabsAsp.Models;
using WebLabsAsp.Extensions;

namespace WebLabsAsp.Services;

public class CartService : Cart
{
    private string sessionKey = "cart";

    [JsonIgnore]
    ISession Session { get; set; }

    public static Cart GetCart(IServiceProvider sp)
    {
        var session = sp.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

        var cart = session?.Get<CartService>("cart")
                   ?? new CartService();
        cart.Session = session;
        return cart;
    }
    public override void AddToCart(Car car)
    {
        base.AddToCart(car);
        Session?.Set<CartService>(sessionKey, this);
    }

    public override void RemoveOneFromCart(Car car)
    {
        base.RemoveOneFromCart(car);
        Session.Set<CartService>(sessionKey, this);
    }
    public override void RemoveFromCart(Guid id)
    {
        base.RemoveFromCart(id);
        Session?.Set<CartService>(sessionKey, this);
    }
    public override void ClearAll()
    {
        base.ClearAll();
        Session?.Set<CartService>(sessionKey, this);
    }
}
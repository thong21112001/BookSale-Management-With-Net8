using BookSale.Management.UI.Models;

namespace BookSale.Management.UI.Helpers
{
    //Class giúp cho phần cartController ít nặng về code, dễ bảo trì
    public static class CartHelper
    {
        private static string CartSessionName = "CartSession";

        //Lấy các item trong giỏ hàng lưu bằng session
        public static List<CartModel> GetCartItems(ISession session)
        {
            return session.Get<List<CartModel>>(CartSessionName) ?? new List<CartModel>();
        }

        //Tiến hành thêm sản phẩm vào giỏ hàng bằng session
        public static void AddToCart(ISession session, CartModel cartModel)
        {
            var carts = GetCartItems(session);

            var cartExist = carts.FirstOrDefault(x => x.CodeBook == cartModel.CodeBook);

            if (cartExist is null)
            {
                carts.Add(cartModel);
            }
            else
            {
                cartExist.Quantity += cartModel.Quantity;
            }

            session.Set(CartSessionName, carts);
        }

        //Tăng số lượng của 1 mặt hàng trong giỏ hàng
        public static int IncrementQuantity(ISession session, string codeBook)
        {
            var carts = GetCartItems(session);
            var cartItem = carts.FirstOrDefault(x => x.CodeBook == codeBook);

            if (cartItem != null)
            {
                cartItem.Quantity++;
                session.Set(CartSessionName, carts);
                return cartItem.Quantity;
            }

            return 0;
        }

        //Giảm số lượng của 1 mặt hàng trong giỏ hàng
        public static int DecrementQuantity(ISession session, string codeBook)
        {
            var carts = GetCartItems(session);
            var cartItem = carts.FirstOrDefault(x => x.CodeBook == codeBook);

            if (cartItem != null)
            {
                cartItem.Quantity--;
                if (cartItem.Quantity <= 0)
                {
                    carts.Remove(cartItem);
                }
                session.Set(CartSessionName, carts);
                return cartItem.Quantity;
            }

            return 0;
        }

        //Xoá mặt hàng khỏi giỏ hàng
        public static void RemoveFromCart(ISession session, string codeBook)
        {
            var carts = GetCartItems(session);
            var cartItem = carts.FirstOrDefault(x => x.CodeBook == codeBook);

            if (cartItem != null)
            {
                carts.Remove(cartItem);
                session.Set(CartSessionName, carts);
            }
        }
    }
}

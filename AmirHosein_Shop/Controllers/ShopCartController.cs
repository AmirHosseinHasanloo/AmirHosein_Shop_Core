using System;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Azure.Documents;

namespace AmirHosein_Shop.Controllers
{
    public class ShopCartController : Controller
    {
        private EshopContext _context;
        private UnitOfWork _unitOfWork;
        private IOrdersRepository _ordersRepository;
        private IProductRepository _productRepository;
        private IOrderDetailRepository _orderDetailRepository;

        public ShopCartController(EshopContext context, IOrdersRepository ordersRepository, IProductRepository productRepository, IOrderDetailRepository orderDetailRepository)
        {
            _unitOfWork = new UnitOfWork(context);
            _context = context;
            _ordersRepository = ordersRepository;
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
        }


        void AddOrderDetail(Product product, int userId, int orderid)
        {
            _unitOfWork.OrderDetailsRepository.Insert(new OrderDetails()
            {
                OrderId = orderid,
                ProductId = product.Id,
                Price = product.Item.Price,
                Count = 1
            });
        }

        [Authorize]
        [Route("AddToCart/{id}")]
        public IActionResult AddToCart(int id)
        {
            var Product = _productRepository.GetProductForAddCart(id);

            if (Product != null)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                var order = _ordersRepository.GetUserOrder(userId);
                if (order != null)
                {
                    var IsExist = _orderDetailRepository.isExistOrderDetails(order.OrderId, Product.Id);

                    if (IsExist != null)
                    {
                        IsExist.Count += 1;
                    }
                    else
                    {
                        // add order detail
                        AddOrderDetail(Product, userId, order.OrderId);
                    }
                }
                else
                {

                    order = new Order
                    {
                        UserId = userId,
                        CreateDate = DateTime.Now,
                        IsFinally = false
                    };
                    // add order 
                    _unitOfWork.OrderRepository.Insert(order);

                    _unitOfWork.OrderRepository.Save();


                    // add order detail
                    AddOrderDetail(Product, userId, order.OrderId);

                }
                _unitOfWork.OrderDetailsRepository.Save();
            }
            return RedirectToAction("ShowCart");
        }

        [Authorize]
        public IActionResult ShowCart()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var orders = _ordersRepository.GetOrdersForShowCart(userId);
            return View(orders);
        }

        public IActionResult DeleteCart(int detailid)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var detail = _unitOfWork.OrderDetailsRepository.GetById(detailid);

            if (detail.Count <= 1)
            {
                _unitOfWork.OrderDetailsRepository.Delete(detail);
                _unitOfWork.OrderDetailsRepository.Save();
            }
            else if (detail.Count > 1)
            {
                detail.Count -= 1;
                _unitOfWork.OrderDetailsRepository.Save();
            }

            return RedirectToAction("ShowCart");
        }


    }
}

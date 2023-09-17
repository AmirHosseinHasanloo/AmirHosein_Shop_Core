using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class UnitOfWork
    {
        private EshopContext _context;

        public UnitOfWork(EshopContext context)
        {
            _context = context;
        }

        private GenericRepository<Users> _usersRepository;
        public GenericRepository<Users> usersRepository
        {
            get
            {
                if (_usersRepository == null)
                {
                    _usersRepository = new GenericRepository<Users>(_context);
                }
                return _usersRepository;
            }
        }

        private GenericRepository<Category> _CategoryRepository;
        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (_CategoryRepository == null)
                {
                    _CategoryRepository = new GenericRepository<Category>(_context);
                }
                return _CategoryRepository;
            }
        }

        private GenericRepository<CategoryToProduct> _CategoryToProductRepository;
        public GenericRepository<CategoryToProduct> CategoryToProductRepository
        {
            get
            {
                if (_CategoryToProductRepository == null)
                {
                    _CategoryToProductRepository = new GenericRepository<CategoryToProduct>(_context);
                }
                return _CategoryToProductRepository;
            }
        }

        private GenericRepository<Product> _ProductRepository;
        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (_ProductRepository == null)
                {
                    _ProductRepository = new GenericRepository<Product>(_context);
                }
                return _ProductRepository;
            }
        }



        private GenericRepository<ShopItem> _ShopItemRepository;
        public GenericRepository<ShopItem> ShopItemRepository
        {
            get
            {
                if (_ShopItemRepository == null)
                {
                    _ShopItemRepository = new GenericRepository<ShopItem>(_context);
                }
                return _ShopItemRepository;
            }
        }

        private GenericRepository<Order> _OrderRepository;
        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (_OrderRepository == null)
                {
                    _OrderRepository = new GenericRepository<Order>(_context);
                }
                return _OrderRepository;
            }
        }


        private GenericRepository<OrderDetails> _OrderDetailsRepository;
        public GenericRepository<OrderDetails> OrderDetailsRepository
        {
            get
            {
                if (_OrderDetailsRepository == null)
                {
                    _OrderDetailsRepository = new GenericRepository<OrderDetails>(_context);
                }
                return _OrderDetailsRepository;
            }
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

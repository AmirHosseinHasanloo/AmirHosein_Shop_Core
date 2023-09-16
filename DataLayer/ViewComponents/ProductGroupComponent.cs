using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ProductGroupComponent : ViewComponent
    {
        private IProductGroupsRepository _groupsRepository;

        public ProductGroupComponent(IProductGroupsRepository groupsRepository)
        {
            _groupsRepository = groupsRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           

            return View("/Views/Components/ProductGroupsComponent.cshtml", _groupsRepository.GetGroupForShow());

        }

    }
}

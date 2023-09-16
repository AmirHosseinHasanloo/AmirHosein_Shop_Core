using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public interface IProductGroupsRepository
    {
        IEnumerable<Category> GetAll();
        IEnumerable<ShowGroupViewModel> GetGroupForShow();
    }
}

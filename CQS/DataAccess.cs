using CQS.Domain;
using System.Collections.Generic;

namespace CQS.DataAccess
{
    public class ApplicationDbContext
    {
        public IEnumerable<Book> Books;
    }
}

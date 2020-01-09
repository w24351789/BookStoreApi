using System.Collections.Generic;

namespace Domain
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Review Country { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}

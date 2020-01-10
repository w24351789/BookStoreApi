using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}

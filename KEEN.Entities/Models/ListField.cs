using System.Collections.Generic;

namespace KEEN.Entities.Models
{
    public class ListField : Field
    {
        public IEnumerable<string> Values { get; set; }
    }
}

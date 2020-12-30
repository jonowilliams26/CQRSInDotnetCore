using CQRSTest.Domain;
using System.Collections.Generic;

namespace CQRSTest.Database
{
    public class Repository
    {
        public List<Todo> Todos { get; } = new List<Todo>
        {
            new Todo{Id = 1, Name = "Cook dinner", Completed = false },
            new Todo{Id = 2, Name = "Make Youtube video", Completed = true },
            new Todo{Id = 3, Name = "Wash car", Completed = false },
            new Todo{Id = 4, Name = "Practice programming", Completed = true },
            new Todo{Id = 5, Name = "Take out garbage", Completed = false },
        };
    }
}

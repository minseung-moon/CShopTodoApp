using System.Collections.Generic;

namespace CShopTodoApp.Models
{
    public interface ITodoRepositoryInMemory
    {
        void Add(Todo model); // 입력
        List<Todo> GetAll(); // 출력
    }

}

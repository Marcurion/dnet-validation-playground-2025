using Domain.Interfaces;

namespace Application
{
    public class MyManager
    {
        private IRepository<object> _repository;

        public MyManager(IRepository<object> repository)
        {
            _repository = repository;
        }

        public void ChangeSomeData()
        {
            _repository.SaveAsync();
        }

    }
}

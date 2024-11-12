using IDonEnglist.Application.ViewModels.Collection;

namespace IDonEnglist.Application.ViewModels.FinalTest
{
    public class FinalTestViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public CollectionViewModelMin Collection { get; set; }
    }
}

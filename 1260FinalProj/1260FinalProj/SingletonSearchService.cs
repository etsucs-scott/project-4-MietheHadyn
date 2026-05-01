using _1260FinalProj.Logic;
using static _1260FinalProj.Logic.Searching; 
namespace _1260FinalProj.Services //Singleton service???
{
    public class SingletonSearchService
    {
        public Guid InstanceId { get; } = Guid.NewGuid();
        public Searching SingleSearch = new Searching(); //search instancer


    }
}

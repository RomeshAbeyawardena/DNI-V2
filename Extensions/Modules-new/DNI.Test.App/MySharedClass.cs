namespace DNI.Test.App
{
    public class MyService : IMyService
    {
        public MyService(IMySharedClass mySharedClass)
        {
            MySharedClass = mySharedClass;
        }

        public IMySharedClass MySharedClass { get; }
    }

    public interface IMyService
    {
        IMySharedClass MySharedClass { get; }
    }

    public interface IMySharedClass
    {
        int Value { get; set; }
    }

    public class MySharedClass : IMySharedClass
    {
        public int Value { get; set; }
    }
}
